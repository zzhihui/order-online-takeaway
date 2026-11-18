using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using Takeaway.Service.Services;
using Takeway.Mobile.Core.Services.DataServices;
using Takeway.Mobile.Core.Services.RestServices;
using Takeway.Mobile.Core.Utilities;
using Takeway.Mobile.Core.ViewModels;
using Takeway.Mobile.Core.ViewModels.Client;

namespace Takeway.Mobile.Core.Application
{
    public class TakeawayClientAppOffline : MvxApplication
    {
        public TakeawayClientAppOffline()
        {
            InitialiseDependencies();
            InitialiseStartNavigation();
        }

        private void InitialiseStartNavigation()
        {
            RegisterAppStart<ShopClientHomeViewModel>();
        }

        private void InitialiseDependencies()
        {
            Mvx.RegisterSingleton<IGlobalModel>(() => new GlobalModel());
            Mvx.ConstructAndRegisterSingleton<IConfigurationRepository, ConfigurationRepository>();
            Mvx.ConstructAndRegisterSingleton<IOrderRepository, OrderRepository>();
            Mvx.RegisterType<ICacheProvider, CacheProvider>();
            Mvx.RegisterType<IShopService, ShopRestService>();
            Mvx.RegisterType<IShopRestServiceWrapper, ShopRestServiceWrapper>();
            //@*TODO:Localisation*@
            Constants.SiteUrl = "https://indasoftyummy.azurewebsites.net";
            Constants.ShopId = 1;
            Constants.OnlineMode = false;
        }
    }
}