using FluentValidation;
using Takeaway.Service.Contract.Entities.Shop;



namespace Takeaway.Service.Validations
{

    public enum WsMenuItemValidatorRule
    {
        IdOnly
    }



    public class WsMenuItemValidator : AbstractValidator<WsMenuItemCategory>
    {
        public WsMenuItemValidator()
        {
            RuleSet(WsMenuItemValidatorRule.IdOnly.ToString(), 
                    () => RuleFor(p => p.Id).GreaterThan(0).WithMessage("Category Id Greater than Zero")
                    );
        }


    }
}