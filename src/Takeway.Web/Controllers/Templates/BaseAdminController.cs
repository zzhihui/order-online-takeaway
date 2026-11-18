using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Services;
using Takeway.Web.Application.Filter;
using Takeway.Web.Application.Utility;
using Takeway.Web.Models.Admin;

namespace Takeway.Web.Controllers.Templates
{
    public interface IAdminController
    {
        ActionResult Index();
        ActionResult List();
        ActionResult KillOrderX7be();
        ActionResult KillOrderX7be(KillOrderViewModel viewModel);
        ActionResult RemoveOrderById(int orderId);
    }

    [AreaAuthorization]
    public class BaseAdminController : BaseContoller
    {
        public DateTime? KillOrdeDate
        {
            get
            {
                if (Session[GetSessionKey(SessionKey.KillOrderDate)] != null)
                    return (DateTime?)Session[GetSessionKey(SessionKey.KillOrderDate)];
                return null;
            }
            set
            {
                Session[GetSessionKey(SessionKey.KillOrderDate)] = value;
            }
        }

        public virtual ActionResult Index()
        {
            return View();
        }

      
        public virtual ActionResult List()
        {
            return View();
        }

      
        public virtual ActionResult KillOrderX7be()
        {
            var viewModel = new KillOrderViewModel();
            if (KillOrdeDate.HasValue)
                viewModel.Orders = GetCompleteOrdersByDate(KillOrdeDate.Value);

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult KillOrderX7be(KillOrderViewModel viewModel)
        {
            if (viewModel.FromDate != DateTime.MinValue)
                KillOrdeDate = viewModel.FromDate;

            viewModel.Orders = GetCompleteOrdersByDate(viewModel.FromDate);
            return View(viewModel);
        }


        private List<WsOrder> GetCompleteOrdersByDate(DateTime fromDate)
        {
            var searchFilter = new WsOrderSearch { SearchDate = fromDate };
            KillOrdeDate = searchFilter.SearchDate;
            var orders = ShopService.GetCompleteOrders(searchFilter, ShopId);
            return orders ?? new List<WsOrder>();
        }

        public virtual ActionResult RemoveOrderById(int orderId)
        {

            var order = ShopService.GetOrderById(orderId, ShopId);
            ShopService.KillOrderById(order, ShopId);

            return RedirectToAction("KillOrderX7be");

        }


    }
}