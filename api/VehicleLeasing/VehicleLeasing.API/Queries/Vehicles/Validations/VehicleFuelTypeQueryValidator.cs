using FluentValidation;

namespace VehicleLeasing.API.Queries.Vehicles.Validations;

public class VehicleFuelTypeQueryValidator : AbstractValidator<VehicleFuelTypeQuery>
{
    public VehicleFuelTypeQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}