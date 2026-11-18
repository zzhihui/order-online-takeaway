using System;
using System.Collections.Generic;
using System.Windows.Input;
using Chance.MvvmCross.Plugins.UserInteraction;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using Newtonsoft.Json;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Services;
using Takeway.Mobile.Core.Resources;
using Takeway.Mobile.Core.Services.RestServices;

namespace Takeway.Mobile.Core.ViewModels.Client
{
    public class ShopClientCategoryItemViewModel : BaseViewModel
    {

        #region Properties

        private WsMenuItemCategory _menuItemCategory;
        public WsMenuItemCategory MenuItemCategory
        {
            get { return _menuItemCategory; }
            set { _menuItemCategory = value; RaisePropertyChanged(() => MenuItemCategory); }
        }

        private List<WsMenuItem> _categoryItems;
        public List<WsMenuItem> CategoryItems
        {
            get { return _categoryItems; }
            set { _categoryItems = value; RaisePropertyChanged(() => CategoryItems); }
        }

        #endregion

        #region Commands

        public ICommand CategoryItemsSelectedCommand
        {
            get
            {
                return new MvxCommand<WsMenuItem>(DoSelectMenuItem);
            }
        }

        #endregion

        #region Attribs

        private readonly IShopRestServiceWrapper _takewayClientService;
        public void Init(string menuItemCategory = null)
        {
            if (menuItemCategory != null)
                MenuItemCategory = JsonConvert.DeserializeObject<WsMenuItemCategory>(menuItemCategory);
            GetMenuItemsByCategory();
        }

        public ShopClientCategoryItemViewModel(IShopRestServiceWrapper takewayClientService)
        {
            _takewayClientService = takewayClientService;
        }

        #endregion

        #region Methods

        private void DoSelectMenuItem(WsMenuItem item)
        {
            if (item.HasSubOrder.HasValue && item.HasSubOrder.Value)
            {
                var objectString = JsonConvert.SerializeObject(item);
                ShowViewModel<ShopClientCategoryItemSubItemViewModel>(new { menuItem = objectString });

            }
            else
            {
                Mvx.Resolve<IUserInteraction>().Input(string.Format(StringResource.page_categoryitem_othercomment, Environment.NewLine),
                    async (input) =>
                    {

                        item.Comment = input;
                        var globalViewModel = Mvx.Resolve<IGlobalModel>();
                        globalViewModel.AddOrderItem(item);
                        ShowViewModel<ShopClientBasketViewModel>();

                    }, null, StringResource.page_categoryitem_othercommenttitle);
            }

        }

        private void GetMenuItemsByCategory()
        {
            Async(() => { CategoryItems = _takewayClientService.GetMenuItemsByCategory(MenuItemCategory.Id); });

        }
        #endregion

    }
}