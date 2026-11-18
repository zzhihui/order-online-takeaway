using System.Collections.Generic;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Web.ViewModels.Reports;

namespace Takeway.Web.ViewModels.Order
{

    public class AddItemToBasketViewModel:ViewModelBase
    {
        public WsMenuItem SelectedItem { get; set; }

        public int Quantity { get; set; }

        public List<int> Numbers = new List<int>()
        {
            1,2,3,4,5,6,7,8,9,10
        };


    }
}