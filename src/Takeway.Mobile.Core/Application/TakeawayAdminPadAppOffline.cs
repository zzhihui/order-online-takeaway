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
    public class TakeawayAdminPadAppOffline : MvxApplication
    {
        public TakeawayAdminPadAppOffline()
        {
            InitialiseDependencies();
            InitialiseStartNavigation();
        }

        private void InitialiseStartNavigation()
        {
            RegisterAppStart<ShopAdminPaymentViewModel>();
            
        }

        private void InitialiseDependencies()
        {
            Mvx.RegisterSingleton<IGlobalModel>(() => new GlobalModel());
            Mvx.ConstructAndRegisterSingleton<IConfigurationRepository, ConfigurationRepository>();
            Mvx.RegisterType<IShopService, ShopRestServiceMock>();
            //@*TODO:Localisation*@
            Constants.SiteUrl = "https://indasoftyummy.azurewebsites.net";
            Constants.ShopId = 1;
            Constants.OnlineMode = false;
        } 
    }
}