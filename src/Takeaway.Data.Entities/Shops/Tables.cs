using System;

namespace Takeaway.Data.Entities.Shops
{
    public class Tables : BaseDataEntity
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public int NumberOfPeople { get; set; }

        public DateTime? Started { get; set; }

        public Order Order { get; set; }
    }
}