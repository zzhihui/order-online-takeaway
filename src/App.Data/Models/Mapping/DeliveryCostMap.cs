using System.Data.Entity.ModelConfiguration;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Models.Mapping
{
    public class DeliveryCostMap : EntityTypeConfiguration<DeliveryCost>
    {
        public DeliveryCostMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

           
            // Table & Column Mappings
            this.ToTable("deliveryCost");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ShopId).HasColumnName("shopId");
            this.Property(t => t.FromDistance).HasColumnName("fromDistance");
            this.Property(t => t.ToDistance).HasColumnName("toDistance");
            this.Property(t => t.DeliveryCostAmount).HasColumnName("deliveryCost");
            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Updated).HasColumnName("updated");
        }
    }
}
