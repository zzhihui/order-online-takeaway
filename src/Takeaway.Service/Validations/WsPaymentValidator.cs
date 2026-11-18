using FluentValidation;
using Takeaway.Service.Contract.Entities.Authentication;
using Takeaway.Service.Contract.Entities.Payment;

namespace Takeaway.Service.Validations
{

    public interface IWsPaymentValidator
    {

    }

    public class WsPaymentValidator : AbstractValidator<WsPayment>, IWsPaymentValidator
    {
        public WsPaymentValidator()
        {
            RuleFor(p => p.CardNumber).NotNull().NotEmpty().WithMessage("Cardnumber cannot be null");
            RuleFor(p => p.Amount).GreaterThan(0).WithMessage("Amount needs to be greater than 0");
            RuleFor(p => p.CardExpirationMonth).NotEmpty().WithMessage("Invalid Card expire month");
            RuleFor(p => p.CardExpirationYear).NotEmpty().WithMessage("Invalid Card expire year");
            RuleFor(p => p.CardCvc).NotEmpty().WithMessage("Invalid Card CVC");
            RuleFor(p => p.CardCvc).Matches(@"^\d+$").WithMessage("Invalid Card CVC");
        }
         
    }
}