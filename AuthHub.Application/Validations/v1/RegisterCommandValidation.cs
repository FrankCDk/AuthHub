using AuthHub.Application.Services.v1.Commands;
using FluentValidation;

namespace AuthHub.Application.Validations.v1
{
    internal class RegisterCommandValidation : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidation()
        {

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("El nombre no puede estar vacio.")
                .MaximumLength(50).WithMessage("La longitud del nombre excede al limite establecido.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("El apellido no puede estar vacio.")
                .MaximumLength(50).WithMessage("La longitud del apellido excede al limite establecido.");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("El nombre de usuario no puede estar vacio.")
                .MaximumLength(50).WithMessage("La longitud del nombre de usuario excede al limite establecido.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Correo no puede estar vacio.")
                .MaximumLength(100).WithMessage("La longitud del email excede al limite establecido.")
                .EmailAddress().WithMessage("Correo no tiene un formato valido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Contraseña no puede estar vacia.")
                .MinimumLength(6).WithMessage("La contraseña debe tener un minimo de 6 caracteres.")
                .MaximumLength(50).WithMessage("La longitud de la contraseña excede al limite establecido.");

            RuleFor(x => x.PasswordConfirmed)
                .NotEmpty().WithMessage("La confirmación de la contraseña no puede estar vacia.")
                .Equal(x => x.Password).WithMessage("La contraseña y la confirmación de contraseña deben ser iguales.");

            RuleFor(x => x.Role)
                .NotNull().WithMessage("Rol de usuario no valido.")
                .NotEmpty().WithMessage("Rol de usuario no valido.");

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(20).WithMessage("La longitud del telefono excede el limite establecido.");


        }



    }
}
