using System.Collections.Generic;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{


    public interface IShopGeneralHourRepository : IDataRepository<ShopGeneralHour, int>
    {
        ICollection<ShopGeneralHour> GetShopGeneralHours(int shopId);
    }

}