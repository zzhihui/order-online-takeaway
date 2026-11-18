using System;

namespace Takeaway.Data.Entities.Shops
{
    public partial class ShopGeneralHour:BaseDataEntity
    {
        
        public string DateOfWeek { get; set; }
        public DateTime? StartingTime { get; set; }
        public DateTime? EndingTime { get; set; }
        
    }
}
