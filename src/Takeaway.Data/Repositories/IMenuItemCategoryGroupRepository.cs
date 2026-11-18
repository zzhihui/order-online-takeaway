using System.Collections.Generic;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{


    public interface IMenuItemCategoryGroupRepository : IDataRepository<MenuItemCategoryGroup, int>
    {
        List<MenuItemCategoryGroup> GetMenuItemCategoryGroup(int shopId);
    }

}