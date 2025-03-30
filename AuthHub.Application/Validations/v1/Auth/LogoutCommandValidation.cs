using AuthHub.Application.Services.v1.Commands;
using FluentValidation;

namespace AuthHub.Application.Validations.v1.Auth
{
    public class LogoutCommandValidation : AbstractValidator<LogoutCommand>
    {
        public LogoutCommandValidation()
        {

        }
    }
}
