namespace Takeaway.Data.Entities.Shops
{
    public class DeliveryArea : BaseDataEntity
    {
        public string Area { get; set; }
        public decimal? MinOrder { get; set; }
        public decimal? DeliveryCost { get; set; }
        public int? DeliveryGroup { get; set; }
    }
}