using FluentValidation;

namespace VehicleLeasing.API.Queries.Vehicles.Validations;

public class VehicleStatusQueryValidator : AbstractValidator<VehicleStatusQuery>
{
    public VehicleStatusQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}