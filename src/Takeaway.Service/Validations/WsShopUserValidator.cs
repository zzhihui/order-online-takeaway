using FluentValidation;
using Takeaway.Service.Contract.Entities.Authentication;

namespace Takeaway.Service.Validations
{
    public interface IWsShopUserValidator
    {

    }

    public enum WsShopUserValidatorRuleSet
    {
        UserNameOnly,
        ExistingUser
    }


    public class WsShopUserValidator : AbstractValidator<WsShopUser>, IWsShopUserValidator
    {
        public WsShopUserValidator()
        {
            RuleFor(p => p.UserName).NotNull().WithMessage("Username cannot be null");
            RuleFor(p => p.Password).NotNull().WithMessage("password cannot be null");
            RuleFor(p => p.UserName).NotEmpty().WithMessage("Username cannot be empty");
            RuleFor(p => p.UserName).NotEqual(p => p.Password).WithMessage("Username and password cannot be the same");
            RuleFor(p => p.Password.Length).GreaterThan(6).WithMessage("Password must be more than 6 chars").When(r=>!string.IsNullOrEmpty(r.Password));


            RuleSet(WsShopUserValidatorRuleSet.UserNameOnly.ToString(), () =>
            {
                RuleFor(p => p.UserName).NotNull().WithMessage("Username cannot be null");
                RuleFor(p => p.UserName).NotEmpty().WithMessage("Username cannot be empty");
                
            });


            RuleSet(WsShopUserValidatorRuleSet.ExistingUser.ToString(), () =>
            {
                RuleFor(p => p.Id).NotNull().WithMessage("UserID cannot be null");
                RuleFor(p => p.UserName).NotNull().WithMessage("Username cannot be null");
                RuleFor(p => p.Password).NotNull().WithMessage("password cannot be null");
                RuleFor(p => p.UserName).NotEmpty().WithMessage("Username cannot be empty");
                RuleFor(p => p.UserName).NotEqual(p => p.Password).WithMessage("Username and password cannot be the same");
                RuleFor(p => p.Password.Length).GreaterThan(6).WithMessage("Password must be more than 6 chars").When(r => !string.IsNullOrEmpty(r.Password));

            });



        }
    }
}