using System.Collections.Generic;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.DbContexts;
using Takeaway.Admin.Util.Entities.Shops;

namespace Takeaway.Admin.Util.Repositories
{
    public interface IDeliveryCostRepository : IDataRepository<DeliveryCost, int>
    {
       
    }

    [Repository]
    public class DeliveryCostRepository : DataRepository<TakeawayContext, DeliveryCost, int>, IDeliveryCostRepository
    {
         public DeliveryCostRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
   }

}