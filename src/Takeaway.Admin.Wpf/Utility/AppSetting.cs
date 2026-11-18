using System.Collections.Specialized;
using System.Configuration;

namespace Takeaway.Admin.Wpf.Utility
{

    public enum AppSettingEnum
    {
        CallerIdExePath,
        UniqueReference
    }


    public interface IAppSettings
    {
        NameValueCollection AppSetting { get; set; }
        string GetSetting(string settingKey);
    }



    public class AppSettings : IAppSettings
    {
        public NameValueCollection AppSetting
        {
            get { return ConfigurationManager.AppSettings; }
            set { }
        }

        public string GetSetting(string settingKey)
        {
            return AppSetting[settingKey];
        }
    }
}