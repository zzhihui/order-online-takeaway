using System.Collections.Generic;
using System.Linq;
using Takeaway.Service.Contract.Entities.Authentication;
using Takeaway.Service.Contract.Entities.Settings;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;

namespace Takeway.Mobile.Core.ViewModels
{

    public interface IGlobalModel
    {
        List<WsSetting> WsSettings { get; set; }
        WsShopUser WsShopUser { get; set; }

        WsShop WsShopInfo { get; set; }
        WsOrder MyOrder { get; set; }

        int? CustomerId { get; set; }
        WsShopSpecialOffer LatestOffer { get; set; }

        void AddOrderItem(WsMenuItem item);
        void RemoveOrderItem(WsMenuItem item);
        void ResetOrder();
        WsSetting GetSettingByKey(string toString);
        string GetIndaSoftEmail();
        decimal GetMinPremium();

        void SetUpOrderHeader(string name, string address, string phoneNumber, string comment, string email, WsDeliveryOption deliveryOption);

        decimal? GetDeliveryCharge();
        bool IsShopClosed();
        bool NoOrderItemYet();
        bool OrderLessThanMinOrder();
        void SetOrderAsInternal();
        bool TakingPaymentOn();
        void RemoveOrderItemBuyGuid(WsMenuItem item);
    }

    public class GlobalModel : IGlobalModel
    {

        public List<WsSetting> WsSettings { get; set; }
        public WsShopUser WsShopUser { get; set; }
        public WsShop WsShopInfo { get; set; }

        public WsOrder MyOrder { get; set; }

        public int? CustomerId { get; set; }
        public WsShopSpecialOffer LatestOffer { get; set; }


        public GlobalModel()
        {
            MyOrder = new WsOrder { OrderItems = new List<WsMenuItem>() };
            WsSettings = new List<WsSetting>();
        }


        public void AddOrderItem(WsMenuItem item)
        {
            MyOrder.OrderItems.Add(item);
            CalculateTotal();
        }

        public void RemoveOrderItem(WsMenuItem item)
        {
            MyOrder.OrderItems.Remove(item);
            CalculateTotal();
        }

        public void ResetOrder()
        {
            MyOrder = new WsOrder();
        }

        public WsSetting GetSettingByKey(string settingKey)
        {
            return WsSettings.FirstOrDefault(r => r.Name == settingKey);
        }

        public string GetIndaSoftEmail()
        {
            var indaSoftEmailSetting = GetSettingByKey(WsSettingKey.IndaSoftEmail.ToString());
            var indaSoftEmail = indaSoftEmailSetting == null ? string.Empty : indaSoftEmailSetting.SettingValue;
            return indaSoftEmail;
        }

        public decimal GetMinPremium()
        {
            decimal minPremium = 0;
            if (WsShopInfo.MinOrder.HasValue)
                minPremium = WsShopInfo.MinOrder.Value;

            return minPremium;

        }

        public void SetUpOrderHeader(string name, string address, string phoneNumber, string comment, string email, WsDeliveryOption deliveryOption)
        {
            MyOrder.ContactName = name;
            MyOrder.Address = address;
            MyOrder.PhoneNumber = phoneNumber;
            MyOrder.Comment = comment;
            MyOrder.DeliveryType = deliveryOption;

            if (MyOrder.DeliveryType.Name == DeliveryOptions.ForCollection)
            {
                MyOrder.DeliveryCharge = 0;
            }
            else
            {
                if (WsShopInfo.DeliveryCharge != null) MyOrder.DeliveryCharge = WsShopInfo.DeliveryCharge.Value;
            }

            MyOrder.CustomerId = CustomerId;
            MyOrder.Email = email;
        }

        public decimal? GetDeliveryCharge()
        {
            decimal? deliveryCharge = null;
            if (WsShopInfo.DeliveryCharge.HasValue)
                deliveryCharge = WsShopInfo.DeliveryCharge.Value;

            return deliveryCharge;
        }

        public bool IsShopClosed()
        {
            return WsShopInfo.IsOpen.HasValue && !WsShopInfo.IsOpen.Value;
        }

        public bool NoOrderItemYet()
        {
            return MyOrder.OrderItems.Count == 0;
        }

        public bool OrderLessThanMinOrder()
        {
            return MyOrder.OrderTotal > GetMinPremium();
        }

        public void SetOrderAsInternal()
        {
            MyOrder.IsInternalOrder = true;
        }

        public bool TakingPaymentOn()
        {
            return WsShopInfo.TakePayment.HasValue && WsShopInfo.TakePayment.Value;
        }

        public void RemoveOrderItemBuyGuid(WsMenuItem item)
        {

            var orderitem = MyOrder.OrderItems.FirstOrDefault(r => r.Guid == item.Guid);
            if(orderitem!=null)
                MyOrder.OrderItems.Remove(orderitem);

            CalculateTotal();
        }


        private void CalculateTotal()
        {
            MyOrder.OrderTotal = MyOrder.OrderItems.Where(item => item.Price.HasValue).Sum(item => item.Price.Value);
            MyOrder.OrderOriginalTotal = MyOrder.OrderTotal;
        }


    }
}