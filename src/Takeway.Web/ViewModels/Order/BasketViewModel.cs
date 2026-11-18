using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Web.Application.Utility;
using Takeway.Web.Content;
using Takeway.Web.ViewModels.Reports;

namespace Takeway.Web.ViewModels.Order
{
    public class BasketViewModel:ViewModelBase
    {
        public WsShop ShopInfo { get; set; }

        public bool AllowSubmit {
            get { return CheckIfAllowSubmit(); }
        }

        public decimal MinOrder { get; set; }

        [Required(ErrorMessageResourceName = "page_order_basket_validate_nameempty", ErrorMessageResourceType = typeof(WebResource))]
        [StringLength(50, ErrorMessageResourceName = "page_order_basket_validate_nameempty", ErrorMessageResourceType = typeof(WebResource))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "page_order_basket_validate_address", ErrorMessageResourceType = typeof(WebResource))]
        [StringLength(200, ErrorMessageResourceName = "page_order_basket_validate_address", ErrorMessageResourceType = typeof(WebResource))]
        public string Address { get; set; }

        [Required(ErrorMessageResourceName = "page_order_basket_validate_phoneempty", ErrorMessageResourceType = typeof(WebResource))]
        [StringLength(50, ErrorMessageResourceName = "page_order_basket_validate_phoneempty", ErrorMessageResourceType = typeof(WebResource))]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessageResourceName = "page_order_basket_validate_email", ErrorMessageResourceType = typeof(WebResource))]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessageResourceName = "page_order_basket_validate_emailinvalid", ErrorMessageResourceType = typeof(WebResource))]
        [StringLength(50, ErrorMessageResourceName = "page_order_basket_validate_email", ErrorMessageResourceType = typeof(WebResource))]
        public string Email { get; set; }

        public string Comment { get; set; }

        public int SelectedDeliveryOptionId { get; set; }
        public int SelectedDeliveryAreaId { get; set; }


        public bool MarkingPermission { get; set; }

        public string CollectionTimeSelected { get; set; }

        public List<string> CollectionTimeOptions { get; set; }

        private bool CheckIfAllowSubmit()
        {

            if (ShopInfo == null)
                return false;
            if (ShopInfo.IsOpen.HasValue && !ShopInfo.IsOpen.Value)
                return false;
            if (MyOrder == null)
                return false;
            if(MyOrder.OrderTotal==0)
                return false;
            if (ShopInfo.MinOrder.HasValue && ShopInfo.MinOrder.Value > MyOrder.OrderTotal)
                return false;

            return true;
        }



       



    }
}