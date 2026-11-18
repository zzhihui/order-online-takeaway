using System.Data.Entity.ModelConfiguration;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Models.Mapping
{
    public class MenuItemCategoryMap : EntityTypeConfiguration<MenuItemCategory>
    {
        public MenuItemCategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(200);

            this.Property(t => t.ImageUrl)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("menuItemCategory");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ShopId).HasColumnName("shopId");
            this.Property(t => t.Name).HasColumnName("name");
            this.Property(t => t.Description).HasColumnName("description");
            this.Property(t => t.ImageUrl).HasColumnName("imageUrl");
            this.Property(t => t.Isactive).HasColumnName("isactive");
            this.Property(t => t.IsInternal).HasColumnName("isInternal");
            this.Property(t => t.CategoryGroupId).HasColumnName("categoryGroupId");
            this.Property(t => t.TranslateName).HasColumnName("translateName");
            this.Property(t => t.SortOrder).HasColumnName("sortOrder");
             
            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Updated).HasColumnName("updated");
        }
    }
}
