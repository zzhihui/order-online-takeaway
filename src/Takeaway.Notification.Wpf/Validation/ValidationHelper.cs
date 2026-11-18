using FluentValidation;
using FluentValidation.Results;

namespace Takeaway.Admin.Wpf.Validation
{


    public interface IValidationHelper
    {
        ValidationResult Validate<T, TK>(TK entity)
            where T : IValidator<TK>, new()
            where TK : class;

    }
    public class ValidationHelper : IValidationHelper
    {
        public ValidationResult Validate<T, TK>(TK entity)
            where T : IValidator<TK>, new()
            where TK : class
        {
            IValidator<TK> validator = new T();
            return validator.Validate(entity);
        }
    }
}