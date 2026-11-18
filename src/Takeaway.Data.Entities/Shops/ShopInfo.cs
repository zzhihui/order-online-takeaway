using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Takeaway.Data.Entities.Shops
{
    public partial class ShopInfo : BaseDataEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? ShopMinOrder { get; set; }
        public decimal? DeliveryCharge { get; set; }
        public string ShopNumber { get; set; }
        public string ShopMobile { get; set; }
        public string ShopWeb { get; set; }
        public string ShopImage { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string SpecialOffer { get; set; }

        public string Email { get; set; }

        public string ShopOpenHours { get; set; }

        public bool? IsOpen { get; set; }
        public bool? IsOpenByOwner { get; set; }
        public bool? TakePayment { get; set; }

        public string ShopDeliveryOpenHours { get; set; }
        public string GoogleMap { get; set; }

        public string IoSlink { get; set; }
        public string Androidlink { get; set; }
        public bool? Isactive { get; set; }

        public bool? ActiveSync { get; set; }

        public int? SmsBalance { get; set; }
        public decimal? RegisterDiscount { get; set; }

        public int? RequireTestOrder { get; set; }

        public List<string> CollectionOptions { get; set; }

        public virtual ICollection<ShopNews> ShopNews { get; set; }

        public virtual ICollection<ShopGeneralHour> ShopHours { get; set; }

        public virtual ICollection<ShopSpecialHour> ShopSpecialHours { get; set; }

      
        public ShopInfo()
        {

            ShopNews = new List<ShopNews>();
            ShopHours = new List<ShopGeneralHour>();
            ShopSpecialHours = new List<ShopSpecialHour>();
            CollectionOptions= new List<string>();


        }


    }
}
