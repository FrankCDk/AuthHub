namespace AuthHub.Application.Dtos.Response
{
    public class LoginDto
    {
        public string? Correo {  get; set; }
        public string? Usuario {  get; set; }
        public string? Token {  get; set; }
        public string? Rol { get; set; }
    }
}
