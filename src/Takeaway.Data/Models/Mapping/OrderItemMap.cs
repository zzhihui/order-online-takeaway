using System.Data.Entity.ModelConfiguration;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Models.Mapping
{
    public class OrderItemMap : EntityTypeConfiguration<OrderItem>
    {
        public OrderItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("orderItems");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.OrderId).HasColumnName("orderId");
            this.Property(t => t.MenuItemId).HasColumnName("menuItemId");
            this.Property(t => t.Comment).HasColumnName("comment");
            this.Property(t => t.DiscountPrice).HasColumnName("discountPrice");
            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Updated).HasColumnName("updated");
            this.Ignore(t => t.ShopId);

            // Relationships
            this.HasOptional(t => t.MenuItem)
                .WithMany(t => t.OrderItems)
                .HasForeignKey(d => d.MenuItemId);
            this.HasOptional(t => t.Order)
                .WithMany(t => t.OrderItems)
                .HasForeignKey(d => d.OrderId);

        }
    }
}
