using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation;

public class VehicleOnTaskValidatior : AbstractValidator<VehicleOnTask>
{
    public VehicleOnTaskValidatior()
    {
        RuleFor(x => x.Address).NotEmpty().Length(5,8);
       


    }
}