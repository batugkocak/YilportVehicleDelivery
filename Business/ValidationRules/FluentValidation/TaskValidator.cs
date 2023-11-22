using FluentValidation;

namespace Business.ValidationRules.FluentValidation;

public class TaskValidator : AbstractValidator<Entities.Concrete.Task>
{
    public TaskValidator()
    {
        RuleFor(x => x.Name).MaximumLength(30);
        RuleFor(x => x.Address).MaximumLength(50);
    }
}