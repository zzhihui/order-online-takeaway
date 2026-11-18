using System;
using System.Collections.Generic;
using System.Configuration;
using Autofac;
using Core.Common.Utility;
using log4net;
using log4net.Config;
using Takeaway.Admin.Util.Services;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;
using Takeway.Mobile.Core.Utilities;

namespace Takeaway.Admin.Service
{
    class Program
    {

        private static readonly ILog Log = LogManager.GetLogger(typeof(Program).Name);

        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            ConfigureUrl();

            SendTestOrders();
        }

        private static void SendTestOrders()
        {
            Log.Info("Start SendTestOrders Job ");
            Console.WriteLine("Start SendTestOrders Job");

            var builder = GetSharedContainerBuilder();
            var container = builder.Build();
            var onlineService = container.Resolve<IOnlineService>();

            Log.Info("Get Shop to Send");
            Console.WriteLine("Get Shop to Send");
            var shops = onlineService.GetAllShopsNeedTestOrder();


            foreach (var shop in shops)
            {
                try
                {
                    
                    Log.InfoFormat("Sending shopId:{0}",shop.ShopId);
                    Console.WriteLine("Sending shopId:{0}", shop.ShopId);


                    //1. Get Shop Menu Item
                    var menuItems = onlineService.GetMenuItemByCategoryGroupId((int) MenuItemCategoryGroupEnum.External,
                        shop.ShopId.Value);

                    if (menuItems == null || menuItems.Count == 0)
                        throw new Exception("No Menu Item");

                    Log.InfoFormat("Creating Order");
                    Console.WriteLine("Creating Order");

                    var order = BuildOrderBasedOnShop(shop);
                        order.OrderItems.Add(menuItems[0]);

                    Log.InfoFormat("Submitting Order");
                    Console.WriteLine("Submitting Order");
                    //2. Submit an order
                    var result=onlineService.SubmitCustomerOrder(order, shop.ShopId.Value);

                    Log.InfoFormat("Complete shopId:{0}", shop.ShopId);
                    Console.WriteLine("Complete shopId:{0}", shop.ShopId);
                }
                catch (System.Exception ex)
                {
                    Log.ErrorFormat("Failed on shopId:{0} --{1}", shop.ShopId, ex.Message);
                    Console.WriteLine("Failed shopId:{0} --{1}", shop.ShopId, ex.Message);
                }
                
            }

            Log.Info("Finish SendTestOrders Job ");

        }

        private static WsOrder BuildOrderBasedOnShop(WsShop shop)
        {
            var order = new WsOrder
            {
                Guid = Guid.NewGuid().ToString(),
                ContactName = "KingFood Test",
                PhoneNumber = "0838834625",
                Email = "support@motionmedia.ie",
                Address = "KingFood Address",
                DeliveryCharge = 0,
                OrderOriginalTotal = 0,
                OrderTotal = 0,
                DeliveryType = new WsDeliveryOption() {Id = 2, Name = "For Collection", ShopId = 0},
                OrderRef = GuidHashUtil.GetUniqueOrderKey(),
                OrderSource = (int) WsOrderSourceEnum.Web,
                Comment = "This is an test order from Kingfood.ie please do not make it. Thanks"
            };

            return order;
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
        

        private static ContainerBuilder GetSharedContainerBuilder()
        {
            var builder = new ContainerBuilder();

            //// Register the Batch Processor Implementations
            builder.RegisterModule(new AutofacModule());

            return builder;
        }      

    }

}
