namespace Takeaway.Data.Entities.Shops
{
    public partial class OrderItemsSelection : BaseDataEntity
    {

        public int? OrderItemId { get; set; }

        public int? OrderItemSelectedId { get; set; }

        public virtual MenuItem OrderItemSelectedItem { get; set; }
        public virtual OrderItem OrderItem { get; set; }

    }
}