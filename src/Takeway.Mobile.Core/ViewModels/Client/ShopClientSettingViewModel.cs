using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using Takeway.Mobile.Core.Resources;
using Takeway.Mobile.Core.Services.DataServices;


namespace Takeway.Mobile.Core.ViewModels.Client
{
    public class ShopClientSettingViewModel : BaseViewModel
    {

        private readonly IConfigurationRepository _dataRepository;

        private bool _receiveNotification;
        public bool ReceiveNotification
        {
            get { return _receiveNotification; }
            set { _receiveNotification = value; RaisePropertyChanged(() => ReceiveNotification); }
        }



        public ICommand SaveSettingCommand
        {
            get
            {
                return new MvxCommand(OnSaveSettingCommand);
            }
        }

        public void Init()
        {
            GetSettings();
        }

        public ShopClientSettingViewModel(IConfigurationRepository dataRepository)
        {
            _dataRepository = dataRepository;

        }

        private void OnSaveSettingCommand()
        {

            _dataRepository.CreateOrUpdateKey(DbConfigurationEnum.AllowNotification.ToString(), ReceiveNotification ? "Yes" : "No");
            ConfirmationMessage = StringResource.page_setting_comfirmed;
        }

        private void GetSettings()
        {
            //Get Notification
            ReceiveNotification = _dataRepository.IsNotificationSettingOn();

        }

    }
}