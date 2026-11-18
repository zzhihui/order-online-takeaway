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
        
        List<WsTables> AvailableTables { get; set; }

        List<WsTableCommand> TableCommandList { get; set; }
        List<WsPaymentType> PaymentTypes { get; set; }

        List<WsMenuItemCategory> FullCategories { get; set; }

        List<WsMenuItem> FullCategoryItems { get; set; }
        List<WsMenuItem> FullHotItemsInternal { get; set; }
        List<WsMenuItem> FullHotItemsExternal { get; set; }

        List<WsOptionItem> FulllOptionItems { get; set; }

        List<WsDeliveryOption> FullDeliveryOptions { get; set; }

        List<WsDeliveryCost> FullDeliveryCost { get; set; }

        List<WsDriver> AllDrivers { get; set; }


        bool IsAdmin { get; set; }
        bool IsManager { get; set; }

    }

    public class AppScope : IAppScope
    {
        public WsShopUser CurrentAgent { get; set; }

        public WsShop ShopInfo { get; set; }

        public List<WsShopUser> AllUsers { get; set; }


        public List<WsTables> AvailableTables { get; set; }
        public List<WsTableCommand> TableCommandList { get; set; }
        public List<WsPaymentType> PaymentTypes { get; set; }
        public List<WsMenuItemCategory> FullCategories { get; set; }
        public List<WsMenuItem> FullCategoryItems { get; set; }
        public List<WsMenuItem> FullHotItemsInternal { get; set; }
        public List<WsMenuItem> FullHotItemsExternal { get; set; }

        public List<WsOptionItem> FulllOptionItems { get; set; }
        public List<WsDeliveryOption> FullDeliveryOptions { get; set; }
        public List<WsDeliveryCost> FullDeliveryCost { get; set; }
        public List<WsDriver> AllDrivers  { get; set; }

        public List<WsSetting> Settings { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsManager { get; set; }

        public AppScope()
        {
            Settings= new List<WsSetting>();
            AvailableTables = new List<WsTables>();
            TableCommandList= new List<WsTableCommand>();
            PaymentTypes= new List<WsPaymentType>();
            FullCategories= new List<WsMenuItemCategory>();
            FullCategoryItems= new List<WsMenuItem>();
            FullHotItemsInternal= new List<WsMenuItem>();
            FullHotItemsExternal= new List<WsMenuItem>();
            FulllOptionItems= new List<WsOptionItem>();
            FullDeliveryOptions= new List<WsDeliveryOption>();
            FullDeliveryCost= new List<WsDeliveryCost>();
            AllDrivers= new List<WsDriver>();
            Settings= new List<WsSetting>();
            AllUsers= new List<WsShopUser>();


        }


    }
}