using System;
using System.Collections.Generic;
using System.Linq;
using Core.Common.Infrastructure.Autofac;
using ServiceStack.Redis;
using Takeaway.Service.Contract.Entities.Settings;

namespace Takeaway.Admin.Util.CacheRepository
{


    public interface IWsOfflineSettingRepository
    {
        List<WsOfflineSetting> GetAllSettings();
        void SaveSetting(string settingKey, string valueString);
        WsOfflineSetting GetSettingByKey(string settingKey);


    }

    [OfflineRepository]
    public class WsOfflineSettingRepository : BaseCacheRepository, IWsOfflineSettingRepository
    {

        public List<WsOfflineSetting> GetAllSettings()
        {
            using (var redisClient = new RedisClient(Host))
            {
                var items = redisClient.As<WsOfflineSetting>();
                return items.GetAll().ToList();

            }
        }

        public void SaveSetting(string settingKey, string valueString)
        {
            using (var redisClient = new RedisClient(Host))
            {
                var items = redisClient.As<WsOfflineSetting>();
                var selectedItem = items.GetAll().FirstOrDefault(x => x.Name == settingKey);
                if (selectedItem != null)
                {
                    items.Delete(selectedItem);
                }
                var newSetting = new WsOfflineSetting()
                {
                    Id = (int)items.GetNextSequence(),
                    Name = settingKey,
                    SettingValue = valueString,
                    Created = DateTime.Now
                };
                items.Store(newSetting);

            }
        }

        public WsOfflineSetting GetSettingByKey(string settingKey)
        {
            using (var redisClient = new RedisClient(Host))
            {
                var items = redisClient.As<WsOfflineSetting>();
                return items.GetAll().FirstOrDefault(r => r.Name == settingKey);
            }
        }


    }
}