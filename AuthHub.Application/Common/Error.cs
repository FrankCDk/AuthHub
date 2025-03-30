namespace AuthHub.Application.Common
{
    public class Error
    {
        public int StatusCode { get; set; } = 400;
        public string Message { get; set; } = "Error interno";

    }
}
