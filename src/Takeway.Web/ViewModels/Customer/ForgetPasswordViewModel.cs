using System.ComponentModel.DataAnnotations;
using Takeway.Web.ViewModels;

namespace Takeway.Web.Models.Customer
{
    public class ForgetPasswordViewModel: ViewModelBase
    {
        [Required(ErrorMessage = "Please enter valid email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter valid email")]
        [StringLength(50, ErrorMessage = "Please enter valid email")]
        public string Email { get; set; }
    }
}