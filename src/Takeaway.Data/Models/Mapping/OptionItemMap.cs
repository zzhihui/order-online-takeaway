using System.Data.Entity.ModelConfiguration;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Models.Mapping
{
    public class OptionItemMap : EntityTypeConfiguration<OptionItem>
    {
        public OptionItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ItemName)
                .HasMaxLength(50);

            
            this.Property(t => t.TranslateName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("optionItem");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ShopId).HasColumnName("shopId");
            this.Property(t => t.ItemName).HasColumnName("itemName");
            this.Property(t => t.ItemDescription).HasColumnName("itemDescription");
            this.Property(t => t.UnitPrice).HasColumnName("unitPrice");
            this.Property(t => t.TranslateName).HasColumnName("translateName");
            this.Property(t => t.Isactive).HasColumnName("isactive");
            this.Property(t => t.SortOrder).HasColumnName("sortOrder");
            this.Property(t => t.GroupId).HasColumnName("groupId");
            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Updated).HasColumnName("updated");
            
        }
    }
}
