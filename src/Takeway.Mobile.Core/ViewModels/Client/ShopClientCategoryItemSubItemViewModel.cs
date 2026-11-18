using System;
using System.Windows.Input;
using Chance.MvvmCross.Plugins.UserInteraction;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using Newtonsoft.Json;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Mobile.Core.Resources;

namespace Takeway.Mobile.Core.ViewModels.Client
{
    public class ShopClientCategoryItemSubItemViewModel : BaseViewModel
    {
        #region Properties

        private WsMenuItem _menuItem;
        public WsMenuItem MenuItem
        {
            get { return _menuItem; }
            set { _menuItem = value; RaisePropertyChanged(() => MenuItem); }
        }


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

        #region Commands


        public ICommand AddItemCommand
        {
            get
            {
                return new MvxCommand(OnAddItemCommand);
            }
        }

        #endregion

        #region Constructors

        public void Init(string menuItem = null)
        {
            if (menuItem != null)
                MenuItem = JsonConvert.DeserializeObject<WsMenuItem>(menuItem);
            LoadSubItems();
        }

        #endregion

        #region Methods

        private void LoadSubItems()
        {

            if (MenuItem.SubItemSelections == null || MenuItem.SubItemSelections.Count == 0)
                return;
            Item1 = SubItemViewModel.CreateSubItem(MenuItem.SubItemSelections[0]);
            Item1Visible = true;

            if (MenuItem.SubItemSelections.Count >= 2)
            {
                Item2 = SubItemViewModel.CreateSubItem(MenuItem.SubItemSelections[1]);
                Item2Visible = true;
            }

            if (MenuItem.SubItemSelections.Count >= 3)
            {
                Item3 = SubItemViewModel.CreateSubItem(MenuItem.SubItemSelections[2]);
                Item3Visible = true;
            }

            if (MenuItem.SubItemSelections.Count >= 4)
            {

                Item4 = SubItemViewModel.CreateSubItem(MenuItem.SubItemSelections[3]);
                Item4Visible = true;

            }

            if (MenuItem.SubItemSelections.Count >= 5)
            {
                Item5 = SubItemViewModel.CreateSubItem(MenuItem.SubItemSelections[4]);
                Item5Visible = true;

            }


            if (MenuItem.SubItemSelections.Count >= 6)
            {
                Item6 = SubItemViewModel.CreateSubItem(MenuItem.SubItemSelections[5]);
                Item6Visible = true;

            }

            if (MenuItem.SubItemSelections.Count >= 7)
            {
                Item7 = SubItemViewModel.CreateSubItem(MenuItem.SubItemSelections[6]);
                Item7Visible = true;

            }

            if (MenuItem.SubItemSelections.Count >= 8)
            {
                Item8 = SubItemViewModel.CreateSubItem(MenuItem.SubItemSelections[7]);
                Item8Visible = true;

            }
            if (MenuItem.SubItemSelections.Count >= 9)
            {
                Item9 = SubItemViewModel.CreateSubItem(MenuItem.SubItemSelections[8]);
                Item9Visible = true;

            }

            if (MenuItem.SubItemSelections.Count >= 10)
            {
                Item10 = SubItemViewModel.CreateSubItem(MenuItem.SubItemSelections[9]);
                Item10Visible = true;

            }

        }

        private void OnAddItemCommand()
        {
            SaveSubItemSelection();
            Mvx.Resolve<IUserInteraction>().Input(string.Format(StringResource.page_categoryitemsubitem_othercomment, Environment.NewLine),
                   async (input) =>
                   {

                       MenuItem.Comment = input;
                       var globalViewModel = Mvx.Resolve<IGlobalModel>();
                       globalViewModel.AddOrderItem(MenuItem);
                       ShowViewModel<ShopClientBasketViewModel>();

                   }, null, StringResource.page_categoryitemsubitem_othercommenttitle);

        }

        private void SaveSubItemSelection()
        {
            if (Item1 != null)
            {
                MenuItem.SubItemSelections[0].Selected = Item1.SelectedOptionExtraItem;
                MenuItem.Price += Item1.SelectedOptionExtraItem.Price;
            }

            if (Item2 != null)
            {
                MenuItem.SubItemSelections[1].Selected = Item2.SelectedOptionExtraItem;
                MenuItem.Price += Item2.SelectedOptionExtraItem.Price;
            }

            if (Item3 != null)
            {
                MenuItem.SubItemSelections[2].Selected = Item3.SelectedOptionExtraItem;
                MenuItem.Price += Item3.SelectedOptionExtraItem.Price;
            }

            if (Item4 != null)
            {
                MenuItem.SubItemSelections[3].Selected = Item4.SelectedOptionExtraItem;
                MenuItem.Price += Item4.SelectedOptionExtraItem.Price;
            }

            if (Item5 != null)
            {
                MenuItem.SubItemSelections[4].Selected = Item5.SelectedOptionExtraItem;
                MenuItem.Price += Item5.SelectedOptionExtraItem.Price;
            }
            if (Item6 != null)
            {
                MenuItem.SubItemSelections[5].Selected = Item6.SelectedOptionExtraItem;
                MenuItem.Price += Item6.SelectedOptionExtraItem.Price;
            }
            if (Item7 != null)
            {
                MenuItem.SubItemSelections[6].Selected = Item7.SelectedOptionExtraItem;
                MenuItem.Price += Item7.SelectedOptionExtraItem.Price;
            }
            if (Item8 != null)
            {
                MenuItem.SubItemSelections[7].Selected = Item8.SelectedOptionExtraItem;
                MenuItem.Price += Item8.SelectedOptionExtraItem.Price;
            }
            if (Item9 != null)
            {
                MenuItem.SubItemSelections[8].Selected = Item9.SelectedOptionExtraItem;
                MenuItem.Price += Item9.SelectedOptionExtraItem.Price;
            }
            if (Item10 != null)
            {
                MenuItem.SubItemSelections[9].Selected = Item10.SelectedOptionExtraItem;
                MenuItem.Price += Item10.SelectedOptionExtraItem.Price;
            }


        }

        #endregion
    }
}