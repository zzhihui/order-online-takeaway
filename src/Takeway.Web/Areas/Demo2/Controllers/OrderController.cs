using System;
using System.Linq;
using System.Web.Mvc;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;
using Takeway.Web.Controllers.Templates;
using Takeway.Web.ViewModels.Order;

namespace Takeway.Web.Areas.Demo2.Controllers
{
    public class OrderController : BaseOrderController, IOrderController
    {
        
        public OrderController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }


        public override ActionResult Index()
        {
            return RedirectToAction("CategoryList");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult AddItemToBasket(AddItemToBasketViewModel viewModel)
        {
            if (!ModelState.IsValid) return View();

            InitOrderIfNull();


            for (int i = 0; i < viewModel.Quantity; i++)
            {
                var selectedItem = (WsMenuItem)viewModel.SelectedItem.Clone();

                selectedItem.Guid = Guid.NewGuid().ToString();

                foreach (var item in selectedItem.SubItemSelections)
                {
                    item.Selected = viewModel.SubOrderSelection.FirstOrDefault(r => r.Id == item.SelectedId);
                    selectedItem.Price += item.Selected.Price;
                }

                viewModel.MyOrder.OrderItems.Add(selectedItem);
            }
            //Calculate Total
            CalculateOrderTotal();

            return RedirectToAction("CategoryList");
        }

    }
}