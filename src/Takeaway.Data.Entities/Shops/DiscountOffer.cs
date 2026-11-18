using System.Collections.Generic;

namespace Takeaway.Data.Entities.Shops
{
    public partial class DiscountOffer:BaseDataEntity
    {
        public DiscountOffer()
        {
            this.MemuItemOffers = new List<MemuItemOffer>();
        }

        
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? DiscountValue { get; set; }
        public bool? Isactive { get; set; }
        
        public virtual ICollection<MemuItemOffer> MemuItemOffers { get; set; }
    }
}
