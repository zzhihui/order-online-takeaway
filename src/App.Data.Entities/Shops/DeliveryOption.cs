using System;
using System.Collections.Generic;

namespace Takeaway.Data.Entities.Shops
{
    public class DeliveryOption : BaseDataEntity
    {
        public DeliveryOption()
        {
            this.Orders = new List<Order>();
        }

        public bool? IsInternal { get; set; }

        public string Name { get; set; }
        
        public virtual ICollection<Order> Orders { get; set; }
       
    }
}