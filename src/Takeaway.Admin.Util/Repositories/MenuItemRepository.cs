using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.DbContexts;
using Takeaway.Admin.Util.Entities.Shops;

namespace Takeaway.Admin.Util.Repositories
{
    public interface IMenuItemRepository : IDataRepository<MenuItem, int>
    {
  
    }


    [Repository]
    public class MenuItemRepository : DataRepository<TakeawayContext, MenuItem, int>, IMenuItemRepository
    {
        public MenuItemRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

    }

}