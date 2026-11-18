using System;
using System.Collections.Generic;
using System.Linq;
using Core.Common.Infrastructure.Autofac;
using ServiceStack.Redis;
using Takeaway.Service.Contract.Entities.Settings;

namespace Takeaway.Admin.Util.CacheRepository
{


    public interface IWsSettingRepository
    {



        void RemoveAllSettings();
        void SaveAllSettings(List<WsSetting> lists);
        List<WsSetting> GetAllSettings();
    }

    [OfflineRepository]
    public class WsSettingRepository : BaseCacheRepository, IWsSettingRepository
    {



        public void RemoveAllSettings()
        {
            using (var redisClient = new RedisClient(Host))
            {
                var items = redisClient.As<WsSetting>();
                items.DeleteAll();
            }
        }

        public void SaveAllSettings(List<WsSetting> lists)
        {
            using (var redisClient = new RedisClient(Host))
            {
                var items = redisClient.As<WsSetting>();
                foreach (var setting in lists)
                {
                    items.Store(setting);

                }

            }
        }

        public List<WsSetting> GetAllSettings()
        {
            using (var redisClient = new RedisClient(Host))
            {
                var items = redisClient.As<WsSetting>();
                return items.GetAll().ToList();
            }
        }
    }
}