using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Net4.Utility;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Enum;
using ILog = log4net.ILog;
using LogManager = log4net.LogManager;

namespace Takeaway.Admin.Wpf.ViewModels
{
    public interface IAboutUsViewModel : IScreen
    {

    }

    [OfflineViewModel]
    public class AboutUsViewModel : BaseViewModel, IAboutUsViewModel
    {
        private static readonly ILog Logger = LogManager.GetLogger("AboutUsViewModel");

        private string _versionNumber;
        public string VersionNumber
        {
            get { return _versionNumber; }
            set
            {
                _versionNumber = value;
                NotifyOfPropertyChange(() => VersionNumber);

            }
        }

        private string _hwid;
        public string Hwid
        {
            get { return _hwid; }
            set
            {
                _hwid = value;
                NotifyOfPropertyChange(() => Hwid);

            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyOfPropertyChange(() => Email);

            }
        }
      
        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                NotifyOfPropertyChange(() => PhoneNumber);

            }
        }


        private readonly IAppScope _appScope;
        private readonly IOnlineService _onlineService;
        private readonly IOfflineShopService _offlineShopService;
        public AboutUsViewModel(IAppScope appScope, IOnlineService onlineService, IOfflineShopService offlineShopService)
        {
            _appScope = appScope;
            _onlineService = onlineService;
            _offlineShopService = offlineShopService;

            InitView();
        }

        private void InitView()
        {
            Logger.DebugFormat("[InitView] GetSystemSettings from Cloud");

            Async(() =>
            {
                Email = _appScope.Settings.FirstOrDefault(r => r.Name == WsOfflineSettingEnum.IndaSoftEmail)?.SettingValue;
                PhoneNumber = _appScope.Settings.FirstOrDefault(r => r.Name == WsOfflineSettingEnum.IndaSoftPhone)?.SettingValue;
                Hwid = ProductLib.Generate();
                VersionNumber = typeof(ShellViewModel).Assembly.GetName().Version.ToString();
            });
        }

        public void DoUpdate()
        {
            Async(() =>
            {
                SyncShopInfo();
                SyncSetting();
                SyncShopUser();

            }, () => { Application.Current.Shutdown(0); });
        }

        private void SyncShopUser()
        {

            var lists = _onlineService.GetShopUsers();
            if (lists == null || lists.Count == 0)
            {
                Logger.ErrorFormat("Fail to SyncTableAlias");
                return;
            }
            _offlineShopService.SyncShopUsers(lists);
        }

        private void SyncSetting()
        {

            var lists = _onlineService.GetSettings();
            if (lists == null || lists.Count == 0)
            {
                Logger.ErrorFormat("Fail to SyncSetting");
                return;
            }
            _offlineShopService.SyncSettings(lists);
        }

        private void SyncShopInfo()
        {

            var shopInfo = _onlineService.GetShopInfo();
            if (shopInfo == null)
            {
                Logger.ErrorFormat("Faild to download Shop Info");
                return;
            }
            _offlineShopService.SyncShopInfo(shopInfo);
        }


    }
}