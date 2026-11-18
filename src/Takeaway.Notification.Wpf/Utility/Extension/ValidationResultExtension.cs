
using System;
using System.Text;
using FluentValidation.Results;
namespace Takeaway.Admin.Wpf.Utility.Extension
{
    public static class ValidationResultExtension
    {

        public static string ToErrorString(this ValidationResult source)
        {
            var validationErrors = new StringBuilder();
            foreach (var validationFailure in source.Errors)
            {
                validationErrors.Append(validationFailure.ErrorMessage);
                validationErrors.Append(Environment.NewLine);
            }
            return validationErrors.ToString();
        }

    }
}