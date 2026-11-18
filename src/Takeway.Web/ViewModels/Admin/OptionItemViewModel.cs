using System.Collections.Generic;
using Takeaway.Service.Contract.Entities.Authentication;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeway.Web.ViewModels.Admin
{
    public class OptionItemViewModel : ViewModelBase
    {
        public List<WsOptionItem> OptionItems { get; set; }
        public WsOptionItem SelectedOptionItem { get; set; }
    }
}