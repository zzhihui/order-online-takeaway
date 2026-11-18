using Core.Common.Infrastructure.Autofac;

namespace Takeaway.Admin.Util.CacheRepository
{

    public interface IWsOrderRepository
    {
        
    }

    [OfflineRepository]
    public class WsOrderRepository: BaseCacheRepository, IWsOrderRepository
    {
         
    }
}