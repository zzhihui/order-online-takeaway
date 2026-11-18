using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    public interface IShopInfoRepository:IDataRepository<ShopInfo, int>
    {
        ShopInfo GetShopInfo(int shopId);
    }
}