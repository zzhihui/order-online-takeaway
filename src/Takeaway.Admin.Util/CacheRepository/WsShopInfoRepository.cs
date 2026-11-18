using System.Linq;
using Core.Common.Infrastructure.Autofac;
using ServiceStack.Redis;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Admin.Util.CacheRepository
{
    public interface IWsShopInfoRepository
    {
        WsShop GetShopInfo();
        void RemoveShopInfo();
        void SaveShopInfo(WsShop shopInfoTransformed);
    }

    [OfflineRepository]
    public class WsShopInfoRepository : BaseCacheRepository, IWsShopInfoRepository
    {

        private const string ListName = "urn:shopinfo";
        public WsShop GetShopInfo()
        {

            using (var redisClient = new RedisClient(Host))
            {
                var items = redisClient.As<WsShop>();
                var itemList = items.Lists[ListName];
                return itemList.GetAll().FirstOrDefault();
            }

        }

        public void RemoveShopInfo()
        {
            using (var redisClient = new RedisClient(Host))
            {
                redisClient.Remove(ListName);
            }
        }

        public void SaveShopInfo(WsShop shopInfoTransformed)
        {
            using (var redisClient = new RedisClient(Host))
            {
                var items = redisClient.As<WsShop>();
                var itemList = items.Lists[ListName];
                itemList.Add(shopInfoTransformed);


            }
        }
    }
}