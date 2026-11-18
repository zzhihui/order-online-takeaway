using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Web.Application.Utility;
using Takeway.Web.Content;
using Takeway.Web.ViewModels;

namespace Takeway.Web.Models.Customer
{
    public class LoginViewModel : ViewModelBase
    {
        public WsShopCustomer SelectedCustomer {
            get
            {
                return (WsShopCustomer)HttpContext.Current.Session[GetSessionKey(SessionKey.RegsiterCustomer)];
            }
            set
            {
                HttpContext.Current.Session[GetSessionKey(SessionKey.RegsiterCustomer)] = value;
            }
        }
        public WsShopCustomer RegisteredCustomer { get; set; }
      
        [Required(ErrorMessage = "Please enter valid email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter valid email")]
        [StringLength(50, ErrorMessage = "Please enter valid email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please enter valid password")]
        [StringLength(30, MinimumLength = 7, ErrorMessage = "Password length should be less than 30 or more than 6 characters")]
        public string Password { get; set; }

        

    }
}