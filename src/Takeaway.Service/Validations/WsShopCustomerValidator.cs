using FluentValidation;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Service.Validations
{

    public interface IWsShopCustomerValidator
    {
        
    }

    public class WsShopCustomerValidator: AbstractValidator<WsShopCustomer>, IWsShopCustomerValidator
    {
        public WsShopCustomerValidator()
        {
            RuleFor(p => p.Email).NotNull().WithMessage("Email cannot be null");
            RuleFor(p => p.Password).NotNull().WithMessage("password cannot be null");
            RuleFor(p => p.Password.Length).GreaterThan(6).WithMessage("Password must be more than 6 chars").When(r => !string.IsNullOrEmpty(r.Password));
        }
    }
}