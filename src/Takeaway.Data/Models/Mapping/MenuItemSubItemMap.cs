using System.Data.Entity.ModelConfiguration;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Models.Mapping
{
    public class MenuItemSubItemMap : EntityTypeConfiguration<MenuItemSubItem>
    {
        public MenuItemSubItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("menuItemSubItems");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ShopId).HasColumnName("shopId");
            this.Property(t => t.MenuItemId).HasColumnName("menuItemId");
            this.Property(t => t.SubmenuItemId).HasColumnName("submenuItemId");
            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Updated).HasColumnName("updated");
            this.Property(t => t.SortOrder).HasColumnName("sortOrder");
            this.Property(t => t.GroupId).HasColumnName("groupId");
            this.Property(t => t.OptionItemGroupId).HasColumnName("optionItemGroupId");

            // Relationships
            this.HasOptional(t => t.MenuItem)
                .WithMany(t => t.MenuItemSubItems)
                .HasForeignKey(d => d.MenuItemId);
            this.HasOptional(t => t.MenuSubItem)
                .WithMany(t => t.MenuItemSubItems)
                .HasForeignKey(d => d.SubmenuItemId);

        }
    }
}
