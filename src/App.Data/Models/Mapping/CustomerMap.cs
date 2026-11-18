using System.Data.Entity.ModelConfiguration;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Models.Mapping
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.FullName)
                .HasMaxLength(50);

            this.Property(t => t.Username)
                .HasMaxLength(50);

            this.Property(t => t.Address1)
                .HasMaxLength(200);

            this.Property(t => t.Address2)
                .HasMaxLength(200);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            this.Property(t => t.Phone1)
                .HasMaxLength(50);

            this.Property(t => t.Phone2)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("customer");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ShopId).HasColumnName("shopId");
            this.Property(t => t.FullName).HasColumnName("fullName");
            this.Property(t => t.DeliveryCharge).HasColumnName("deliveryCharge");
            this.Property(t => t.Username).HasColumnName("username");
            this.Property(t => t.Address1).HasColumnName("address1");
            this.Property(t => t.Address2).HasColumnName("address2");
            this.Property(t => t.Email).HasColumnName("email");
            this.Property(t => t.Phone1).HasColumnName("phone1");
            this.Property(t => t.Phone2).HasColumnName("phone2");
            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Updated).HasColumnName("updated");
        }
    }
}
