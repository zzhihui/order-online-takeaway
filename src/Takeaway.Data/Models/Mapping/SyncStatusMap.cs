using System.Data.Entity.ModelConfiguration;
using Takeaway.Data.Entities.Health;
using Takeaway.Data.Entities.Settings;

namespace Takeaway.Data.Models.Mapping
{
    public class SyncStatusMap : EntityTypeConfiguration<SyncStatus>
    {
        public SyncStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

        
            // Table & Column Mappings
            this.ToTable("SyncStatus");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ShopId).HasColumnName("shopId");
            this.Property(t => t.CheckTime).HasColumnName("checkTime");
            this.Property(t => t.Created).HasColumnName("created");
            this.Property(t => t.Updated).HasColumnName("updated");
        }
    }
}
