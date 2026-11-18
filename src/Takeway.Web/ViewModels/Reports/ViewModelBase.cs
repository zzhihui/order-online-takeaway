using System.Collections.Generic;
using System.Web;
using Takeaway.Service.Contract.Entities.Payment;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Web.Application.Utility;

namespace Takeway.Web.ViewModels
{
    public class ViewModelBase
    {

        #region BaseMessage
        
        public string ErrorMessage { get; set; }
        public string ConfirmationMessage { get; set; }

        #endregion

        #region Session Variables

        public WsShopCustomer ShopCustomer
        {
            get
            {
                return (WsShopCustomer)HttpContext.Current.Session[GetSessionKey(SessionKey.LoggedInCustomer)];
            }
            set
            {
                HttpContext.Current.Session[GetSessionKey(SessionKey.LoggedInCustomer)] = value;
            }
        }


        public List<WsMenuItem> SubOrderSelection
        {
            get
            {
                return (List<WsMenuItem>)HttpContext.Current.Session[GetSessionKey(SessionKey.OrderSubItemSelection)];
            }
            set
            {
                HttpContext.Current.Session[GetSessionKey(SessionKey.OrderSubItemSelection)] = value;
            }
        }

        public List<WsMenuItem> CategoryItems
        {
            get
            {
                return (List<WsMenuItem>)HttpContext.Current.Session[GetSessionKey(SessionKey.OrderCategoryItems)];
            }
            set
            {
                HttpContext.Current.Session[GetSessionKey(SessionKey.OrderCategoryItems)] = value;
            }
        }

        protected string GetSessionKey(string sessionKey)
        {
            return string.Format("{0}_{1}", HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"],
                sessionKey);
        }


        public List<WsMenuItemCategory> Categories
        {
            get
            {
                return (List<WsMenuItemCategory>)HttpContext.Current.Session[GetSessionKey(SessionKey.OrderCategories)];
            }
            set
            {
                HttpContext.Current.Session[GetSessionKey(SessionKey.OrderCategories)] = value;
            }
        }


        public List<WsDeliveryOption> DeliveryOptions
        {
            get { return (List<WsDeliveryOption>)HttpContext.Current.Session[GetSessionKey(SessionKey.OrderDeliveryOption)]; }
            set { HttpContext.Current.Session[GetSessionKey(SessionKey.OrderDeliveryOption)] = value; }
        }

        public List<WsDeliveryArea> DeliveryAreas
        {
            get { return (List<WsDeliveryArea>)HttpContext.Current.Session[GetSessionKey(SessionKey.OrderDeliveryAreas)]; }
            set { HttpContext.Current.Session[GetSessionKey(SessionKey.OrderDeliveryAreas)] = value; }
        }


        public WsOrder MyOrder
        {
            get
            {
                return (WsOrder)HttpContext.Current.Session[GetSessionKey(SessionKey.OrderMyOrder)];
            }
            set
            {
                HttpContext.Current.Session[GetSessionKey(SessionKey.OrderMyOrder)] = value;
            }
        }

        public List<WsPaymentMonth> PaymentMonths
        {
            get
            {
                return (List<WsPaymentMonth>)HttpContext.Current.Session[GetSessionKey(SessionKey.PaymentPaymentMonths)];
            }
            set
            {
                HttpContext.Current.Session[GetSessionKey(SessionKey.PaymentPaymentMonths)] = value;
            }

        }
        public List<WsPaymentYear> PaymentYears
        {
            get
            {
                return (List<WsPaymentYear>)HttpContext.Current.Session[GetSessionKey(SessionKey.PaymentPaymentYear)];
            }
            set
            {
                HttpContext.Current.Session[GetSessionKey(SessionKey.PaymentPaymentYear)] = value;
            }
        }


        #endregion


        
    }
}