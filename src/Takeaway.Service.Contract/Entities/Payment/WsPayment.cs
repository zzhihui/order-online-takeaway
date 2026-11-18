namespace Takeaway.Service.Contract.Entities.Payment
{
    public class WsPayment: WsEntity
    {
        public decimal Amount { get; set; }
       
        public string Description { get; set; }

        public string CardNumber { get; set; }

        public string CardExpirationYear { get; set; }
        public string CardExpirationMonth { get; set; }
        public string CardName { get; set; }

        public string CardCvc { get; set; }

    }
}