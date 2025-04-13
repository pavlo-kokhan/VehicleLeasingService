using FluentValidation;

namespace VehicleLeasing.API.Commands.LeasingRequests.Validations;

public class CreateLeasingRequestCommandValidator : AbstractValidator<CreateLeasingRequestCommand>
{
    public CreateLeasingRequestCommandValidator()
    {
        RuleFor(x => x.FixedPrice)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);
    }
}