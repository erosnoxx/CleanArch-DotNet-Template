using Application.Contracts.Services;
using Application.Enums;
using Application.Extensions;
using Application.Models.Role;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Text.Json;

namespace Api.Authentication
{
    public class PermissionsAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string _permission;

        public PermissionsAuthorizeAttribute(string permission)
        {
            _permission = permission;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var rolesConfig = context.HttpContext.RequestServices.GetRequiredService<RolesConfig>();
            var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();

            var allowAnonymous = context.ActionDescriptor.EndpointMetadata
                .OfType<AllowAnonymousAttribute>()
                .Any();

            if (allowAnonymous)
            {
                return;
            }

            var authenticateResult = await context.HttpContext.AuthenticateAsync();

            if (!authenticateResult.Succeeded)
            {
                throw new UnauthorizedAccessException("Você não está autenticado");
            }

            var userId = authenticateResult.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new UnauthorizedAccessException("Você não está autenticado");
            }

            var user = await userService.GetUserByIdAsync(userId);

            if (user is null)
            {
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Content = JsonSerializer.Serialize(new { message = "Usuário não existe" }),
                    ContentType = "application/json"
                };
                return;
            }

            if (!user.Enabled)
            {
                throw new UnauthorizedAccessException("Usuário desabilitado");
            }

            if (user.Role.ContainsEnum(ERole.Admin))
            {
                return;
            }

            var roles = rolesConfig.Roles;

            if (!roles.Any(r => r.Name.Equals(user.Role, StringComparison.OrdinalIgnoreCase)))
            {
                throw new UnauthorizedAccessException("Você não tem permissão para acessar este recurso");
            }

            if (!roles.Any(r => r.Name.Equals(user.Role, StringComparison.OrdinalIgnoreCase) && r.Permissions.Contains(_permission)))
            {
                throw new UnauthorizedAccessException("Você não tem permissão para acessar este recurso");
            }
        }
    }
}
