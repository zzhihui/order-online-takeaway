using System.Collections.Generic;
using System.Linq;
using ServiceStack.Redis;
using Takeaway.Admin.Util.CacheRepository;
using Takeaway.Admin.Util.Repositories;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Admin.Util.Repositories
{

    public interface IWsPhoneRecordRepository
    {
        List<WsPhoneRecord> GetAll();
        List<WsPhoneRecord> GetLast5();
        void RemoveAll();
        void SaveItem(WsPhoneRecord item);
        List<WsPhoneRecord> GetLast5ByChannel(string channel);
    }
}

public class WsPhoneRecordRepository : BaseCacheRepository, IWsPhoneRecordRepository
{
    public List<WsPhoneRecord> GetAll()
    {
        using (var redisClient = new RedisClient(Host))
        {
            var items = redisClient.As<WsPhoneRecord>();
            return items.GetAll().ToList();
        }
    }

    public List<WsPhoneRecord> GetLast5()
    {
        using (var redisClient = new RedisClient(Host))
        {
            var items = redisClient.As<WsPhoneRecord>();
            return items.GetAll().OrderByDescending(r => r.Created).Take(5).ToList();
        }
    }

    public void RemoveAll()
    {
        using (var redisClient = new RedisClient(Host))
        {
            var items = redisClient.As<WsPhoneRecord>();
            items.DeleteAll();
        }
    }

    public void SaveItem(WsPhoneRecord item)
    {
        using (var redisClient = new RedisClient(Host))
        {
            var items = redisClient.As<WsPhoneRecord>();
            item.Id = (int)items.GetNextSequence();
            items.Store(item);
        }
    }

    public List<WsPhoneRecord> GetLast5ByChannel(string channel)
    {
        using (var redisClient = new RedisClient(Host))
        {
            var items = redisClient.As<WsPhoneRecord>();
            return items.GetAll().Where(r => r.Channel == channel).OrderByDescending(r => r.Created).Take(5).ToList();
        }
    }
}
