using System.ComponentModel.DataAnnotations;
using System.Web;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Web.Application.Utility;
using Takeway.Web.ViewModels;

namespace Takeway.Web.Models.Customer
{
    public class VerificationViewModel : ViewModelBase
    {
       
        public WsShopCustomer RegisteredCustomer { get; set; }

        [Required(ErrorMessage = "Please enter valid mobile number")]
        [StringLength(50, ErrorMessage = "Please enter valid mobile number")]
        [RegularExpression(@"^\+353[0-9]{2}[0-9]{7}$", ErrorMessage = "Please enter valid mobile number +353871111111")]
        public string MobileNumber { get; set; }

        

    }
}