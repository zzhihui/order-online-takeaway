using System.Collections.Generic;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{


    public interface IShopSpecialHourRepository : IDataRepository<ShopSpecialHour, int>
    {
        List<ShopSpecialHour> GetValidSpecials(int shopId);
    }

}