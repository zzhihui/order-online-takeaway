using System.Data.Entity.ModelConfiguration;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Models.Mapping
{
    public class MenuSubItemItemMap : EntityTypeConfiguration<MenuSubItemItem>
    {
        public MenuSubItemItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("menuSubItemItems");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ShopId).HasColumnName("shopId");
            this.Property(t => t.SubItemId).HasColumnName("subItemId");
            this.Property(t => t.MenuItemId).HasColumnName("menuItemId");
            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Updated).HasColumnName("updated");
            this.Property(t => t.Cost).HasColumnName("cost");
            

            // Relationships
            this.HasOptional(t => t.MenuItem)
                .WithMany(t => t.MenuSubItemItems)
                .HasForeignKey(d => d.MenuItemId);
            this.HasOptional(t => t.MenuSubItem)
                .WithMany(t => t.MenuSubItemItems)
                .HasForeignKey(d => d.SubItemId);

        }
    }
}
