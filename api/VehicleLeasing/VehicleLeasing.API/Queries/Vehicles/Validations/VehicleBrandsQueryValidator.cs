using FluentValidation;

namespace VehicleLeasing.API.Queries.Vehicles.Validations;

public class VehicleBrandsQueryValidator : AbstractValidator<VehicleBrandsQuery>
{
    public VehicleBrandsQueryValidator()
    {
        RuleFor(x => x.Category)
            .NotEmpty();
    }
}