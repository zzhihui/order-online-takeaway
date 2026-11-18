using System;

namespace Takeaway.Data.Entities.Shops
{
    public partial class ShopSpecialHour:BaseDataEntity
    {
        
        public DateTime? DateOfYear { get; set; }
        public DateTime? StartingTime { get; set; }
        public DateTime? EndingTime { get; set; }
        public bool? Isactive { get; set; }
        
    }
}
