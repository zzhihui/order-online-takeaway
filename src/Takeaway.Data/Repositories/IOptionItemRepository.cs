using System.Collections.Generic;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    public interface IOptionItemRepository : IDataRepository<OptionItem, int>
    {
        List<OptionItem> GetAllActive(int shopId);
    }
}