using System.Data.Entity.ModelConfiguration;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Models.Mapping
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ContactName)
                .HasMaxLength(50);

            this.Property(t => t.PhoneNumber)
                .HasMaxLength(50);

            this.Property(t => t.Address)
                .HasMaxLength(200);

            this.Property(t => t.Comment)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("order");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ShopId).HasColumnName("shopId");
            this.Property(t => t.ContactName).HasColumnName("contactName");
            this.Property(t => t.PhoneNumber).HasColumnName("phoneNumber");
            this.Property(t => t.Address).HasColumnName("address");
            this.Property(t => t.Comment).HasColumnName("comment");
            this.Property(t => t.Email).HasColumnName("email");
            this.Property(t => t.DiscountRules).HasColumnName("discountRules");
            this.Property(t => t.DeliveryOptionId).HasColumnName("deliveryOptionId");
            this.Property(t => t.CustomerId).HasColumnName("customerId");
            this.Property(t => t.OrderTotal).HasColumnName("orderTotal");
            this.Property(t => t.OrderOriginalTotal).HasColumnName("orderOriginalTotal");
            this.Property(t => t.StatusId).HasColumnName("statusId");
            this.Property(t => t.IsInternalOrder).HasColumnName("isInternalOrder");
            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Updated).HasColumnName("updated");
            this.Property(t => t.DeliveryCharge).HasColumnName("deliveryCharge");
            this.Property(t => t.PaymentRef).HasColumnName("paymentRef");
            this.Property(t => t.OrderRef).HasColumnName("orderRef");
            this.Property(t => t.PaymentType).HasColumnName("paymentType");
            this.Property(t => t.DriverName).HasColumnName("driverName");
            this.Property(t => t.AgentId).HasColumnName("agentId");
            this.Property(t => t.IsWalkIn).HasColumnName("iswalkin");
            this.Property(t => t.IsMobileOrder).HasColumnName("ismobileOrder");
            this.Property(t => t.TableNumber).HasColumnName("tableNumber");
            this.Property(t => t.CancellationReason).HasColumnName("cancellationReason");
            this.Property(t => t.NumberOfPeople).HasColumnName("numberOfPeople");
            this.Property(t => t.OrderSource).HasColumnName("orderSource");
            this.Property(t => t.MarketingPermission).HasColumnName("marketingPermission");
            this.Property(t => t.CollectionTime).HasColumnName("collectionTime");

            this.Property(t => t.Guid).HasColumnName("guid");
            this.Ignore(t => t.RewardPoints);


            // Relationships
            this.HasOptional(t => t.DeliveryOption)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.DeliveryOptionId);

        }
    }
}

