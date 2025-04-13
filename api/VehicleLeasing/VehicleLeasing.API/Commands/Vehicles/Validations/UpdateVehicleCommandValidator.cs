using FluentValidation;
using VehicleLeasing.API.Commands.Vehicles.Validations.Abstract;

namespace VehicleLeasing.API.Commands.Vehicles.Validations;

public class UpdateVehicleCommandValidator : VehicleCommandValidatorBase<UpdateVehicleCommand>
{
    public UpdateVehicleCommandValidator()
    {
        RuleFor(v => v.Brand)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(v => v.Model)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(v => v.Year)
            .NotNull()
            .GreaterThan(1900)
            .LessThan(DateTime.Now.Year + 1);
        
        RuleFor(v => v.EstimatedPrice)
            .NotNull()
            .GreaterThan(0);
    }
}