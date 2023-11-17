using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation;

public class VehicleValidator : AbstractValidator<Vehicle>
{
    public VehicleValidator()
    {
        RuleFor(x => x.Plate).NotEmpty().Length(5,8);
        RuleFor(x => x.Type).NotEmpty().GreaterThanOrEqualTo(0).LessThanOrEqualTo((int)VehicleType.Diðer);
        RuleFor(x => x.FuelType).NotEmpty().GreaterThanOrEqualTo(0).LessThanOrEqualTo((int)FuelType.Diðer);
        RuleFor(x => x.Status).NotEmpty().GreaterThanOrEqualTo(0).LessThanOrEqualTo(2);

        RuleFor(x => x.ModelName).NotEmpty().Length(2, 15);
        RuleFor(x => x.Note).MaximumLength(200);
        RuleFor(x => x.ModelYear).GreaterThan(1950).LessThan(2050).WithMessage("Model yýlý hatalý.");


    }
}