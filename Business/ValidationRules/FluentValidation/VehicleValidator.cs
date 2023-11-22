using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation;

public class VehicleValidator : AbstractValidator<Vehicle>
{
    public VehicleValidator()
    {

        RuleFor(x => x.Plate).NotEmpty().Length(5,8);
        RuleFor(x => x.ModelName).NotEmpty().MaximumLength(15);
        RuleFor(x => x.ModelYear).GreaterThan(1950).LessThan(2050).WithMessage("Model yýlý hatalý.");
        RuleFor(x => x.Note).MaximumLength(200);

    }
}