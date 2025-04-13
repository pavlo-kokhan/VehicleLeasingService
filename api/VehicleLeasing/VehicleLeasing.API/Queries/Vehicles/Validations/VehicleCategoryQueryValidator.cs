using FluentValidation;

namespace VehicleLeasing.API.Queries.Vehicles.Validations;

public class VehicleCategoryQueryValidator : AbstractValidator<VehicleQuery>
{
    public VehicleCategoryQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}