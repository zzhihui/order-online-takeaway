using System.Collections.Generic;

namespace Takeaway.Data.Entities.Shops
{
    public partial class OrderItem : BaseDataEntity
    {

        public OrderItem ()
        {
            OrderItemsSelections= new List<OrderItemsSelection>();
        }


        public int? OrderId { get; set; }
        public int? MenuItemId { get; set; }

        public virtual MenuItem MenuItem { get; set; }
        public virtual Order Order { get; set; }

        public string Comment { get; set; }

        public decimal? DiscountPrice { get; set; }

        public virtual ICollection<OrderItemsSelection> OrderItemsSelections { get; set; }

    }
}
