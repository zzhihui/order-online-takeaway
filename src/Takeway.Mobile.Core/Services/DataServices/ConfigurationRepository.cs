using System;
using System.Collections.Generic;
using System.Linq;
using Cirrious.MvvmCross.Plugins.Sqlite;

namespace Takeway.Mobile.Core.Services.DataServices
{
    public interface IConfigurationRepository
    {
        List<DbConfigurationItem> All();

        void Add(DbConfigurationItem collectedItem);
        void Delete(DbConfigurationItem collectedItem);
        void Update(DbConfigurationItem collectedItem);
        DbConfigurationItem Get(int id);
        DbConfigurationItem GetByKey(string key);
        bool IsNotificationSettingOn();
        int GetLastOffer();
        int GetUserId();
        void CreateOrUpdateKey(string configKey, string configValue);
        string GetOrderName();
        string GetOrderAddress();
        string GetPhoneNumber();
        string GetEmail();
    }


    public class ConfigurationRepository : IConfigurationRepository
    {

        private readonly ISQLiteConnection _connection;

        public ConfigurationRepository(ISQLiteConnectionFactory factory)
        {
            _connection = factory.Create("IndaSoftTakeaway.sql");
            _connection.CreateTable<DbConfigurationItem>();

        }
        public List<DbConfigurationItem> All()
        {
            return _connection
                .Table<DbConfigurationItem>()
                .OrderByDescending(r => r.Id).ToList();

        }

        public void Add(DbConfigurationItem collectedItem)
        {
            _connection.Insert(collectedItem);
        }

        public void Delete(DbConfigurationItem collectedItem)
        {
            _connection.Delete(collectedItem);
        }

        public void Update(DbConfigurationItem collectedItem)
        {
            _connection.Update(collectedItem);
        }

        public DbConfigurationItem Get(int id)
        {
            return _connection.Get<DbConfigurationItem>(id);
        }

        public DbConfigurationItem GetByKey(string key)
        {
            return _connection.Table<DbConfigurationItem>().FirstOrDefault(r => r.ConfigKey == key);
        }

        public bool IsNotificationSettingOn()
        {
            var notificationSetting = GetByKey(DbConfigurationEnum.AllowNotification.ToString());

            return (notificationSetting == null ||
                    !notificationSetting.ConfigValue.Equals("No", StringComparison.CurrentCultureIgnoreCase));
        }

        public int GetLastOffer()
        {
            var lastOfferIdItem = GetByKey(DbConfigurationEnum.LastOfferId.ToString());
            var lastOffer = 0;
            if (lastOfferIdItem != null && !string.IsNullOrEmpty(lastOfferIdItem.ConfigValue))
                lastOffer = Convert.ToInt32(lastOfferIdItem.ConfigValue);

            return lastOffer;

        }

        public int GetUserId()
        {
            var userIdItem = GetByKey(DbConfigurationEnum.UserId.ToString());
            var userId = 0;
            if (userIdItem != null && !string.IsNullOrEmpty(userIdItem.ConfigValue))
                userId = Convert.ToInt32(userIdItem.ConfigValue);
            return userId;

        }

        public void CreateOrUpdateKey(string configKey, string configValue)
        {
            var configItem = GetByKey(configKey);
            if (configItem != null && !string.IsNullOrEmpty(configValue))
            {
                configItem.ConfigValue = configValue;
                configItem.Updated = DateTime.Now;
                Update(configItem);

            }
            else if (!string.IsNullOrEmpty(configValue))
            {
                configItem = new DbConfigurationItem { ConfigKey = configKey, ConfigValue = configValue, Created = DateTime.Now };
                Add(configItem);

            }
        }

        public string GetOrderName()
        {
            var storedName = GetByKey(DbConfigurationEnum.Name.ToString());
            return storedName != null ? storedName.ConfigValue : string.Empty;
        }

        public string GetOrderAddress()
        {
            var storedAddress = GetByKey(DbConfigurationEnum.Address.ToString());
            return storedAddress != null ? storedAddress.ConfigValue : string.Empty;
        }

        public string GetPhoneNumber()
        {
            var storedPhone = GetByKey(DbConfigurationEnum.Phone.ToString());
            return storedPhone != null ? storedPhone.ConfigValue : string.Empty;
        }

        public string GetEmail()
        {
            var storedEmail = GetByKey(DbConfigurationEnum.Email.ToString());
            return storedEmail != null ? storedEmail.ConfigValue : string.Empty;
        }
    }
}