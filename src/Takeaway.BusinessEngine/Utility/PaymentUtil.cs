using System;
using System.Globalization;
using Stripe;
using Takeaway.Data.Entities.Payment;

namespace Takeaway.BusinessEngine.Utility
{
    public interface IPaymentUtil
    {
        PaymentInfo TakePayment(Payment paymentInfo);
    }


    public class PaymentUtil : IPaymentUtil
    {
        public PaymentInfo TakePayment(Payment paymentInfo)
        {
            var paymentResult = new PaymentInfo();

            try
            {
                var tokenId = GetTokenId(paymentInfo);


                var myCharge = new StripeChargeCreateOptions
                {
                    Amount = Convert.ToInt32(paymentInfo.Amount * 100),
                    Currency = paymentInfo.Currency,
                    Description = paymentInfo.Description,
                    SourceTokenOrExistingSourceId = tokenId
                };

                var chargeService = new StripeChargeService(paymentInfo.ApiKey);
                var stripeCharge = chargeService.Create(myCharge);
                paymentResult.IsSuccess = true;
                paymentResult.PaymentRef = stripeCharge.Id;
            }
            catch (StripeException ex)
            {
                paymentResult.IsSuccess = false;
                paymentResult.ErrorMessage = ex.Message;
            }
            catch (System.Exception ex)
            {
                paymentResult.IsSuccess = false;
                paymentResult.ErrorMessage = ex.Message;
            }

            return paymentResult;
        }

        private string GetTokenId(Payment paymentInfo)
        {
            var myToken = new StripeTokenCreateOptions
            {
                Card = new StripeCreditCardOptions()
                {
                    Cvc = paymentInfo.CardCvc.ToString(CultureInfo.CurrentCulture),
                    ExpirationMonth = paymentInfo.CardExpirationMonth,
                    ExpirationYear = paymentInfo.CardExpirationYear,
                    Name = paymentInfo.CardName,
                    Number = paymentInfo.CardNumber
                }
            };

            var tokenService = new StripeTokenService(paymentInfo.ApiKey);
            var stripeToken = tokenService.Create(myToken);

            return stripeToken.Id;

        }
    }
}