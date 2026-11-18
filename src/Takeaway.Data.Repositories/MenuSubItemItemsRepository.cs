using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{


    [Repository]
    public class MenuSubItemItemsRepository : DataRepository<TakeawayContext, MenuSubItemItem, int>, IMenuSubItemItemsRepository
    {
        public MenuSubItemItemsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }


    }

}