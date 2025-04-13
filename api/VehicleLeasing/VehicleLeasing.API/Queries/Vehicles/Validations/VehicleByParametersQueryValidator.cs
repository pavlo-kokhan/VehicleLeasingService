using FluentValidation;

namespace VehicleLeasing.API.Queries.Vehicles.Validations;

public class VehicleByParametersQueryValidator : AbstractValidator<VehicleLeasingCalculatorQuery>
{
    public VehicleByParametersQueryValidator()
    {
        RuleFor(x => x.Category)
            .NotEmpty();
        
        RuleFor(x => x.Brand)
            .NotEmpty();
        
        RuleFor(x => x.Model)
            .NotEmpty();
        
        RuleFor(x => x.Category)
            .NotEmpty();
        
        RuleFor(x => x.LeasingMonths)
            .NotEmpty()
            .GreaterThanOrEqualTo(12)
            .LessThanOrEqualTo(60);

        RuleFor(x => x.AdvancePercentage)
            .NotEmpty()
            .GreaterThanOrEqualTo(20)
            .LessThanOrEqualTo(70);
    }
}