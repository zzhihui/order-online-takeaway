using System.Collections.Specialized;
using System.Configuration;

namespace Core.Common.Core
{

    public interface IAppSettingInstance
    {
        NameValueCollection AppSetting { get; set; }
        string GetSetting(string settingKey);
    }

    public class AppSettingInstance : IAppSettingInstance
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