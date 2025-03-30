using AuthHub.Application.Auth.v1.Commands;
using FluentValidation;

namespace AuthHub.Application.Validations.v1.Auth
{
    public class LoginComandValidation : AbstractValidator<LoginCommand>
    {

        public LoginComandValidation() {

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("El password no puede estar vacio.")
                .MinimumLength(6).WithMessage("La longitud minima del password debe ser de 6 caracteres.");

            RuleFor(u => u.Email)
                .EmailAddress().WithMessage("Debe ingresar una dirección de correo valida.");

            RuleFor(u => u.Username)
                .MinimumLength(6).WithMessage("La longitud minima del Usuario debe ser de 6 caracteres.");

        }
    }
   
}
