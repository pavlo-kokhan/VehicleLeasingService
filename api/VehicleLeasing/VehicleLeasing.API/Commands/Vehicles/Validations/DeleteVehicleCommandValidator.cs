using FluentValidation;

namespace VehicleLeasing.API.Commands.Vehicles.Validations;

public class DeleteVehicleCommandValidator : AbstractValidator<DeleteVehicleCommand>
{
    public DeleteVehicleCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty();
    }
}