using System.Text;

namespace Core.Common.Utility.Extension
{
    public static class FluentValidationExtension
    {
        public static string ToErrorMessage(this FluentValidation.Results.ValidationResult source)
        {
            var builder = new StringBuilder();

            foreach (var error in source.Errors)
            {
                builder.AppendLine(error.ErrorMessage);
            }

            return builder.ToString();
        }
    }
}