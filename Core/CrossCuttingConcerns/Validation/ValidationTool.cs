using FluentValidation;

namespace Core.CrossCuttingConcerns.Validation;

public static class ValidationTool
{
 
 public static void Validate(IValidator validator, object entity)
 {
  var validatorContext = new ValidationContext<object>(entity);
  var validatorResult = validator.Validate(validatorContext);
  
  if (!validatorResult.IsValid)
  {
   throw new ValidationException(validatorResult.Errors);
  }
  
 }
 
}