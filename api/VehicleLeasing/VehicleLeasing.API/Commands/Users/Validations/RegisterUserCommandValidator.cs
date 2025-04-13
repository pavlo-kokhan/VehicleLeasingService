using FluentValidation;

namespace VehicleLeasing.API.Commands.Users.Validations;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty();

        RuleFor(u => u.Surname)
            .NotEmpty();

        RuleFor(u => u.Email)
            .NotEmpty();

        RuleFor(u => u.Password)
            .NotEmpty()
            .Length(4);
    }
}