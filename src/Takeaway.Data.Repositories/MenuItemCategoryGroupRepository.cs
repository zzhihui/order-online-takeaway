using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    [Repository]
    public class MenuItemCategoryGroupRepository : DataRepository<TakeawayContext, MenuItemCategoryGroup, int>, IMenuItemCategoryGroupRepository
    {
        public MenuItemCategoryGroupRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }


        public List<MenuItemCategoryGroup> GetMenuItemCategoryGroup(int shopId)
        {
            return GetMany(r => r.Isactive.HasValue && r.Isactive.Value && r.ShopId==shopId).OrderByDescending(s => s.SortOrder).ToList();
        }
    }

}