using System;
using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    [Repository]
    public class MenuItemCategoryRepository : DataRepository<TakeawayContext, MenuItemCategory, int>, IMenuItemCategoryRepository
    {
        public MenuItemCategoryRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<MenuItemCategory> GetCategoriesByGroupId(int groupId, int shopId)
        {
            return GetMany(r => r.Isactive.HasValue && r.Isactive.Value && r.CategoryGroupId==groupId && r.ShopId==shopId).OrderByDescending(s=>s.SortOrder).ToList();
        }

        public List<MenuItemCategory> GetCategoriesByGroupIdIncNonActive(int groupId, int shopId)
        {
            return GetMany(r => r.CategoryGroupId == groupId && r.ShopId == shopId).OrderByDescending(s => s.SortOrder).ToList();
        }
    }

}