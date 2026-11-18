using System.Data.Entity.ModelConfiguration;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Models.Mapping
{
    public class ShopGeneralHourMap : EntityTypeConfiguration<ShopGeneralHour>
    {
        public ShopGeneralHourMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.DateOfWeek)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("shopGeneralHour");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ShopId).HasColumnName("shopId");
            this.Property(t => t.DateOfWeek).HasColumnName("dateOfWeek");
            this.Property(t => t.StartingTime).HasColumnName("startingTime");
            this.Property(t => t.DeliveryStartingTime).HasColumnName("deliveryStartingTime");
            this.Property(t => t.EndingTime).HasColumnName("endingTime");
            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Updated).HasColumnName("updated");
        }
    }
}
