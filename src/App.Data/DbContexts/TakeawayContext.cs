using System.Data.Entity;
using log4net;
using Takeaway.Data.Entities.Authentication;
using Takeaway.Data.Entities.Health;
using Takeaway.Data.Entities.Licence;
using Takeaway.Data.Entities.Settings;
using Takeaway.Data.Entities.Shops;
using Takeaway.Data.Models.Mapping;

namespace Takeaway.Data.DbContexts
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
            : base("Name=TakeawayContext")
        {

            //Configuration.AutoDetectChangesEnabled = false;
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;

            Database.Log = sql => Logger.DebugFormat(sql);
        }


        public DbSet<Customer> Customers { get; set; }
        public DbSet<DeliveryOption> DeliveryOptions { get; set; }
        public DbSet<DiscountOffer> DiscountOffers { get; set; }
        public DbSet<MemuItemOffer> MemuItemOffers { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<MenuItemCategory> MenuItemCategories { get; set; }
        public DbSet<MenuItemSubItem> MenuItemSubItems { get; set; }
        public DbSet<MenuSubItem> MenuSubItems { get; set; }
        public DbSet<MenuSubItemItem> MenuSubItemItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderItemsSelection> OrderItemsSelections { get; set; }

        public DbSet<ServerSetting> ServerSettings { get; set; }
        public DbSet<ShopGeneralHour> ShopGeneralHours { get; set; }
        public DbSet<ShopInfo> ShopInfoes { get; set; }
        public DbSet<ShopNews> ShopNews { get; set; }
        public DbSet<ShopSpecialHour> ShopSpecialHours { get; set; }
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


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new DeliveryOptionMap());
            modelBuilder.Configurations.Add(new DiscountOfferMap());
            modelBuilder.Configurations.Add(new MemuItemOfferMap());
            modelBuilder.Configurations.Add(new MenuItemMap());
            modelBuilder.Configurations.Add(new MenuItemCategoryMap());
            modelBuilder.Configurations.Add(new MenuItemSubItemMap());
            modelBuilder.Configurations.Add(new MenuSubItemMap());
            modelBuilder.Configurations.Add(new MenuSubItemItemMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new OrderItemMap());
            modelBuilder.Configurations.Add(new OrderItemsSelectionMap());
            modelBuilder.Configurations.Add(new ServerSettingMap());
            modelBuilder.Configurations.Add(new ShopGeneralHourMap());
            modelBuilder.Configurations.Add(new ShopInfoMap());
            modelBuilder.Configurations.Add(new ShopNewsMap());
            modelBuilder.Configurations.Add(new ShopSpecialHourMap());
            modelBuilder.Configurations.Add(new ShopUserMap());
            modelBuilder.Configurations.Add(new SyncStatusMap());
            modelBuilder.Configurations.Add(new LicenceKeyMap());
            modelBuilder.Configurations.Add(new OptionItemMap());
            modelBuilder.Configurations.Add(new TableCommandMap());
            modelBuilder.Configurations.Add(new DriverMap());
            modelBuilder.Configurations.Add(new PaymentTypeMap());
            modelBuilder.Configurations.Add(new TableAliasMap());
            modelBuilder.Configurations.Add(new DeliveryAreanMap());
            modelBuilder.Configurations.Add(new DeliveryCostMap());

        }
    }
}