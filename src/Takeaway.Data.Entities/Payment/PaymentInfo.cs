namespace Takeaway.Data.Entities.Payment
{
    public class PaymentInfo : BaseDataEntity
    {
        public bool IsSuccess { get; set; }

        public string PaymentRef { get; set; }
        public int? CostCredit { get; set; }
        public string ErrorMessage { get; set; }


    }
}