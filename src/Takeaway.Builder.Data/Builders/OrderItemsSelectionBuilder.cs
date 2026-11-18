using Core.Test.Utilities.Utilities;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Builder.Data.Builders
{
    public class OrderItemsSelectionBuilder
    {
        private readonly OrderItemsSelection _entity = new OrderItemsSelection();

        public OrderItemsSelectionBuilder()
        {
            _entity.OrderItemId = 1;
            _entity.OrderItemSelectedId = 2;
        }



        public OrderItemsSelectionBuilder WithOrderSelectedItem(MenuItem custom = null)
        {
            _entity.OrderItemSelectedItem = custom ?? ObjectMother.CreateAndFill<MenuItem>();

            return this;
        }

        public OrderItemsSelection Build()
        {
            return _entity;
        }
    }
}