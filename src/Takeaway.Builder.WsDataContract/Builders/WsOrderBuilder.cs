using System;
using System.Collections.Generic;
using System.Linq;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Builder.WsDataContract.Builders
{
    public class WsOrderBuilder
    {
        private readonly WsOrder _entity;


        public WsOrderBuilder()
        {
            _entity = new WsOrder
            {
                Address = "18 SandyFord Dublin 18",
                ContactName = "Test User",
                PhoneNumber = "08799999",
                Email = "test@indasoft.ie"
            };


        }

        public WsOrderBuilder WithDeliveryOption(WsDeliveryOption customisation = null)
        {
            _entity.DeliveryType = customisation ?? new WsDeliveryOption() { Id = 1, Name = "For Delivery" };

            return this;
        }

        public WsOrderBuilder WithOrderItem(List<WsMenuItem> customisation = null)
        {

            if (customisation == null)
            {
                _entity.OrderItems = new List<WsMenuItem>()
                {
                     new WsMenuItem()
                        {
                            Comment = "Please add no onion",
                            Price = Convert.ToDecimal(9.59),
                            HasSubOrder = false,
                            Id = 1,
                            Name = "Chicken Soup",

                        },
                    new WsMenuItem()
                        {
                            Comment = "",
                            Price = Convert.ToDecimal(30),
                            HasSubOrder = true,
                            Id = 2,
                            Name = "Beef Main Course",
                            SubItemSelections = new List<WsMenuSubItem>()
                            {
                                new WsMenuSubItem()
                                {
                                    Id=1,
                                    Name = "Rice Or Chips",
                                    Selected = new WsMenuItem()
                                    {
                                        Id=10,
                                        Name = "Rice",
                                        
                                    }
                                }
                                ,
                                 new WsMenuSubItem()
                                {
                                    Id=1,
                                    Name = "Drink",
                                    Selected = new WsMenuItem()
                                    {
                                        Id=11,
                                        Name = "Cola",
                                        
                                    }
                                }
                            }


                        },
                       
                         new WsMenuItem()
                        {
                            Comment = "",
                            Price = Convert.ToDecimal(7),
                            HasSubOrder = false,
                            Id = 3,
                            Name = "Ice cream"

                        }


                };

            }
            else
            {
                _entity.OrderItems = customisation;
            }

            return this;
        }


        public WsOrder Build(int? id = null)
        {

            if (id.HasValue)
                _entity.Id = id.Value;

            if (_entity.OrderItems != null)
            {
                _entity.OrderTotal = (from item in _entity.OrderItems select item.Price).Sum() ?? 0;
                _entity.OrderOriginalTotal = _entity.OrderTotal;
            }
                

            return _entity;
        }
    }
}