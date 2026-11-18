namespace Takeaway.Admin.Util.Entities.Shops
{
    public class OrderReference: BaseDataEntity
    {
        public int OrderType { get; set; }
        public int OrderSequence { get; set; }
    }
}