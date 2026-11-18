using System.Collections.Generic;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    public interface IShopInfoRepository:IDataRepository<ShopInfo, int>
    {
        ShopInfo GetShopInfo(int shopId);
        List<ShopInfo> GetAllShopsNeedTestOrder(int shopId);
    }
}