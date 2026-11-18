using System;

namespace Takeaway.Admin.Util.Entities.Shops
{
    public class Tables : BaseDataEntity
    {
        public int TableId { get; set; }
        public string Json { get; set; }
    }
}