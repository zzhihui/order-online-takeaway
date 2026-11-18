using System.Data.Entity.ModelConfiguration;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Models.Mapping
{
    public class DeliveryAreanMap : EntityTypeConfiguration<DeliveryArea>
    {
        public DeliveryAreanMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

           
            // Table & Column Mappings
            this.ToTable("deliveryArea");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ShopId).HasColumnName("shopId");
            this.Property(t => t.Area).HasColumnName("area");
            this.Property(t => t.MinOrder).HasColumnName("minOrder");
            this.Property(t => t.DeliveryCost).HasColumnName("deliveryCost");
            this.Property(t => t.DeliveryGroup).HasColumnName("deliveryGroup");
            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Updated).HasColumnName("updated");
        }
    }
}
