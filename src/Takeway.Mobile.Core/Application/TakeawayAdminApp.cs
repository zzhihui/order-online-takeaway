using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using Takeaway.Service.Services;
using Takeway.Mobile.Core.Services.DataServices;
using Takeway.Mobile.Core.Services.RestServices;
using Takeway.Mobile.Core.Utilities;
using Takeway.Mobile.Core.ViewModels;
using Takeway.Mobile.Core.ViewModels.Admin;

namespace Takeway.Mobile.Core.Application
{
    public class TakeawayAdminApp : MvxApplication
    {
        public TakeawayAdminApp()
        {
            InitialiseDependencies();
            InitialiseStartNavigation();
        }

        private void InitialiseStartNavigation()
        {
            RegisterAppStart<ShopAdminHomePadViewModel>();
        }

        private void InitialiseDependencies()
        {
            Mvx.RegisterSingleton<IGlobalModel>(() => new GlobalModel());
            Mvx.ConstructAndRegisterSingleton<IConfigurationRepository, ConfigurationRepository>();
            Mvx.RegisterType<IShopService, ShopRestServiceWrapper>();
            //@*TODO:Localisation*@
            Constants.SiteUrl = "https://indasoftyummy.azurewebsites.net";
            Constants.ShopId = 1;
            Constants.CategoryGroupId = 1;
            Constants.OnlineMode = true;
        }
    }
}