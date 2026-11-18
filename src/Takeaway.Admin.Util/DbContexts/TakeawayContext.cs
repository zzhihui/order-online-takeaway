using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using log4net;
using Takeaway.Admin.Util.Entities.Authentication;
using Takeaway.Admin.Util.Entities.Health;
using Takeaway.Admin.Util.Entities.Licence;
using Takeaway.Admin.Util.Entities.Settings;
using Takeaway.Admin.Util.Entities.Shops;
using Takeaway.Admin.Util.Models.Mapping;

namespace Takeaway.Admin.Util.DbContexts
{
    public partial class TakeawayContext : DbContext
    {
        private static readonly ILog Logger = LogManager.GetLogger("DbContextSql");

        static TakeawayContext()
        {
            var ensureDllIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            Database.SetInitializer<TakeawayContext>(null);
        }

        public TakeawayContext()
            : base(ConnectionString())
        {

            //Configuration.AutoDetectChangesEnabled = false;
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;

            //Database.Log = sql => Logger.DebugFormat(sql);
        }



        private static string ConnectionString()
        {
            var sqlBuilder = new SqlConnectionStringBuilder();
            sqlBuilder.DataSource = ".";
            sqlBuilder.InitialCatalog = "takeaway_local";
            sqlBuilder.UserID = "sa";
            sqlBuilder.Password = "REPLACE_ME";
            sqlBuilder.PersistSecurityInfo = true;
            sqlBuilder.IntegratedSecurity = false;
            sqlBuilder.MultipleActiveResultSets = true;
            return sqlBuilder.ToString();
        }


        public DbSet<Customer> Customers { get; set; }
        public DbSet<DeliveryOption> DeliveryOptions { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<MenuItemCategory> MenuItemCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
  
        public DbSet<ServerSetting> ServerSettings { get; set; }
  
        public DbSet<ShopInfo> ShopInfoes { get; set; }

        public DbSet<ShopUser> ShopUsers { get; set; }

        public DbSet<SyncStatus> SyncStatuses { get; set; }

        public DbSet<LicenceInfo> LicenceKeys { get; set; }
        public DbSet<OptionItem> OptionItems { get; set; }

        public DbSet<TableCommand> TableCommands { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }

        public DbSet<TableAlias> TableAliass { get; set; }

        public DbSet<DeliveryArea> DeliveryAreas { get; set; }

        public DbSet<DeliveryCost> DeliveryCosts { get; set; }

        public DbSet<OfflineSetting> OfflineSettings { get; set; }

        public DbSet<OrderReference> OrderReferences { get; set; }
        public DbSet<Tables> Tableses { get; set; }
        public DbSet<CallerId> CallerIds { get; set; }
        public DbSet<OrderHolder> OrderHolders { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new DeliveryOptionMap());
            modelBuilder.Configurations.Add(new MenuItemMap());
            modelBuilder.Configurations.Add(new MenuItemCategoryMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new ServerSettingMap());
            modelBuilder.Configurations.Add(new ShopInfoMap());
            modelBuilder.Configurations.Add(new ShopUserMap());
            modelBuilder.Configurations.Add(new OptionItemMap());
            modelBuilder.Configurations.Add(new TableCommandMap());
            modelBuilder.Configurations.Add(new DriverMap());
            modelBuilder.Configurations.Add(new PaymentTypeMap());
            modelBuilder.Configurations.Add(new TableAliasMap());
            modelBuilder.Configurations.Add(new DeliveryAreanMap());
            modelBuilder.Configurations.Add(new DeliveryCostMap());
            modelBuilder.Configurations.Add(new OfflineServerSettingMap());
            modelBuilder.Configurations.Add(new OrderReferenceMap());
            modelBuilder.Configurations.Add(new TablesdMap());
            modelBuilder.Configurations.Add(new CallerIdMap());
            modelBuilder.Configurations.Add(new OrderHolderMap());

        }
    }
}