using System.Collections.ObjectModel;
using System.Linq;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Admin.Wpf.ViewModels
{
    public class SubItemModel
    {

        #region Properties
        
        public ObservableCollection<WsMenuItem> OptionItems { get; set; }

        public WsMenuItem SelectedOptionExtraItem { get; set; }

        public string Description { get; set; }
        public string Name { get; set; }

        public string Guid { get; set; }

        public int? SortOrder { get; set; }
        public int? GroupId { get; set; }
        public int? OptionItemGroupId { get; set; }

        public string Comment { get; set; }
        #endregion

        #region Constructor

        public SubItemModel()
        {
            OptionItems= new ObservableCollection<WsMenuItem>();
        }

        #endregion


        #region Methods

        public static SubItemModel CreateSubItem(WsMenuSubItem subItemSelection, bool selectByDefault)
        {
            var item = new SubItemModel
            {
                OptionItems = new ObservableCollection<WsMenuItem>(subItemSelection.Selections),
                Description = subItemSelection.Description,
                SortOrder= subItemSelection.SortOrder,
                GroupId = subItemSelection.GroupId,
                OptionItemGroupId = subItemSelection.OptionItemGroupId,
                Name = subItemSelection.Name,
                Guid = subItemSelection.Guid,
               
            };

            if (subItemSelection.Selected != null)
                item.SelectedOptionExtraItem = subItemSelection.Selections.FirstOrDefault(r => r.Id == subItemSelection.Selected.Id);
            else if (selectByDefault)
                item.SelectedOptionExtraItem = subItemSelection.Selections.FirstOrDefault();


            return item;
        }

        #endregion
    }
}