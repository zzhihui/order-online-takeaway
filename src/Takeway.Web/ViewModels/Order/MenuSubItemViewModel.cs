using System;
using System.Collections.Generic;
using Glimpse.AspNet.Tab;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Web.Application.Utility;

namespace Takeway.Web.ViewModels.Order
{
    public class MenuSubItemViewModel: ViewModelBase
    {

        public List<WsMenuSubItem> MenuSubItems { get; set; }

        public WsMenuSubItem SelectedMenuSubItem { get; set; }

        public List<WsMenuItem> MenuItems { get; set; }
        public WsMenuItem SelectedSubItemMenuItem { get; set; }

        public int  SelectedMenuItemId { get; set; }

        public decimal CostValue { get; set; }

    }
}