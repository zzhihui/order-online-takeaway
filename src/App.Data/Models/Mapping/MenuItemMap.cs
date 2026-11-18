using System.Data.Entity.ModelConfiguration;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Models.Mapping
{
    public class MenuItemMap : EntityTypeConfiguration<MenuItem>
    {
        public MenuItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ItemName)
                .HasMaxLength(50);

            this.Property(t => t.ItemPic)
                .HasMaxLength(200);

            this.Property(t => t.VideoLink)
                .HasMaxLength(200);

            this.Property(t => t.TranslateName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("menuItem");
            this.Property(t => t.ShopId).HasColumnName("shopId");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ItemName).HasColumnName("itemName");
            this.Property(t => t.ItemDescription).HasColumnName("itemDescription");
            this.Property(t => t.UnitPrice).HasColumnName("unitPrice");
            this.Property(t => t.ItemPic).HasColumnName("itemPic");
            this.Property(t => t.VideoLink).HasColumnName("videoLink");
            this.Property(t => t.MenuItemCategoryId).HasColumnName("menuItemCategoryId");
            this.Property(t => t.TranslateName).HasColumnName("translateName");
            this.Property(t => t.Isactive).HasColumnName("isactive");
            this.Property(t => t.Ishot).HasColumnName("isHot");
            this.Property(t => t.HotGroupId).HasColumnName("hotGroupId");
            this.Property(t => t.SortOrder).HasColumnName("sortOrder");

            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Updated).HasColumnName("updated");
            this.Property(t => t.NoSelection).HasColumnName("noSelection");
            this.Property(t => t.FoodCategoryId).HasColumnName("foodCategoryId");
            
            this.Ignore(t => t.HasSubOrder);

            // Relationships
            this.HasOptional(t => t.MenuItemCategory)
                .WithMany(t => t.MenuItems)
                .HasForeignKey(d => d.MenuItemCategoryId);

        }
    }
}
