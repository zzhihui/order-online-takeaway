using System.Linq;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Resources;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Enum;


namespace Takeaway.Admin.Wpf.ViewModels
{
    public interface IPinViewModel : IScreen
    {
        string UserPin { get; set; }
        bool HaveRights { get; set; }
        bool IsAdmin { get; set; }
    }


    [OfflineViewModel]
    public class PinViewModel : BaseViewModel, IPinViewModel
    {

        #region Properties

        private string _userPin;
        public string UserPin
        {
            get
            {
                return this._userPin;
            }
            set
            {
                if (value != this._userPin)
                {
                    this._userPin = value;
                    NotifyOfPropertyChange(() => UserPin);
                }
            }
        }

        private bool _haveRights;
        public bool HaveRights
        {
            get
            {
                return this._haveRights;
            }
            set
            {
                if (value != this._haveRights)
                {
                    this._haveRights = value;
                    NotifyOfPropertyChange(() => HaveRights);
                }
            }
        }

        #endregion

        #region Contructors

        private readonly IOfflineShopService _service;
        private readonly IAppScope _appScope;
        public PinViewModel(IOfflineShopService service, IAppScope appScope)
        {
            _service = service;
            _appScope = appScope;
            HaveRights = false;
            IsAdmin = false;
        }

        #endregion

        #region Methods

        public void DoEnterPin()
        {
            if (string.IsNullOrEmpty(UserPin))
            {
                ErrorMessage = StringResource.Login_InvalidLogon;
                return;
            }

            var user = _appScope.AllUsers.FirstOrDefault(r => r.Pin == UserPin);

            if (user == null || user.UserGroupId == (int)WsUserGroupIdEnum.User)
            {
                ErrorMessage = StringResource.Login_InvalidLogon;
                return;
            }

            HaveRights = true;

            if (user.UserGroupId == (int)WsUserGroupIdEnum.Admin)
                IsAdmin = true;

            TryClose();

        }

        public void ClearEntry()
        {
            UserPin = string.Empty;
        }

        public void EnterValue(string enterValue)
        {
            UserPin += enterValue;
        }

        #endregion
    }
}