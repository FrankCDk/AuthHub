namespace AuthHub.Application.Dto.Login
{
    public class LoginResponse
    {
        public string Username { get; set; } = string.Empty;
        public string? Token { get; set; }
    }
}
