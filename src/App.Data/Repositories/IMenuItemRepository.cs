using System.Collections.Generic;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{


    public interface IMenuItemRepository : IDataRepository<MenuItem, int>
    {
        List<MenuItem> GetActiveMenuItemsByCategoryId(int categoryId, int shopId);
        List<MenuItem> GetHotMenuItems(int shopId);
        List<MenuItem> GetHotMenuItemsInternal(int shopId);
        List<MenuItem> GetAllActive(int shopId);
        MenuItem GetById(int id, int shopId);
    }

}