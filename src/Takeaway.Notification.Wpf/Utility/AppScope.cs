using System.Collections.Generic;
using System.Collections.ObjectModel;
using Takeaway.Service.Contract.Entities.Authentication;
using Takeaway.Service.Contract.Entities.Settings;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Admin.Wpf.Utility
{

    public interface IAppScope
    {
        WsShopUser CurrentAgent { get; set; }

        List<WsShopUser> AllUsers { get; set; }

        WsShop ShopInfo { get; set; }
      
        List<WsSetting> Settings { get; set; }
       
        List<WsOrder> LocalOrders { get; set; }


        bool IsAdmin { get; set; }
        bool IsManager { get; set; }

        int? MaxOrderId { get; set; }
        int? OpenMaxOrderId { get; set; }

    }

    public class AppScope : IAppScope
    {
        public WsShopUser CurrentAgent { get; set; }

        public WsShop ShopInfo { get; set; }

        public List<WsShopUser> AllUsers { get; set; }

        public List<WsSetting> Settings { get; set; }

        public List<WsOrder> LocalOrders { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsManager { get; set; }
        public int? MaxOrderId { get; set; }
        public int? OpenMaxOrderId { get; set; }

        public AppScope()
        {
            Settings= new List<WsSetting>();
            LocalOrders = new List<WsOrder>();
        }


    }
}