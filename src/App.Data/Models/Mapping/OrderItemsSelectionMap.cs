using System.Data.Entity.ModelConfiguration;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Models.Mapping
{
    public class OrderItemsSelectionMap : EntityTypeConfiguration<OrderItemsSelection>
    {
        public OrderItemsSelectionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("orderItemsSelection");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ShopId).HasColumnName("shopId");
            this.Property(t => t.OrderItemId).HasColumnName("orderItemId");
            this.Property(t => t.OrderItemSelectedId).HasColumnName("orderItemSelectedId");
            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Updated).HasColumnName("updated");
            
            // Relationships
            this.HasOptional(t => t.OrderItem)
                .WithMany(t => t.OrderItemsSelections)
                .HasForeignKey(d => d.OrderItemId);


            this.HasOptional(t => t.OrderItemSelectedItem)
                .WithMany(t => t.OrderItemsSelections)
                .HasForeignKey(d => d.OrderItemSelectedId);

        }
    }
}
