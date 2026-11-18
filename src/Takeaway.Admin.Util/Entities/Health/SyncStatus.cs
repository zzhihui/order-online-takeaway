using System;

namespace Takeaway.Admin.Util.Entities.Health
{
    public class SyncStatus : BaseDataEntity
    {
        public DateTime? CheckTime { get; set; }
    }
}