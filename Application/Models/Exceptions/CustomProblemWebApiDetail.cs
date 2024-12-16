namespace Application.Models.Exceptions
{
    public class CustomProblemWebApiDetail
    {
        public string Exception { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string LinkReference { get; set; } = string.Empty;
    }
}
