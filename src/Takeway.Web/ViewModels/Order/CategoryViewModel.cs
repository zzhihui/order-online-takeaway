using System.Collections.Generic;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Web.ViewModels.Reports;

namespace Takeway.Web.ViewModels.Order
{
    public class CategoryViewModel : ViewModelBase
    {
        public List<WsMenuItemCategory> CategoryCategories { get; set; }

        public WsMenuItemCategory SelectedCategory { get; set; }



    }
}