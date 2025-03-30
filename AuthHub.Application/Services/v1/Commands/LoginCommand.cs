using AuthHub.Application.Dtos.Response;
using MediatR;

namespace AuthHub.Application.Auth.v1.Commands
{
    public class LoginCommand : IRequest<LoginDto>
    {

        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

    }
}
