using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Resources;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Admin.Wpf.Utility.Extension;
using Takeaway.Admin.Wpf.Validation;
using Takeaway.Service.Contract.Enum;


namespace Takeaway.Admin.Wpf.ViewModels
{
    public interface ILoginViewModel : IScreen
    {
        string UserPin { get; set; }
    }


    [OfflineViewModel]
    public class LoginViewModel : BaseViewModel, ILoginViewModel
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

        #endregion

        #region Constructor

        private readonly IOfflineShopService _service;
        private readonly IAppScope _appScope;
        private readonly IEventAggregator _eventAggregator;
        private readonly IValidationHelper _validationHelper;

        public LoginViewModel(IEventAggregator eventAggregator, IOfflineShopService service, IAppScope appScope, IValidationHelper validationHelper)
        {
            _eventAggregator = eventAggregator;
            _service = service;
            _appScope = appScope;
            _validationHelper = validationHelper;
        }

        #endregion

        #region Events

        public void BtLogin()
        {
            Async(() =>
            {


                var result = _validationHelper.Validate<LoginViewModelValidator, LoginViewModel>(this);
                if (!result.IsValid)
                {
                    ErrorMessage = result.ToErrorString();
                    return;
                }


                var shopAgent = _appScope.AllUsers.FirstOrDefault(r=>r.Pin== UserPin);
                if (shopAgent == null || shopAgent.Id == 0)
                {
                    ErrorMessage = StringResource.Login_InvalidLogon;
                    return;
                }

                _appScope.CurrentAgent = shopAgent;

                switch (shopAgent.UserGroupId)
                {
                    case (int)WsUserGroupIdEnum.Admin:
                        _appScope.IsAdmin = true;
                        _appScope.IsManager = true;
                        break;
                    case (int)WsUserGroupIdEnum.Manager:
                        _appScope.IsAdmin = false;
                        _appScope.IsManager = true;
                        break;
                    default:
                        _appScope.IsAdmin = false;
                        _appScope.IsManager = false;
                        break;
                }

                _eventAggregator.Publish(IoC.Get<IMainViewModel>(), action => Task.Factory.StartNew(action));
            });
        }

        public void BtShowSoftKey()
        {
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.System) + Path.DirectorySeparatorChar + "osk.exe");
        }
        public void ExecuteFilterView(ActionExecutionContext context)
        {
            var keyArgs = context.EventArgs as KeyEventArgs;

            if (keyArgs != null && keyArgs.Key == Key.Enter)
            {
                BtLogin();
            }
        }


        #endregion
    }
}