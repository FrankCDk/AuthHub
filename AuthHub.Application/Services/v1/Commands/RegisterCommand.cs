using AuthHub.Application.Dtos.Response;
using MediatR;

namespace AuthHub.Application.Services.v1.Commands
{
    public class RegisterCommand : IRequest
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string PasswordConfirmed { get; set; }
        public required string State { get; set; }
        public required string Role { get; set; }
    }
}
