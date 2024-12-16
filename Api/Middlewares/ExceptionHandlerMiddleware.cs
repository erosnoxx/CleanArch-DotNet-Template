using Application.Exceptions;
using Application.Models.Exceptions;
using System.Net;
using System.Text;

namespace Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private string _body = string.Empty;
        private const string LinkReference = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status";

        public ExceptionHandlerMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlerMiddleware> logger
        )
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                context.Request.EnableBuffering();

                _body = await ReadRequestBodyAsync(context.Request) ?? string.Empty;

                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred: {Message}", e.Message);
                await HandleExceptionAsync(context, e);
            }

        }

        private async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            if (context.Response.Headers.IsReadOnly)
            {
                return;
            }
            var customException = new CustomProblemWebApiDetail();

            var statusCode = HttpStatusCode.InternalServerError;
            switch (e)
            {
                case ConflictException:
                    statusCode = HttpStatusCode.Conflict;
                    break;
                case NoContentException:
                    statusCode = HttpStatusCode.NoContent;
                    break;
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    break;

                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    break;

                case ArgumentException:
                case InvalidOperationException:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
            }

            customException.Exception = e.GetType().Name;
            customException.StatusCode = (int)statusCode;
            customException.Message = e.Message;
            customException.LinkReference = $"{LinkReference}/{customException.StatusCode}";

            context.Response.StatusCode = customException.StatusCode;

            await context.Response.WriteAsJsonAsync(customException);
        }

        private async Task<string?> ReadRequestBodyAsync(HttpRequest request)
        {
            if (request.Body == null || !request.Body.CanRead)
            {
                return null;
            }

            using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            {
                var requestBody = await reader.ReadToEndAsync();

                // Rebobinar o corpo da solicitação para que outros middlewares possam lê-lo
                request.Body.Seek(0, SeekOrigin.Begin);

                return requestBody;
            }
        }
    }
}
