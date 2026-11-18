
using Core.Common.Resources;
using FluentValidation;
using Takeaway.Admin.Wpf.ViewModels;

namespace Takeaway.Admin.Wpf.Validation
{
    public class LoginViewModelValidator:AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(p => p.UserPin).NotNull().WithMessage(StringResource.LoginViewModelValidator_PinNull);
            RuleFor(p => p.UserPin).NotEmpty().WithMessage(StringResource.LoginViewModelValidator_PinEmpty);
            RuleFor(p => p.UserPin.Length)
                .GreaterThan(3)
                .WithMessage(StringResource.LoginViewModelValidator_PinLength)
                .When(p => !string.IsNullOrEmpty(p.UserPin));
        }
    }
}