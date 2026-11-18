using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takeaway.BusinessEngine.Utility;
using Takeaway.Data.Entities.Payment;

namespace Takeaway.Service.IntegrationTests.PaymentTests
{
    [TestClass]

    #if !DEBUG
    [Ignore] 
    #endif
    public class PaymentTest
    {
        [TestMethod]
        public void StripeWithTestCards()
        {
            var payment = new PaymentUtil();
            var paymentObj = new Payment
            {
                Amount = 1000,
                Description = "Sample Payment",
                CardNumber = "4242424242424242",
                CardName = "Test CardName",
                Currency = "eur",
                CardCvc = "1223",
                CardExpirationYear = "2021",
                CardExpirationMonth = "10"
            };


            var result=payment.TakePayment(paymentObj);

            Assert.IsNotNull(result);
        }
    }
}
