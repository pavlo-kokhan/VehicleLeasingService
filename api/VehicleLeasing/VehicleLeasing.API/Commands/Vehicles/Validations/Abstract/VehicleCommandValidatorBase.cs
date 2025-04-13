using FluentValidation;
using VehicleLeasing.API.Commands.Vehicles.Abstract;

namespace VehicleLeasing.API.Commands.Vehicles.Validations.Abstract;

public class VehicleCommandValidatorBase<T> : AbstractValidator<T> where T : IVehicleCommand
{
    protected VehicleCommandValidatorBase()
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