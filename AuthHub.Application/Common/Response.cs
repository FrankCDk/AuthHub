namespace AuthHub.Application.Common
{
    public class Response<T> : Response
    {        
        public T? Data { get; set; }
    }

    public class Response
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }


}
