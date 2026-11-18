using System.Collections.Generic;
using System.Linq;
using Core.Common.Infrastructure.Autofac;
using ServiceStack.Redis;
using Takeaway.Service.Contract.Entities.Authentication;
using Takeaway.Service.Contract.Entities.Settings;

namespace Takeaway.Admin.Util.CacheRepository
{
    public interface IWsUserRepository
    {
        List<WsShopUser> GetAllUsers();
        void RemoveAll();
        void SaveAll(List<WsShopUser> lists);
    }

    [OfflineRepository]
    public class WsUserRepository: BaseCacheRepository, IWsUserRepository
    {
        public List<WsShopUser> GetAllUsers()
        {
            using (var redisClient = new RedisClient(Host))
            {
                var items = redisClient.As<WsShopUser>();
                return items.GetAll().ToList();

            }
        }

        public void RemoveAll()
        {
            using (var redisClient = new RedisClient(Host))
            {
                var items = redisClient.As<WsShopUser>();
                items.DeleteAll();
            }
        }

        public void SaveAll(List<WsShopUser> lists)
        {
            using (var redisClient = new RedisClient(Host))
            {
                var items = redisClient.As<WsShopUser>();
                foreach (var setting in lists)
                {
                    items.Store(setting);

                }

            }
        }
    }
}