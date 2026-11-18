using System;
using System.Collections.Generic;
using Core.Test.Utilities.Utilities;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Builder.Data.Builders
{
    public class OrderItemBuilder
    {

        private readonly OrderItem _entity = new OrderItem();


        public OrderItemBuilder()
        {
            _entity.Comment = "Order Item Comment";
            _entity.DiscountPrice = Convert.ToDecimal(12.30);
            _entity.Created = DateTime.Now;
            

        }


        public OrderItemBuilder WithOrderItemSelection(List<OrderItemsSelection> custom = null)
        {
            if (custom != null)
                _entity.OrderItemsSelections = custom;
            else 
                _entity.OrderItemsSelections= new List<OrderItemsSelection>()
                {
                    ObjectMother.CreateAndFill<OrderItemsSelection>(),
                    ObjectMother.CreateAndFill<OrderItemsSelection>(),
                    ObjectMother.CreateAndFill<OrderItemsSelection>(),
                };


            return this;
        }

        public OrderItemBuilder WithMenuItem(MenuItem custom = null)
        {
            _entity.MenuItem = custom ?? ObjectMother.CreateAndFill<MenuItem>();

            return this;
        }


        public OrderItem Build()
        {
            return _entity;
        }
    }
}