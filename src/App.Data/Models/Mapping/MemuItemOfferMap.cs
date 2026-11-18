using System.Data.Entity.ModelConfiguration;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Models.Mapping
{
    public class MemuItemOfferMap : EntityTypeConfiguration<MemuItemOffer>
    {
        public MemuItemOfferMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("memuItemOffer");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ShopId).HasColumnName("shopId");
            this.Property(t => t.MenuItemId).HasColumnName("menuItemId");
            this.Property(t => t.DiscountPromotionId).HasColumnName("discountPromotionId");
            this.Property(t => t.FromDate).HasColumnName("fromDate");
            this.Property(t => t.EndDate).HasColumnName("endDate");
            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Updated).HasColumnName("updated");

            // Relationships
            this.HasOptional(t => t.DiscountOffer)
                .WithMany(t => t.MemuItemOffers)
                .HasForeignKey(d => d.DiscountPromotionId);
            this.HasOptional(t => t.MenuItem)
                .WithMany(t => t.MemuItemOffers)
                .HasForeignKey(d => d.MenuItemId);

        }
    }
}
