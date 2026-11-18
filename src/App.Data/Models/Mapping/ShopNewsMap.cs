using System.Data.Entity.ModelConfiguration;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Models.Mapping
{
    public class ShopNewsMap : EntityTypeConfiguration<ShopNews>
    {
        public ShopNewsMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.News)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("shopNews");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ShopId).HasColumnName("shopId");
            this.Property(t => t.News).HasColumnName("news");
            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Updated).HasColumnName("updated");
        }
    }
}
