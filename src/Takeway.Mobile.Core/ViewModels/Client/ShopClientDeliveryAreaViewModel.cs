using Takeway.Mobile.Core.Utilities;

namespace Takeway.Mobile.Core.ViewModels.Client
{
    public class ShopClientDeliveryAreaViewModel : BaseViewModel
    {
        public string HelpHtmlText
        {
            get
            {
                return string.Format("{0}/home/deliveryArea", Constants.SiteUrl);
            }
        }
    }
}