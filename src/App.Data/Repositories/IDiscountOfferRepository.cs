using System.Collections.Generic;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    public interface IDiscountOfferRepository : IDataRepository<DiscountOffer, int>
    {
        List<DiscountOffer> GetAll(int shopId);
    }
}