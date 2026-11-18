using System.Collections.Generic;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    public interface IShopNewsRepository : IDataRepository<ShopNews, int>
    {
        List<ShopNews> GetLast5News(int shopId);
    }

}