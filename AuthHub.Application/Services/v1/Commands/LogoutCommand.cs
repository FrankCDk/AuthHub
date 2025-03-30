using MediatR;

namespace AuthHub.Application.Services.v1.Commands
{
    public class LogoutCommand : IRequest<bool>
    {
        public int IdUser { get; set; }
        public string? User {  get; set; }
        public string? Token { get; set; }
    }
}
