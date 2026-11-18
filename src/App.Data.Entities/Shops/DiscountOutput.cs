namespace Takeaway.Data.Entities.Shops
{
    public class DiscountOutput : BaseDataEntity
    {
        public decimal Amount { get; set; }
        public string DiscountRef { get; set; }
        
    }
}