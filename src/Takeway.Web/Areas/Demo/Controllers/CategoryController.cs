using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;
using Takeaway.Service.Services;
using Takeway.Web.Controllers.Templates;
using Takeway.Web.ViewModels.Order;

namespace Takeway.Web.Areas.Demo.Controllers
{
    public class CategoryController : BaseCategoryController
    {

        public CategoryController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }

    }
}