using System;

namespace Takeaway.Data.Entities.Shops
{
    public partial class MemuItemOffer : BaseDataEntity
    {

        public int? MenuItemId { get; set; }
        public int? DiscountPromotionId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual DiscountOffer DiscountOffer { get; set; }
        public virtual MenuItem MenuItem { get; set; }
    }
}
