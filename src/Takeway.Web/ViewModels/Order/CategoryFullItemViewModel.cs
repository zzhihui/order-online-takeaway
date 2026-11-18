using System.Collections.Generic;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeway.Web.ViewModels.Order
{
    public class CategoryFullItemViewModel : ViewModelBase
    {
        public List<WsMenuItemCategory> CategoryCategories { get; set; }
    }
}