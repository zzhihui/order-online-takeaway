using System.Collections.ObjectModel;
using Cirrious.MvvmCross.ViewModels;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeway.Mobile.Core.ViewModels.Client
{
    public class SubItemViewModel : MvxViewModel
    {
        private ObservableCollection<WsMenuItem> _optionItems;
        public ObservableCollection<WsMenuItem> OptionItems
        {
            get { return _optionItems; }
            set
            {
                _optionItems = value;
                SelectedOptionExtraItem = null;
                RaisePropertyChanged(() => OptionItems);


            }
        }

        private WsMenuItem _selectedOptionExtraItem;
        public WsMenuItem SelectedOptionExtraItem
        {
            get { return _selectedOptionExtraItem; }
            set
            {
                _selectedOptionExtraItem = value;
                RaisePropertyChanged(() => SelectedOptionExtraItem);
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; RaisePropertyChanged(() => Description); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(() => Name); }
        }

        public SubItemViewModel()
        {
            OptionItems = new ObservableCollection<WsMenuItem>();
        }

        public static SubItemViewModel CreateSubItem(WsMenuSubItem subItemSelection)
        {
            var item = new SubItemViewModel
            {
                OptionItems = new ObservableCollection<WsMenuItem>(subItemSelection.Selections),
                Description = subItemSelection.Description,
                SelectedOptionExtraItem = null,
                Name = subItemSelection.Name,
            };


            if (item.OptionItems != null && item.OptionItems.Count > 0)
                item.SelectedOptionExtraItem = item.OptionItems[0];

            return item;
        }
    }
}