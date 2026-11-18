using System;
using System.Collections.Generic;

namespace Takeaway.Service.Contract.Entities.Shop
{
    public class WsShop : WsEntity
    {
        /// <summary>
        /// Shop Name
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// Shop Phone number
        /// </summary>
        public string ShopNumber { get; set; }

        /// <summary>
        /// Shop mobile number for SMS purpose
        /// </summary>
        public string ShopMobile { get; set; }

        /// <summary>
        /// Shop URL for the QR code
        /// </summary>
        public string ShopUrl { get; set; }

        /// <summary>
        /// Shop Address 1
        /// </summary>
        public string ShopAddress1 { get; set; }

        /// <summary>
        /// Shop Address 2
        /// </summary>
        public string ShopAddress2 { get; set; }


        public string Email { get; set; }

        /// <summary>
        /// Shop Background Image Path
        /// </summary>
        public string ShopBackGroundImagePath { get; set; }
       
        public string ShopSpecialOffer { get; set; }

        public bool? IsOpen { get; set; }

        public bool? IsOpenByOwner { get; set; }

        public decimal? MinOrder { get; set; }

        public decimal? DeliveryCharge { get; set; }

        public string ShopOpenHours { get; set; }

        public bool? TakePayment { get; set; }

        public string ShopDeliveryOpenHours { get; set; }
        public string GoogleMap { get; set; }

        public string IoSlink { get; set; }
        public string Androidlink { get; set; }

        public bool? Isactive { get; set; }

        public int? SmsBalance { get; set; }

        public List<string> CollectionOptions { get; set; }

        public bool? ActiveSync { get; set; }
        public decimal? RegisterDiscount { get; set; }

        public int? RequireTestOrder { get; set; }

        public List<WsShopNews> ShopNews { get; set; }
      

        public WsShop()
        {
            ShopNews = new List<WsShopNews>();
            CollectionOptions= new List<string>();
        }
    }
}