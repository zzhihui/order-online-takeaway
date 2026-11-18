using System;

namespace Takeaway.Admin.Util.Entities
{
    public class BaseDataEntity : Entity, ICloneable
    {
        public int Id { get; set; }
        public int ShopId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}