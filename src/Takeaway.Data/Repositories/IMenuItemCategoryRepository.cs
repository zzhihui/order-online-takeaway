using System.Collections.Generic;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{


    public interface IMenuItemCategoryRepository : IDataRepository<MenuItemCategory, int>
    {
        List<MenuItemCategory> GetCategoriesByGroupId(int groupId, int shopId);
        List<MenuItemCategory> GetCategoriesByGroupIdIncNonActive(int groupId, int shopId);
    }

}