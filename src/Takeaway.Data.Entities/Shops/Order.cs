using System;
using System.Collections.Generic;

namespace Takeaway.Data.Entities.Shops
{
    public class Order : BaseDataEntity
    {
        public Order()
        {
            this.OrderItems = new List<OrderItem>();
        }

        public decimal OrderTotal { get; set; }

        public decimal OrderOriginalTotal { get; set; }

        public string Email { get; set; }

        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public string DiscountRules { get; set; }

        public string Comment { get; set; }
        public int? DeliveryOptionId { get; set; }

        public int? CustomerId { get; set; }

        public int? StatusId { get; set; }

        public bool? IsInternalOrder { get; set; }

        public string PaymentRef { get; set; }

        public decimal? DeliveryCharge { get; set; }

        public string OrderRef { get; set; }

        public string PaymentType { get; set; }
        public string DriverName { get; set; }

        public bool? IsWalkIn { get; set; }

        public bool? IsMobileOrder { get; set; }
        public int? TableNumber { get; set; }
        public int? NumberOfPeople { get; set; }

        public string CancellationReason { get; set; }

        public int AgentId { get; set; }

        public string Guid { get; set; }

        public int? OrderSource { get; set; }

        public int? MarketingPermission { get; set; }
        public int? RewardPoints { get; set; }

        public string CollectionTime { get; set; }

        public virtual DeliveryOption DeliveryOption { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}