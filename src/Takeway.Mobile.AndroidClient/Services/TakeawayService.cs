using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Cirrious.CrossCore;
using Takeaway.Service.Services;
using Takeway.Mobile.Core.Services.DataServices;
using Takeway.Mobile.Core.Services.RestServices;
using Takeway.Mobile.Core.ViewModels;

namespace Takeway.Mobile.AndroidClient.Services
{
    [Service]
    [IntentFilter(new String[] { "indasoft.TakeawayService.Client" })]
    public class TakeawayService : IntentService
    {

        public const string SpecialOfferUpdateAction = "SpecialOfferUpdate";
        IBinder _binder;

        protected override void OnHandleIntent(Intent intent)
        {
            var configurationRepository = Mvx.Resolve<IConfigurationRepository>();

            //Check if notification is turn on
            if (!configurationRepository.IsNotificationSettingOn())
                return;

            //Get Last offer message
            var lastOffer = configurationRepository.GetLastOffer();

            //Get UserId
            var userId = configurationRepository.GetUserId();

            //Retrieve From Service
            var clientService = Mvx.Resolve<IShopService>();
            var clientOffers = clientService.GetLatestOffer(lastOffer, userId);
            if (clientOffers == null || clientOffers.Count <= 0) return;

            //Latest Offer
            var latestClientOffer = clientOffers.OrderByDescending(r => r.Id).First();
            var globalViewModel = Mvx.Resolve<IGlobalModel>();
            globalViewModel.LatestOffer = latestClientOffer;

            //Notification
            var speciaOfferIntent = new Intent(SpecialOfferUpdateAction);
            SendOrderedBroadcast(speciaOfferIntent, null);

            //SaveLatestOffer
            configurationRepository.CreateOrUpdateKey(DbConfigurationEnum.LastOfferId.ToString(), latestClientOffer.Id.ToString());
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