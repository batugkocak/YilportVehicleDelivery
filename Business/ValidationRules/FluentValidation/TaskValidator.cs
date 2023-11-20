using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation;

public class TaskValidator : AbstractValidator<Entities.Concrete.Task>
{
    public TaskValidator()
    {
      


    }
}