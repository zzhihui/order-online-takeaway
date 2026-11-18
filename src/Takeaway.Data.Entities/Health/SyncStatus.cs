using System;

namespace Takeaway.Data.Entities.Health
{
    public class SyncStatus : BaseDataEntity
    {
        public DateTime? CheckTime { get; set; }
    }
}