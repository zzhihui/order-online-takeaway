using System.Data.Entity.ModelConfiguration;
using Takeaway.Admin.Util.Entities.Shops;

namespace Takeaway.Admin.Util.Models.Mapping
{
    public class DeliveryAreanMap : EntityTypeConfiguration<DeliveryArea>
    {
        public DeliveryAreanMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

           
            // Table & Column Mappings
            this.ToTable("WsDeliveryArea");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ShopId).HasColumnName("shopId");
            this.Property(t => t.Json).HasColumnName("json");
            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Updated).HasColumnName("updated");
        }
    }
}
