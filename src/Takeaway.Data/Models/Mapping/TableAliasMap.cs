using System.Data.Entity.ModelConfiguration;
using Takeaway.Data.Entities.Authentication;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Models.Mapping
{
    public class TableAliasMap : EntityTypeConfiguration<TableAlias>
    {
        public TableAliasMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);
            
            // Table & Column Mappings
            this.ToTable("tableAlias");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ShopId).HasColumnName("shopId");
            this.Property(t => t.ItemName).HasColumnName("itemName");
            this.Property(t => t.ItemDescription).HasColumnName("description");
            this.Property(t => t.TranslateName).HasColumnName("translateName");
            this.Property(t => t.Isactive).HasColumnName("isactive");
            this.Property(t => t.SortOrder).HasColumnName("sortOrder");
            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Updated).HasColumnName("updated");
        }
    }
}