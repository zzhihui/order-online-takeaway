using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Web.ViewModels;

namespace Takeway.Web.Models.OnlineDash
{
    public class OnlineDashViewModel: ViewModelBase
    {
        public WsShop ShopInfo { get; set; }


        [Display(Name = "Shop Open")]
        public bool IsOpen { get; set; }

        public List<WsOrder> OrdersToday { get; set; }
        public WsOrder SelectedOrder { get; set; }
     
        public OnlineDashViewModel()
        {
            OrdersToday= new List<WsOrder>();
        }

    }
}