namespace Takeaway.Data.Entities.Shops
{
    public class DeliveryCost : BaseDataEntity
    {
        public decimal? FromDistance { get; set; }
        public decimal? ToDistance { get; set; }

        public decimal? DeliveryCostAmount { get; set; }
    }
}