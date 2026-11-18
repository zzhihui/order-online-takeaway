using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Services;
using Takeway.Mobile.Core.Services.RestServices;
using Takeway.Mobile.Core.Utilities;
using Takeway.Mobile.Core.ViewModels.Client;

namespace Takeway.Mobile.Core.ViewModels.Admin
{
    public class ShopAdminHomePadViewModel : BaseViewModel
    {
        #region Properties

        //Shop Information
        private WsShop _shopInfo;
        public WsShop ShopInfo
        {
            get { return _shopInfo; }
            set { _shopInfo = value; RaisePropertyChanged(() => ShopInfo); }
        }

        //Menu Category
        private List<WsMenuItemCategory> _menuItemCategory;
        public List<WsMenuItemCategory> MenuItemCategory
        {
            get { return _menuItemCategory; }
            set { _menuItemCategory = value; RaisePropertyChanged(() => MenuItemCategory); }
        }

        //Menu Items for the category
        private List<WsMenuItem> _categoryItems;
        public List<WsMenuItem> CategoryItems
        {
            get { return _categoryItems; }
            set { _categoryItems = value; RaisePropertyChanged(() => CategoryItems); }
        }

        //Current Order
        private WsOrder _myOrder;
        public WsOrder MyOrder
        {
            get { return _myOrder; }
            set
            {
                _myOrder = value;
                RaisePropertyChanged(() => MyOrder);
            }
        }

        //Current  Item
        private WsMenuItem _selectedItem;
        public WsMenuItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);
            }
        }
        private ObservableCollection<WsMenuItem> _myOrderItems;
        public ObservableCollection<WsMenuItem> MyOrderItems
        {
            get { return _myOrderItems; }
            set { _myOrderItems = value; RaisePropertyChanged(() => MyOrderItems); }
        }

        private string _orderTotal;
        public string OrderTotal
        {
            get { return _orderTotal; }
            set { _orderTotal = value; RaisePropertyChanged(() => OrderTotal); }
        }

        private bool _canSubmit;
        public bool CanSubmit
        {
            get { return _canSubmit; }
            set { _canSubmit = value; RaisePropertyChanged(() => CanSubmit); }
        }

        #region Display Region Flags

        //Show Different Regions
        private bool _showOptionExtraRegion;
        public bool ShowOptionExtraRegion
        {
            get { return _showOptionExtraRegion; }
            set { _showOptionExtraRegion = value; RaisePropertyChanged(() => ShowOptionExtraRegion); }
        }

   
        private bool _showCategoryItemRegion;
        public bool ShowCategoryItemRegion
        {
            get { return _showCategoryItemRegion; }
            set { _showCategoryItemRegion = value; RaisePropertyChanged(() => ShowCategoryItemRegion); }
        }

        #endregion

        #region SubItems

        //All SubItems
        private bool _item1Visible;
        public bool Item1Visible
        {
            get { return _item1Visible; }
            set { _item1Visible = value; RaisePropertyChanged(() => Item1Visible); }
        }

        private SubItemViewModel _item1;
        public SubItemViewModel Item1
        {
            get { return _item1; }
            set { _item1 = value; RaisePropertyChanged(() => Item1); }
        }

        private bool _item2Visible;
        public bool Item2Visible
        {
            get { return _item2Visible; }
            set { _item2Visible = value; RaisePropertyChanged(() => Item2Visible); }
        }

        private SubItemViewModel _item2;
        public SubItemViewModel Item2
        {
            get { return _item2; }
            set { _item2 = value; RaisePropertyChanged(() => Item2); }

        }

        private bool _item3Visible;
        public bool Item3Visible
        {
            get { return _item3Visible; }
            set { _item3Visible = value; RaisePropertyChanged(() => Item3Visible); }
        }

        private SubItemViewModel _item3;
        public SubItemViewModel Item3
        {
            get { return _item3; }
            set { _item3 = value; RaisePropertyChanged(() => Item3); }

        }

        private bool _item4Visible;
        public bool Item4Visible
        {
            get { return _item4Visible; }
            set { _item4Visible = value; RaisePropertyChanged(() => Item4Visible); }
        }

        private SubItemViewModel _item4;
        public SubItemViewModel Item4
        {
            get { return _item4; }
            set { _item4 = value; RaisePropertyChanged(() => Item4); }
        }

        private bool _item5Visible;
        public bool Item5Visible
        {
            get { return _item5Visible; }
            set { _item5Visible = value; RaisePropertyChanged(() => Item5Visible); }
        }

        private SubItemViewModel _item5;
        public SubItemViewModel Item5
        {
            get { return _item5; }
            set { _item5 = value; RaisePropertyChanged(() => Item5); }
        }

        private bool _item6Visible;
        public bool Item6Visible
        {
            get { return _item6Visible; }
            set { _item6Visible = value; RaisePropertyChanged(() => Item6Visible); }
        }

        private SubItemViewModel _item6;
        public SubItemViewModel Item6
        {
            get { return _item6; }
            set { _item6 = value; RaisePropertyChanged(() => Item6); }
        }

        private bool _item7Visible;
        public bool Item7Visible
        {
            get { return _item7Visible; }
            set { _item7Visible = value; RaisePropertyChanged(() => Item7Visible); }
        }

        private SubItemViewModel _item7;
        public SubItemViewModel Item7
        {
            get { return _item7; }
            set { _item7 = value; RaisePropertyChanged(() => Item7); }
        }

        private bool _item8Visible;
        public bool Item8Visible
        {
            get { return _item8Visible; }
            set { _item8Visible = value; RaisePropertyChanged(() => Item8Visible); }
        }

        private SubItemViewModel _item8;
        public SubItemViewModel Item8
        {
            get { return _item8; }
            set { _item8 = value; RaisePropertyChanged(() => Item8); }
        }

        private bool _item9Visible;
        public bool Item9Visible
        {
            get { return _item9Visible; }
            set { _item9Visible = value; RaisePropertyChanged(() => Item9Visible); }
        }

        private SubItemViewModel _item9;
        public SubItemViewModel Item9
        {
            get { return _item9; }
            set { _item9 = value; RaisePropertyChanged(() => Item9); }
        }

        private bool _item10Visible;
        public bool Item10Visible
        {
            get { return _item10Visible; }
            set { _item10Visible = value; RaisePropertyChanged(() => Item10Visible); }
        }

        private SubItemViewModel _item10;
        public SubItemViewModel Item10
        {
            get { return _item10; }
            set { _item10 = value; RaisePropertyChanged(() => Item10); }
        }
        #endregion

        

        private enum DisplayRegionEnum
        {
            CategoryItem,
            OptionExtra,
            SubmitOrder
        }

        #endregion

        #region Constructor

        private readonly IShopRestServiceWrapper _takewayClientService;

        public void Init()
        {

            GetSystemSetting();
            LoadShopDetail();
            LoadShopCategory();
            GetPendingOrder();
        }

        public ShopAdminHomePadViewModel(IGlobalModel globalModel, IShopRestServiceWrapper takewayClientService)
        {
            _takewayClientService = takewayClientService;
            GlobalViewModel = globalModel;
        }

        #endregion

        #region Command

        public ICommand MenuItemCategorySelectedCommand
        {
            get
            {
                return new MvxCommand<WsMenuItemCategory>(DoSelectMenuItemCategory);
            }
        }

        public ICommand CategoryItemsSelectedCommand
        {
            get
            {
                return new MvxCommand<WsMenuItem>(OnSelectMenuItemCommand);
            }
        }
   
        public ICommand AddItemCommand
        {
            get
            {
                return new MvxCommand(OnAddItemToBasketCommand);
            }
        }

       


        #endregion

        #region Methods
      

        private void DisplayRegion(DisplayRegionEnum display)
        {
            switch (display)
            {
                case DisplayRegionEnum.CategoryItem:
                    ShowOptionExtraRegion = false;
                    ShowCategoryItemRegion = true;
                    

                    break;
                case DisplayRegionEnum.OptionExtra:
                    ShowOptionExtraRegion = true;
                    ShowCategoryItemRegion = false;
                    
                    break;
                case DisplayRegionEnum.SubmitOrder:
                    ShowOptionExtraRegion = false;
                    ShowCategoryItemRegion = false;
                    
                    break;

            }

        }

        private void OnAddItemToBasketCommand()
        {
            SaveSubItemSelection();
            //var clonseObj = SerializeHelper.CloneJson(SelectedItem);
            //clonseObj.Guid = Guid.NewGuid().ToString();

            GlobalViewModel.AddOrderItem(SelectedItem);
            GetPendingOrder();

            DisplayRegion(DisplayRegionEnum.CategoryItem);
        }

        private void SaveSubItemSelection()
        {
            if (Item1 != null)
            {
                SelectedItem.SubItemSelections[0].Selected = Item1.SelectedOptionExtraItem;
                SelectedItem.Price += Item1.SelectedOptionExtraItem.Price;
            }

            if (Item2 != null)
            {
                SelectedItem.SubItemSelections[1].Selected = Item2.SelectedOptionExtraItem;
                SelectedItem.Price += Item2.SelectedOptionExtraItem.Price;
            }

            if (Item3 != null)
            {
                SelectedItem.SubItemSelections[2].Selected = Item3.SelectedOptionExtraItem;
                SelectedItem.Price += Item3.SelectedOptionExtraItem.Price;
            }

            if (Item4 != null)
            {
                SelectedItem.SubItemSelections[3].Selected = Item4.SelectedOptionExtraItem;
                SelectedItem.Price += Item4.SelectedOptionExtraItem.Price;
            }

            if (Item5 != null)
            {
                SelectedItem.SubItemSelections[4].Selected = Item5.SelectedOptionExtraItem;
                SelectedItem.Price += Item5.SelectedOptionExtraItem.Price;
            }
            if (Item6 != null)
            {
                SelectedItem.SubItemSelections[5].Selected = Item6.SelectedOptionExtraItem;
                SelectedItem.Price += Item6.SelectedOptionExtraItem.Price;
            }
            if (Item7 != null)
            {
                SelectedItem.SubItemSelections[6].Selected = Item7.SelectedOptionExtraItem;
                SelectedItem.Price += Item7.SelectedOptionExtraItem.Price;
            }
            if (Item8 != null)
            {
                SelectedItem.SubItemSelections[7].Selected = Item8.SelectedOptionExtraItem;
                SelectedItem.Price += Item8.SelectedOptionExtraItem.Price;
            }
            if (Item9 != null)
            {
                SelectedItem.SubItemSelections[8].Selected = Item9.SelectedOptionExtraItem;
                SelectedItem.Price += Item9.SelectedOptionExtraItem.Price;
            }
            if (Item10 != null)
            {
                SelectedItem.SubItemSelections[9].Selected = Item10.SelectedOptionExtraItem;
                SelectedItem.Price += Item10.SelectedOptionExtraItem.Price;
            }


        }
       

        private void GetPendingOrder()
        {
            MyOrder = GlobalViewModel.MyOrder;
            MyOrderItems = new ObservableCollection<WsMenuItem>(MyOrder.OrderItems);
            OrderTotal = MyOrder.OrderTotal.ToString("C2", new CultureInfo("en-IE"));
            CanSubmit = MyOrderItems.Count > 0;
        }

        private void LoadShopCategory()
        {
            MenuItemCategory = _takewayClientService.GetMenuItemCategoryByGroupId(Constants.CategoryGroupId);
        }

        private void LoadShopDetail()
        {
            //Only Load when it is empty
             if (GlobalViewModel.WsShopInfo==null)
            ShopInfo = _takewayClientService.GetShopInfo();
            GlobalViewModel.WsShopInfo = ShopInfo;

        }


        private void OnSelectMenuItemCommand(WsMenuItem selectedItem)
        {
            var selectItemObj = SerializeHelper.CloneJson(selectedItem);
            selectedItem.Guid = Guid.NewGuid().ToString();
            SelectedItem = selectItemObj;
            CleanSubItems();
            LoadSubItems();
            DisplayRegion(DisplayRegionEnum.OptionExtra);

        }

        private void CleanSubItems()
        {
            Item1 = null;
            Item1Visible = false;
            Item2 = null;
            Item2Visible = false;
            Item3 = null;
            Item3Visible = false;
            Item4 = null;
            Item4Visible = false;
            Item5 = null;
            Item5Visible = false;
            Item6 = null;
            Item6Visible = false;
            Item7 = null;
            Item7Visible = false;
            Item8 = null;
            Item8Visible = false;
            Item9 = null;
            Item9Visible = false;
            Item10 = null;
            Item10Visible = false;

        }

        //Methods
        private void LoadSubItems()
        {

            if (SelectedItem.SubItemSelections == null || SelectedItem.SubItemSelections.Count == 0)
                return;

            Item1 = SubItemViewModel.CreateSubItem(SelectedItem.SubItemSelections[0]);
            Item1Visible = true;

            if (SelectedItem.SubItemSelections.Count >= 2)
            {
                Item2 = SubItemViewModel.CreateSubItem(SelectedItem.SubItemSelections[1]);
                Item2Visible = true;
            }

            if (SelectedItem.SubItemSelections.Count >= 3)
            {
                Item3 = SubItemViewModel.CreateSubItem(SelectedItem.SubItemSelections[2]);
                Item3Visible = true;
            }

            if (SelectedItem.SubItemSelections.Count >= 4)
            {

                Item4 = SubItemViewModel.CreateSubItem(SelectedItem.SubItemSelections[3]);
                Item4Visible = true;

            }

            if (SelectedItem.SubItemSelections.Count >= 5)
            {
                Item5 = SubItemViewModel.CreateSubItem(SelectedItem.SubItemSelections[4]);
                Item5Visible = true;

            }


            if (SelectedItem.SubItemSelections.Count >= 6)
            {
                Item6 = SubItemViewModel.CreateSubItem(SelectedItem.SubItemSelections[5]);
                Item6Visible = true;

            }

            if (SelectedItem.SubItemSelections.Count >= 7)
            {
                Item7 = SubItemViewModel.CreateSubItem(SelectedItem.SubItemSelections[6]);
                Item7Visible = true;

            }

            if (SelectedItem.SubItemSelections.Count >= 8)
            {
                Item8 = SubItemViewModel.CreateSubItem(SelectedItem.SubItemSelections[7]);
                Item8Visible = true;

            }
            if (SelectedItem.SubItemSelections.Count >= 9)
            {
                Item9 = SubItemViewModel.CreateSubItem(SelectedItem.SubItemSelections[8]);
                Item9Visible = true;

            }

            if (SelectedItem.SubItemSelections.Count >= 10)
            {
                Item10 = SubItemViewModel.CreateSubItem(SelectedItem.SubItemSelections[9]);
                Item10Visible = true;

            }

        }


        private void DoSelectMenuItemCategory(WsMenuItemCategory selectCategory)
        {
            CategoryItems = _takewayClientService.GetMenuItemsByCategory(selectCategory.Id);
            DisplayRegion(DisplayRegionEnum.CategoryItem);

        }


        private void GetSystemSetting()
        {
            var systemSettings = _takewayClientService.GetSystemSettings();
            GlobalViewModel.WsSettings = systemSettings;
        }

        #endregion
    }
}