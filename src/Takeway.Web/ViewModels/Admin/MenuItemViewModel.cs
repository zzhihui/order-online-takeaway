using System.Collections.Generic;
using System.Web;
using Takeaway.Service.Contract.Entities.Authentication;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Web.Application.Utility;
using Takeway.Web.ViewModels;

namespace Takeway.Web.Models.Admin
{
    public class MenuItemViewModel: ViewModelBase
    {
        public List<WsMenuItemCategory> MenuItemCategories { get; set; }
        
        public List<WsMenuItem> MenuItemsForCategories { get; set; }

        public WsMenuItem EditMenuItem { get; set; }

        public List<WsMenuSubItem> MenuSubItems { get; set; }

        public int SelectedMenuItemId { get; set; }

        public int CategoryGroupId
        {
            get
            {
                return (int)HttpContext.Current.Session[GetSessionKey(SessionKey.OrderCategoryGroupId)];
            }
            set
            {
                HttpContext.Current.Session[GetSessionKey(SessionKey.OrderCategoryGroupId)] = value;
            }
        }

    }
}