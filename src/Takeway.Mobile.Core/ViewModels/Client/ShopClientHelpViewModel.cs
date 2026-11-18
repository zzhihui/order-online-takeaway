using Takeway.Mobile.Core.Utilities;

namespace Takeway.Mobile.Core.ViewModels.Client
{
    public class ShopClientHelpViewModel : BaseViewModel
    {
        
        public string HelpHtmlText
        {
            get
            {
                return string.Format("{0}/home/helpClient", Constants.SiteUrl);
            }
        }
        
    }
}