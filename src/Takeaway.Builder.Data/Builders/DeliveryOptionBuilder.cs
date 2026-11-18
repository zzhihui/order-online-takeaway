using Takeaway.Data.Entities.Shops;

namespace Takeaway.Builder.Data.Builders
{
    public class DeliveryOptionBuilder
    {

        private readonly DeliveryOption _entity = new DeliveryOption();

        public DeliveryOptionBuilder()
        {
            _entity.Name = "For Delivery";
        }

        public DeliveryOption Build()
        {
            return _entity;
        }
    }
}