using System.Data.Entity.ModelConfiguration;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Models.Mapping
{
    public class ShopSpecialHourMap : EntityTypeConfiguration<ShopSpecialHour>
    {
        public ShopSpecialHourMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("shopSpecialHour");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ShopId).HasColumnName("shopId");
            this.Property(t => t.DateOfYear).HasColumnName("dateOfYear");
            this.Property(t => t.StartingTime).HasColumnName("startingTime");
            this.Property(t => t.EndingTime).HasColumnName("endingTime");
            this.Property(t => t.Isactive).HasColumnName("isactive");
            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Updated).HasColumnName("updated");
        }
    }
}
