using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class OwnerValidator : AbstractValidator<Owner>
    {
        public OwnerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(25);
        }
    }
}
