using FluentValidation;
using Takeaway.Service.Contract.Entities.Shop;


namespace Takeaway.Service.Validations
{

    public enum WsOrderValidatorRule
    {
        ExistingOrder,
        NewOrder
    }

    public interface IWsOrderValidator
    {

    }
    public class WsOrderValidator : AbstractValidator<WsOrder>, IWsOrderValidator
    {
        public WsOrderValidator()
        {


            RuleSet(WsOrderValidatorRule.NewOrder.ToString(),
                () =>
                    {
                        RuleFor(p => p.Guid).NotNull();
                    }
                );



            RuleSet(WsOrderValidatorRule.ExistingOrder.ToString(),
                    () => {
                        RuleFor(p => p.Guid).NotNull();
                    });


        }


    }
}