using System.Collections.Generic;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
     [Repository]
    public class DeliveryCostRepository : DataRepository<TakeawayContext, DeliveryCost, int>, IDeliveryCostRepository
    {
         public DeliveryCostRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

         public List<DeliveryCost> GetAll(int shopId)
         {
             return GetMany(r => r.ShopId == shopId);
         }
    }

}