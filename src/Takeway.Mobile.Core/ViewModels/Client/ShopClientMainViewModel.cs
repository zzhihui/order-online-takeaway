using System.Collections.Generic;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using Newtonsoft.Json;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;
using Takeaway.Service.Services;
using Takeway.Mobile.Core.Services.RestServices;

namespace Takeway.Mobile.Core.ViewModels.Client
{
    public class ShopClientMainViewModel : BaseViewModel
    {

        #region Properties


        private string _specialOffer;
        public string SpecialOffer
        {
            get { return _specialOffer; }
            set { _specialOffer = value; RaisePropertyChanged(() => SpecialOffer); }
        }


        private List<WsMenuItemCategory> _menuItemCategory;
        public List<WsMenuItemCategory> MenuItemCategory
        {
            get { return _menuItemCategory; }
            set { _menuItemCategory = value; RaisePropertyChanged(() => MenuItemCategory); }
        }

        #endregion

        #region Commands

        public ICommand MenuItemCategorySelectedCommand
        {
            get
            {
                return new MvxCommand<WsMenuItemCategory>(DoSelectMenuItemCategory);
            }
        }
        #endregion

        #region Constructor


        private readonly IShopRestServiceWrapper _takewayClientService;


        public void Init()
        {

            GetShopDetail();
            GetCategory();
        }

        public ShopClientMainViewModel(IShopRestServiceWrapper takewayClientService, IGlobalModel globalModel)
        {
            _takewayClientService = takewayClientService;
            GlobalViewModel = globalModel;
        }

        #endregion

        #region Methods


        private void DoSelectMenuItemCategory(WsMenuItemCategory item)
        {
            var objectString = JsonConvert.SerializeObject(item);
            ShowViewModel<ShopClientCategoryItemViewModel>(new { menuItemCategory = objectString });
        }


        private void GetCategory()
        {
            Async(
                () => { MenuItemCategory = _takewayClientService.GetMenuItemCategoryByGroupId((int)MenuItemCategoryGroupEnum.External); }
                );


        }

        private void GetShopDetail()
        {
            if (GlobalViewModel.WsShopInfo != null)
                SpecialOffer = GlobalViewModel.WsShopInfo.ShopSpecialOffer;
        }

        #endregion
    }
}