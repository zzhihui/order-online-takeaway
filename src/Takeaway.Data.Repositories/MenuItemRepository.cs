using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    [Repository]
    public class MenuItemRepository : DataRepository<TakeawayContext, MenuItem, int>, IMenuItemRepository
    {
        public MenuItemRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<MenuItem> GetActiveMenuItemsByCategoryId(int categoryId, int shopId)
        {


            var query = from m in DataContext.MenuItems
                                  .Include("menuItemSubItems")
                                  .Include("menuItemSubItems.menuSubItem")
                                  .Include("menuItemSubItems.menuSubItem.menuSubItemItems")
                        where m.Isactive == true && m.MenuItemCategoryId == categoryId
                              && m.ShopId==shopId
                        select m;





            return query.ToList().OrderByDescending(s=>s.SortOrder).ToList();
        }

        public List<MenuItem> GetHotMenuItems(int shopId)
        {
            var query = from m in DataContext.MenuItems
                                 .Include("menuItemSubItems")
                                 .Include("menuItemSubItems.menuSubItem")
                                 .Include("menuItemSubItems.menuSubItem.menuSubItemItems")
                         join c in DataContext.MenuItemCategories on m.MenuItemCategoryId equals c.Id
                        where m.Isactive == true && m.Ishot==true && c.Isactive==true && !c.IsInternal.HasValue && m.ShopId==shopId
                       select m;

            return query.OrderByDescending(s=>s.SortOrder).Take(20).ToList();
        }

        public List<MenuItem> GetHotMenuItemsInternal(int shopId)
        {
            var query = from m in DataContext.MenuItems
                .Include("menuItemSubItems")
                .Include("menuItemSubItems.menuSubItem")
                .Include("menuItemSubItems.menuSubItem.menuSubItemItems")
                join c in DataContext.MenuItemCategories on m.MenuItemCategoryId equals c.Id
                where m.Isactive == true && m.Ishot == true && c.Isactive == true && c.IsInternal == true && m.ShopId==shopId
                select m;
            return query.OrderByDescending(s => s.SortOrder).Take(20).ToList();
        }

        public List<MenuItem> GetAllActive(int shopId)
        {
            var query = from m in DataContext.MenuItems
                                 .Include("menuItemSubItems")
                                 .Include("menuItemSubItems.menuSubItem")
                                 .Include("menuItemSubItems.menuSubItem.menuSubItemItems")
                        where m.Isactive == true && m.ShopId==shopId
                        select m;
            return query.OrderBy(s=>s.ItemName).ToList();
        }

        public MenuItem GetById(int id, int shopId)
        {
            return GetOne(r => r.ShopId == shopId && r.Id == id);
        }
    }

}