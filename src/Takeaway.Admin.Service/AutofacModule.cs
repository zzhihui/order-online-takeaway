using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Core.Common.Core;
using Takeaway.Admin.Service.Core;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Utility;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Service.Jobs;
using Takeaway.Admin.Util.CacheRepository;
using Takeaway.Admin.Util.Repositories;
using Takeaway.Admin.Util.Transformers.In;
using Takeaway.Admin.Util.Transformers.Out;


namespace Takeaway.Admin.Service
{
    public class AutofacModule : Autofac.Module
    {
        public void Configure(ContainerBuilder builder)
        {
            builder.RegisterModule(this);
        }

        protected override void Load(ContainerBuilder builder)
        {

            builder.Register(context => new AppScope()).As<IAppScope>().SingleInstance();

            
            
            builder.RegisterAssemblyTypes(typeof(IUnitOfWork).Assembly)
              .Where(t => typeof(IUnitOfWork).IsAssignableFrom(t))
              .PropertiesAutowired()
              .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IUnitOfWork).Assembly)
                .Where(t => typeof(IUnitOfWorkContext).IsAssignableFrom(t))
                .PropertiesAutowired()
                .AsImplementedInterfaces().InstancePerLifetimeScope();


            //builder.RegisterType<UnitOfWorkContext>().As<IUnitOfWorkContext>().SingleInstance(); 
            //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().SingleInstance();

            builder.RegisterType<ShopInfoRepository>().As<IShopInfoRepository>();
            builder.RegisterType<DeliveryOptionRepository>().As<IDeliveryOptionRepository>();
            builder.RegisterType<ShopUserRepository>().As<IShopUserRepository>();
            builder.RegisterType<MenuItemRepository>().As<IMenuItemRepository>();
            builder.RegisterType<OptionItemRepository>().As<IOptionItemRepository>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<OrderTransformer>().As<IOrderTransformer>();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            builder.RegisterType<DeliveryCostRepository>().As<IDeliveryCostRepository>();
            builder.RegisterType<ServerSettingRepository>().As<IServerSettingRepository>();
            builder.RegisterType<TableCommandRepository>().As<ITableCommandRepository>();
            builder.RegisterType<DriverRepository>().As<IDriverRepository>();
            builder.RegisterType<TableAliasRepository>().As<ITableAliasRepository>();
            builder.RegisterType<PaymentTypeRepository>().As<IPaymentTypeRepository>();
            builder.RegisterType<StoredProcedureRepository>().As<IStoredProcedureRepository>();
            builder.RegisterType<MenuItemCategoryRepository>().As<IMenuItemCategoryRepository>();
            builder.RegisterType<CustomerTransformer>().As<ICustomerTransformer>();
            builder.RegisterType<OfflineServerSettingRepository>().As<IOfflineServerSettingRepository>();
            builder.RegisterType<WsOfflineSettingTransformer>().As<IWsOfflineSettingTransformer>();
            builder.RegisterType<OrderSequenceRepository>().As<IOrderSequenceRepository>();
            builder.RegisterType<OrderHolderRepository>().As<IOrderHolderRepository>();
            builder.RegisterType<CallerIdRepository>().As<ICallerIdRepository>();
            builder.RegisterType<WsPhoneRecordTransformer>().As<IWsPhoneRecordTransformer>();
            builder.RegisterType<WsShopCustomerTransformer>().As<IWsShopCustomerTransformer>();
            builder.RegisterType<TablesRepository>().As<ITablesRepository>();
            builder.RegisterType<WsShopInfoRepository>().As<IWsShopInfoRepository>();
            builder.RegisterType<WsSettingRepository>().As<IWsSettingRepository>();
            builder.RegisterType<WsOfflineSettingRepository>().As<IWsOfflineSettingRepository>();
            builder.RegisterType<PhoneRecordTransformer>().As<IPhoneRecordTransformer>();
            builder.RegisterType<WsUserRepository>().As<IWsUserRepository>();
            




            builder.RegisterType<OfflineShopService>().As<IOfflineShopService>();
            builder.RegisterType<OnlineService>().As<IOnlineService>();

            

            builder.FromThisAssembly()
                .AnnotatedWith<ProcessJobAttribute>()
                .AsSelf()
                .WithDefaults();

            builder.FromThisAssembly()
                .AnnotatedWith<ProcessHandlerAttribute>()
                .WithDefaults();

           
                
        }
    }
}