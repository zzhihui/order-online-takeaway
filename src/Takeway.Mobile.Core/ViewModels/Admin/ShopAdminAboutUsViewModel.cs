using System.Windows.Input;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Email;
using Cirrious.MvvmCross.ViewModels;
using Takeway.Mobile.Core.Resources;

namespace Takeway.Mobile.Core.ViewModels.Admin
{
    public class ShopAdminAboutUsViewModel : BaseViewModel
    {
        #region Properties

        public ICommand ContactIndaSoftCommand
        {
            get
            {
                return new MvxCommand(OnContactIndaSoftCommand);
            }
        }

        #endregion

        #region Constructor

        private readonly IGlobalModel _globalModel;

        public ShopAdminAboutUsViewModel(IGlobalModel globalModel)
        {
            _globalModel = globalModel;
        }
        #endregion

        #region Methods

        private void OnContactIndaSoftCommand()
        {

            var indaSoftEmail = _globalModel.GetIndaSoftEmail();

            Mvx.Resolve<IMvxComposeEmailTask>().ComposeEmail(indaSoftEmail,
                  string.Empty,
                  StringResource.page_abouus_emailtitle,
                  StringResource.page_abouus_emailbody,
                  false);
        }

        #endregion

    }
}