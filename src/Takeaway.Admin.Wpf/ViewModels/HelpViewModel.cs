using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Takeway.Mobile.Core.Utilities;

namespace Takeaway.Admin.Wpf.ViewModels
{

    public interface IHelpViewModel : IScreen
    {

    }
    [OfflineViewModel]
    public class HelpViewModel : BaseViewModel, IHelpViewModel
    {
        #region Properties

        private string _webAddress;
        public string WebAddress
        {
            get
            {
                return this._webAddress;
            }
            set
            {
                if (value != this._webAddress)
                {
                    this._webAddress = value;
                    NotifyOfPropertyChange(() => WebAddress);
                }
            }
        }

        #endregion

        #region Constructor

        public HelpViewModel()
        {

            WebAddress = string.Format("{0}/home/helpadmin", Constants.SiteUrl);
        }

        #endregion
    }
}