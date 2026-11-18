using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.DbContexts;
using Takeaway.Admin.Util.Entities.Shops;

namespace Takeaway.Admin.Util.Repositories
{

    public interface IMenuItemCategoryRepository : IDataRepository<MenuItemCategory, int>
    {

    }
    [Repository]
    public class MenuItemCategoryRepository : DataRepository<TakeawayContext, MenuItemCategory, int>, IMenuItemCategoryRepository
    {
        public MenuItemCategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}