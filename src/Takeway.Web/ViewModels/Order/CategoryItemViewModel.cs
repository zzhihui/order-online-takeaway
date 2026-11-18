using System.Collections.Generic;
using Takeaway.Service.Contract.Entities.Settings;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Web.ViewModels.Reports;

namespace Takeway.Web.ViewModels.Order
{
    public class CategoryItemViewModel : ViewModelBase
    {
        public int  CategoryId { get; set; }
        public WsSetting AllowOnlineOrder { get; set; }

        public List<WsMenuItemCategory> CategoryCategories { get; set; }
    }
}