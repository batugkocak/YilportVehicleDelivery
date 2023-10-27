using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation;

//TODO: I'm not sure if I need FluentValidation.
public class VehicleValidator : AbstractValidator<Vehicle>
{
    public VehicleValidator()
    {
        RuleFor(x => x.Plate).NotEmpty();
    }

    private bool BeAValidPostcode(string postcode)
    {
        // custom postcode validating logic goes here
        return true;
    }
}