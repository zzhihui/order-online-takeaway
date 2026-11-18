using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Autofac;
using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;
using Newtonsoft.Json;
using Takeaway.Admin.Service.Processors;
using Takeaway.Admin.Util.Services;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Mobile.Core.Utilities;
using Topshelf;
using Topshelf.Common.Logging;

namespace Takeaway.Admin.Service
{
    class Program
    {

        private static readonly ILog Log = LogManager.GetLogger(typeof(Program).Name);

        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            ConfigureUrl();

            if (ConfigurationManager.AppSettings["FeatureSyncCloudToClientOn"] == "true")
            {

                Log.InfoFormat("[Main] Start to Sync");
                SyncApplication();
            }
            else
            {
                Log.InfoFormat("[Main] Initial Service Configuration");
                InitConfiguration();
            }
        }

        private static void ConfigureUrl()
        {
            //@*TODO:Localisation*@
#if DEBUG
            Constants.SiteUrl = "http://localhost:49041";
#else
            Constants.SiteUrl = "https://indasoftkingfoodbatch.azurewebsites.net";
#endif

        }

        private static void SyncApplication()
        {

            var builder = GetSharedContainerBuilder();
            var container = builder.Build();


            Log.Info("Start SyncCloudToClient Job ");

            var offlineShopService = container.Resolve<IOfflineShopService>();
            var onlineService = container.Resolve<IOnlineService>();

            var shopInfo = SyncShopInfo(onlineService, offlineShopService);
            if (shopInfo != null && shopInfo.ActiveSync.HasValue && shopInfo.ActiveSync.Value)
            {
                SyncSetting(onlineService, offlineShopService);
            }




            Log.Info("Finish SyncClientToCloud Job ");
        }


        private static ContainerBuilder GetSharedContainerBuilder()
        {
            var builder = new ContainerBuilder();


            //// Register the Batch Processor Implementations
            builder.RegisterModule(new AutofacModule());

            return builder;
        }
        

        private static void SyncSetting(IOnlineService onlineService, IOfflineShopService offlineShopService)
        {

            var lists = onlineService.GetSettings();
            if (lists == null || lists.Count == 0)
            {
                Log.ErrorFormat("Fail to SyncSetting");
                return;
            }
            offlineShopService.SyncSettings(lists);
        }

      
        private static WsShop SyncShopInfo(IOnlineService onlineService,IOfflineShopService offlineShopService)
        {
            var shopInfo = onlineService.GetShopInfo();
            if (shopInfo == null)
            {
                Log.ErrorFormat("Faild to download Shop Info");
                return null;
            }
            offlineShopService.SyncShopInfo(shopInfo);
            return shopInfo;
        }





        private static void InitConfiguration()
        {
          
            try
            {
                Log.DebugFormat("[InitConfiguration] Current Directory {0}", AppDomain.CurrentDomain.BaseDirectory);
                Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

                HostFactory.Run(
                    x =>
                    {
                        x.Service<SyncProcessor>(
                            s =>
                            {
                                s.ConstructUsing(name => new SyncProcessor());
                                s.WhenStarted(tc => tc.Start());
                                s.WhenStopped(tc => tc.Stop());
                            });
                        x.RunAsLocalSystem();

                        x.UseCommonLogging();
                        x.SetDescription("Indasoft Windows Service");
                        x.SetDisplayName("Indasoft.WindowService");
                        x.SetServiceName("Indasoft.WindowService");
                    });
            }
            catch (Exception exception)
            {
                var errMsg = string.Format("ERROR:- There has been an issue with the batch processor job: {0}{2}StackTrace ---{2}{1}", exception.Message, exception.StackTrace, Environment.NewLine);
                Log.Error(errMsg);
                Console.WriteLine(errMsg);
                throw;
            }

        }

    }

}
