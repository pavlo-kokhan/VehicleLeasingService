using FluentValidation;

namespace VehicleLeasing.API.Queries.Vehicles.Validations;

public class VehicleTransmissionQueryValidator : AbstractValidator<VehicleTransmissionQuery>
{
    public VehicleTransmissionQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}