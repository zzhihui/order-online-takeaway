using System;
using System.Collections.Generic;
using Core.Test.Utilities.Utilities;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Builder.Data.Builders
{
    public class OrderBuilder
    {

        private readonly Order _entity = new Order();

        public OrderBuilder()
        {
            _entity.Created = DateTime.Now;
            _entity.OrderTotal = Convert.ToDecimal(100);
            _entity.OrderOriginalTotal = Convert.ToDecimal(100);
            _entity.ContactName = "customer contact name";
            _entity.PhoneNumber = "0871111111";
            _entity.Address = "customer test address";
            _entity.Comment = "customer final comment";
            _entity.StatusId = 1;


        }


        public OrderBuilder WithDeliveryOption(DeliveryOption custom=null)
        {
            _entity.DeliveryOption = custom ?? ObjectMother.CreateAndFill<DeliveryOption>();
            return this;
        }


        public OrderBuilder WithOrderItems(List<OrderItem> custom = null)
        {
            if (custom == null)
            {
                _entity.OrderItems = new List<OrderItem>()
                {
                    ObjectMother.CreateAndFill<OrderItem>(),
                    ObjectMother.CreateAndFill<OrderItem>(),
                    ObjectMother.CreateAndFill<OrderItem>(),
                    ObjectMother.CreateAndFill<OrderItem>(),
                };

            }
            else
            {

                _entity.OrderItems = custom;

            }

            return this;
        }


        public Order Build()
        {
            return _entity;
        }
    }
}