using FluentValidation;
using FluentValidation.Internal;

namespace Core.Common.Utility.Extension
{
    public static class ValidatorExtensions
    {
        public static void ValidateAndThrow<T>(this IValidator validator, T instance, string ruleSet)
         where T : class 
        {
            var result = string.IsNullOrEmpty(ruleSet)
                ? validator.Validate(instance)
                : validator.Validate(new ValidationContext(
                    instance,
                    new PropertyChain(),
                    new RulesetValidatorSelector(ruleSet)));

            if (!result.IsValid)
                throw new ValidationException(result.Errors);
        } 
    }
}