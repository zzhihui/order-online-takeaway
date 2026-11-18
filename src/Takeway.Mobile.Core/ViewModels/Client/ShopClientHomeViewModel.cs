using System.Collections.Generic;
using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.WebBrowser;
using Cirrious.MvvmCross.ViewModels;
using Takeaway.Service.Contract.Entities.Settings;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Mobile.Core.Services.RestServices;

namespace Takeway.Mobile.Core.ViewModels.Client
{
    public class ShopClientHomeViewModel : BaseViewModel
    {


        #region Properties


        private WsShop _shopInfo;
        public WsShop ShopInfo
        {
            get { return _shopInfo; }
            set { _shopInfo = value; RaisePropertyChanged(() => ShopInfo); }
        }

        private List<WsShopNews> _shopNews;
        public List<WsShopNews> ShopNews
        {
            get { return _shopNews; }
            set
            {
                _shopNews = value;
                RaisePropertyChanged(() => ShopNews);
            }
        }

        private string _shopOpenImagePath;
        public string ShopOpenImagePath
        {
            get
            {
                return _shopOpenImagePath;
            }
            set { _shopOpenImagePath = value; RaisePropertyChanged(() => ShopOpenImagePath); }
        }

        private string _shopOpenHours;
        public string ShopOpenHours
        {
            get
            {
                return _shopOpenHours;
            }
            set { _shopOpenHours = value; RaisePropertyChanged(() => ShopOpenHours); }
        }

        #endregion

        #region Commands

        public ICommand ViewMenuCommand
        {
            get
            {
                return new MvxCommand(OnViewMenuCommand);
            }
        }


        public ICommand ViewDeliveryArea
        {
            get
            {
                return new MvxCommand(OnViewDeliveryArea);
            }
        }



        public ICommand ViewBasketCommand
        {
            get
            {
                return new MvxCommand(OnViewBasketCommand);
            }
        }

        public ICommand ShopUrlCommand
        {
            get
            {
                return new MvxCommand(OnShopUrlCommand);
            }
        }

        #endregion

        #region Constructor

        private readonly IShopRestServiceWrapper _takewayClientService;
        private readonly IMvxWebBrowserTask _webBrowser;

        public void Init()
        {

            GetSystemSetting();
            GetShopDetail();
        }


        public ShopClientHomeViewModel(IShopRestServiceWrapper takewayClientService, IMvxWebBrowserTask webBrowser, IGlobalModel globalModel)
        {
            _takewayClientService = takewayClientService;
            _webBrowser = webBrowser;
            GlobalViewModel = globalModel;
        }
        #endregion

        #region Methods

        private void OnViewBasketCommand()
        {
            ShowViewModel<ShopClientBasketViewModel>();
        }
        private void OnShopUrlCommand()
        {
            if (ShopInfo == null)
                return;
            _webBrowser.ShowWebPage(ShopInfo.ShopUrl);
        }

        private void OnViewMenuCommand()
        {
            ShowViewModel<ShopClientMainViewModel>();
        }

        private void OnViewDeliveryArea()
        {
            ShowViewModel<ShopClientDeliveryAreaViewModel>();
        }


        private void GetShopDetail()
        {

            ShopInfo = _takewayClientService.GetShopInfo();
            ShopOpenHours = ShopInfo.ShopOpenHours;
            GlobalViewModel.WsShopInfo = ShopInfo;

            if (ShopInfo.ShopNews != null)
                ShopNews = ShopInfo.ShopNews;
            if (ShopInfo.IsOpen.HasValue && ShopInfo.IsOpen.Value)
                ShopOpenImagePath = "res:ic_open";
            else
                ShopOpenImagePath = "res:ic_closed";

        }

        private void GetSystemSetting()
        {
            List<WsSetting> systemSettings = null;
            systemSettings = _takewayClientService.GetSystemSettings();
            GlobalViewModel.WsSettings = systemSettings;

        }

        #endregion

    }
}