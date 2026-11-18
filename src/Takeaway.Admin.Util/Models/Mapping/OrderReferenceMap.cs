using System.Data.Entity.ModelConfiguration;
using Takeaway.Admin.Util.Entities.Shops;

namespace Takeaway.Admin.Util.Models.Mapping
{
    public class OrderReferenceMap : EntityTypeConfiguration<OrderReference>
    {
        public OrderReferenceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);
            
            // Table & Column Mappings
            this.ToTable("orderRef");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ShopId).HasColumnName("shopId");
            this.Property(t => t.OrderType).HasColumnName("orderType");
            this.Property(t => t.OrderSequence).HasColumnName("orderSequence");
            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Updated).HasColumnName("updated");
        }
    }
}