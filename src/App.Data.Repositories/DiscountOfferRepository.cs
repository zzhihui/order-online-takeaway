using System.Collections.Generic;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    [Repository]
    public class DiscountOfferRepository : DataRepository<TakeawayContext, DiscountOffer, int>, IDiscountOfferRepository
    {
        public DiscountOfferRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<DiscountOffer> GetAll(int shopId)
        {
            return GetMany(r => r.ShopId == shopId);
        }
    }
}