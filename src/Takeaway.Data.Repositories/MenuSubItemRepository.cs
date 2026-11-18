using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{


    [Repository]
    public class MenuSubItemRepository : DataRepository<TakeawayContext, MenuSubItem, int>, IMenuSubItemRepository
    {
        public MenuSubItemRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }


        public MenuSubItem GetSubItemById(int id, int shopId)
        {
            var query = from m in DataContext.MenuSubItems
                               .Include("MenuSubItemItems")
                               .Include("MenuSubItemItems.MenuItem")
                        where m.Id == id && m.ShopId == shopId
                        select m;

            return query.FirstOrDefault();
        }
    }

}