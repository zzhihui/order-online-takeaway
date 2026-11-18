using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities.Licence;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    public class OptionItemRepository : DataRepository<TakeawayContext, OptionItem, int>,IOptionItemRepository
    {
        public OptionItemRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<OptionItem> GetAllActive(int shopId)
        {
            return GetMany(r => r.Isactive.HasValue && r.Isactive.Value && r.ShopId==shopId).OrderByDescending(s => s.SortOrder).ToList();
        }
    }
}