using System.Collections.Generic;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
     [Repository]
    public class DeliveryOptionRepository : DataRepository<TakeawayContext, DeliveryOption, int>, IDeliveryOptionRepository
    {
        public DeliveryOptionRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

         public List<DeliveryOption> GetExternalDeliveryOption(int shopId)
         {
             return GetMany(r => !r.IsInternal.HasValue);
         }

         public List<DeliveryOption> GetInternalDeliveryOption(int shopId)
         {
             return GetAllEntities();
         }
    }

}