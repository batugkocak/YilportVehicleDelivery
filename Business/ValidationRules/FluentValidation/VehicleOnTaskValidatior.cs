using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation;

public class VehicleOnTaskValidatior : AbstractValidator<VehicleOnTask>
{
    public VehicleOnTaskValidatior()
    {
        RuleFor(x => x.TaskDefinition).NotEmpty().Length(4, 30).WithMessage("Görev adýnda bir sorun var.");
        RuleFor(x => x.Address).NotEmpty().MaximumLength(50);
        RuleFor(x => x.AuthorizedPerson).MaximumLength(30);
    }
}