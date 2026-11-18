using System.Collections.Generic;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.DbContexts;
using Takeaway.Admin.Util.Entities.Shops;

namespace Takeaway.Admin.Util.Repositories
{
    public interface IDeliveryOptionRepository : IDataRepository<DeliveryOption, int>
    {
        List<DeliveryOption> GetAll();
    }

    [Repository]
    public class DeliveryOptionRepository : DataRepository<TakeawayContext, DeliveryOption, int>, IDeliveryOptionRepository
    {
        public DeliveryOptionRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<DeliveryOption> GetAll()
        {
            return GetAllEntities();
        }
    }

}