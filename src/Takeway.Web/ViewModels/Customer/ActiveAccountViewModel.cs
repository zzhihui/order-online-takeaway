using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Web.ViewModels;

namespace Takeway.Web.Models.Customer
{
    public class ActiveAccountViewModel: ViewModelBase
    {
        public int UserId { get; set; }
        public WsShopCustomer RegisteredCustomer { get; set; }
    }
}