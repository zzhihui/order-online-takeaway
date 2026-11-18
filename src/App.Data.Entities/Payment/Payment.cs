using System;

namespace Takeaway.Data.Entities.Payment
{
    public class Payment : BaseDataEntity
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }

        public string Description { get; set; }

        public string CardNumber { get; set; }

        public string CardExpirationYear { get; set; }
        public string CardExpirationMonth { get; set; }
        public string CardName { get; set; }

        public string CardCvc { get; set; }




    }
}