using System.Data.Entity.ModelConfiguration;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Models.Mapping
{
    public class ShopInfoMap : EntityTypeConfiguration<ShopInfo>
    {
        public ShopInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id); 

            
            // Table & Column Mappings
            this.ToTable("shopInfo");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ShopId).HasColumnName("shopId");
            this.Property(t => t.Name).HasColumnName("name");
            this.Property(t => t.Description).HasColumnName("description");
            this.Property(t => t.DeliveryCharge).HasColumnName("deliveryCharge");
            this.Property(t => t.ShopMinOrder).HasColumnName("shopMinOrder");
            this.Property(t => t.ShopNumber).HasColumnName("shopNumber");
            this.Property(t => t.ShopMobile).HasColumnName("shopMobile");
            this.Property(t => t.ShopWeb).HasColumnName("shopWeb");
            this.Property(t => t.ShopImage).HasColumnName("shopImage");
            this.Property(t => t.Address1).HasColumnName("address1");
            this.Property(t => t.Address2).HasColumnName("address2");
            this.Property(t => t.SpecialOffer).HasColumnName("specialOffer");
            this.Property(t => t.ShopOpenHours).HasColumnName("shopOpenHours");
            this.Property(t => t.TakePayment).HasColumnName("takePayment");
            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Updated).HasColumnName("updated");
            this.Property(t => t.Email).HasColumnName("email");
            this.Property(t => t.ShopDeliveryOpenHours).HasColumnName("shopDeliveryOpenHours");
            this.Property(t => t.GoogleMap).HasColumnName("googleMap");
            this.Property(t => t.IsOpenByOwner).HasColumnName("isOpen");
            this.Property(t => t.IoSlink).HasColumnName("iosLink");
            this.Property(t => t.Androidlink).HasColumnName("androidLink");
            this.Property(t => t.Isactive).HasColumnName("isactive");
            this.Property(t => t.ActiveSync).HasColumnName("activeSync");
            this.Property(t => t.SmsBalance).HasColumnName("smsBalance");
            this.Property(t => t.RegisterDiscount).HasColumnName("registerDiscount");
            this.Property(t => t.RequireTestOrder).HasColumnName("requireTestOrder");

            this.Ignore(t => t.IsOpen);
            this.Ignore(t => t.ShopNews);
            this.Ignore(t => t.ShopHours);
            this.Ignore(t => t.ShopSpecialHours);
            this.Ignore(t => t.CollectionOptions);
        }
    }
}
