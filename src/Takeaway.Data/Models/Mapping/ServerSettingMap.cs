using System.Data.Entity.ModelConfiguration;
using Takeaway.Data.Entities.Settings;

namespace Takeaway.Data.Models.Mapping
{
    public class ServerSettingMap : EntityTypeConfiguration<ServerSetting>
    {
        public ServerSettingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SettingKey)
                .HasMaxLength(50);

            this.Property(t => t.SettingValue)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ServerSetting");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ShopId).HasColumnName("shopId");
            this.Property(t => t.SettingKey).HasColumnName("settingKey");
            this.Property(t => t.SettingValue).HasColumnName("settingValue");
            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Updated).HasColumnName("updated");
            this.Property(t => t.IsServer).HasColumnName("isServer");
        }
    }
}
