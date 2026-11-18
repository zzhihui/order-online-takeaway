using System.Data.Entity.ModelConfiguration;
using Takeaway.Admin.Util.Entities.Settings;

namespace Takeaway.Admin.Util.Models.Mapping
{
    public class OfflineServerSettingMap : EntityTypeConfiguration<OfflineSetting>
    {
        public OfflineServerSettingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("localSetting");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ShopId).HasColumnName("shopId");
            this.Property(t => t.Name).HasColumnName("settingKey");
            this.Property(t => t.SettingValue).HasColumnName("settingValue");
            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Updated).HasColumnName("updated");
        }
    }
}
