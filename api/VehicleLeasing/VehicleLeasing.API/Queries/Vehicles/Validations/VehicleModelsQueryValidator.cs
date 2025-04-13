using FluentValidation;

namespace VehicleLeasing.API.Queries.Vehicles.Validations;

public class VehicleModelsQueryValidator : AbstractValidator<VehicleModelsQuery>
{
    public VehicleModelsQueryValidator()
    {
        RuleFor(x => x.Category)
            .NotEmpty();

        RuleFor(x => x.Brand)
            .NotEmpty();
        
    }
}