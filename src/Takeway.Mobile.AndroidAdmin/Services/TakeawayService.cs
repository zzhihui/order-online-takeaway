using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Cirrious.CrossCore;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Services;
using Takeway.Mobile.Core.Services.DataServices;
using Takeway.Mobile.Core.ViewModels;

namespace Takeway.Mobile.AndroidAdmin.Services
{
    [Service]
    [IntentFilter(new String[] { "indasoft.TakeawayService.Admin" })]
    public class TakeawayService : IntentService
    {

        public const string NewOrderUpdateAction = "NewOrderUpdate";
        private IBinder _binder;
        private List<WsOrder> _openOrders;

        protected override void OnHandleIntent(Intent intent)
        {
            var configurationRepository = Mvx.Resolve<IConfigurationRepository>();

            //Get Notification
            if (!configurationRepository.IsNotificationSettingOn())
                return;


            //Get orders
            var globalViewModel = Mvx.Resolve<IGlobalModel>();
            if (globalViewModel == null || globalViewModel.WsShopUser == null)
                return;

            var serviceClient = Mvx.Resolve<IShopService>();
            _openOrders = serviceClient.GetOpenOrders();

            if (_openOrders == null || _openOrders.Count==0)
                return;

            var maxOrder = _openOrders.OrderByDescending(r => r.Id).First();

            //Get Local Setting
            var lastOrderSetting = configurationRepository.GetByKey(DbConfigurationEnum.LastOrderId.ToString());
            var lastOrderId = 0;
            if (lastOrderSetting != null)
                lastOrderId = int.Parse(lastOrderSetting.ConfigValue);


            if (maxOrder.Id <= lastOrderId)
                return;


            var speciaOfferIntent = new Intent(NewOrderUpdateAction);
            SendOrderedBroadcast(speciaOfferIntent, null);


            //Store Last Order
            CreateOrUpdateKey(DbConfigurationEnum.LastOrderId.ToString(), maxOrder.Id.ToString(), configurationRepository);

           
        }

        private void CreateOrUpdateKey(string configKey, string configValue, IConfigurationRepository configurationRepository)
        {

            var configItem = configurationRepository.GetByKey(configKey);
            if (configItem != null && !string.IsNullOrEmpty(configValue))
            {
                configItem.ConfigValue = configValue;
                configItem.Updated = DateTime.Now;
                configurationRepository.Update(configItem);

            }
            else if (!string.IsNullOrEmpty(configValue))
            {
                configItem = new DbConfigurationItem { ConfigKey = configKey, ConfigValue = configValue, Created = DateTime.Now };
                configurationRepository.Add(configItem);

            }
        }

        public override IBinder OnBind(Intent intent)
        {
            _binder = new TakeawayServiceBinder(this);
            return _binder;
        }

    }

    public class TakeawayServiceBinder : Binder
    {
        readonly TakeawayService _service;

        public TakeawayServiceBinder(TakeawayService service)
        {
            this._service = service;
        }

        public TakeawayService GetService()
        {
            return _service;
        }
    }
}