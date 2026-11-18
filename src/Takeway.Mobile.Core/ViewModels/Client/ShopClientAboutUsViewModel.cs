using System.Windows.Input;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Email;
using Cirrious.MvvmCross.ViewModels;
using Takeway.Mobile.Core.Resources;

namespace Takeway.Mobile.Core.ViewModels.Client
{
    public class ShopClientAboutUsViewModel : BaseViewModel
    {

        public ICommand ContactIndaSoftCommand
        {
            get
            {
                return new MvxCommand(OnContactIndaSoftCommand);
            }
        }

        private void OnContactIndaSoftCommand()
        {

            var globalViewModel = Mvx.Resolve<IGlobalModel>();
            var indaSoftEmail = globalViewModel.GetIndaSoftEmail();
            Mvx.Resolve<IMvxComposeEmailTask>().ComposeEmail(indaSoftEmail,
                  string.Empty,
                  StringResource.page_abouus_emailtitle,
                  StringResource.page_abouus_emailbody,
                  false);
        }
    }
}