using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using Takeaway.Admin.Util.DbContexts;
using Takeaway.Admin.Util.Entities.Shops;

namespace Takeaway.Admin.Util.Repositories
{
    public interface IOptionItemRepository : IDataRepository<OptionItem, int>
    {
      

    }


    public class OptionItemRepository : DataRepository<TakeawayContext, OptionItem, int>,IOptionItemRepository
    {
        public OptionItemRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        
    }
}