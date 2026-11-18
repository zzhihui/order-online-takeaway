using System;
using System.Collections.Generic;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Builder.WsDataContract.Builders
{
    public class WsShopBuilder
    {
        private readonly WsShop _entity;

        public WsShopBuilder()
        {
            _entity = new WsShop
            {
                ShopName = "Demo Restaurant",
                ShopNumber = "(0)15988109",
                ShopMobile = "0870966688",
                ShopAddress1 = "139 Parnell Street Dublin 1",
                ShopAddress2 = "Dublin",
                IsOpen = true,
                ShopUrl = "www.ichiban.ie",
                ShopBackGroundImagePath = "https://www.ichiban.ie/content/background2.jpg",
                MinOrder = 0,
                ShopSpecialOffer = string.Format("We have this A on sale.{0}We have this A on sale.You can buy one get one free{0}Free chips", Environment.NewLine),
                ShopOpenHours = string.Format("Mo 5pm-3am{0}Tu 5pm-3am{0}We 5pm-3am{0}Th 5pm-3am{0}Fr 5pm-3am{0}Sa 5pm-3am{0}Su 5pm-3am", Environment.NewLine),

            };
        }

        public WsShopBuilder WithWsShopNewsParameters(List<WsShopNews> customisation = null)
        {

            if (customisation == null)
            {


                _entity.ShopNews = new List<WsShopNews>()
                {
                    new WsShopNews() {News = "We closed on ", Created = DateTime.Today.AddDays(-3)},
                    new WsShopNews() {News = "New special offer on this", Created = DateTime.Today.AddDays(-5)},
                    new WsShopNews() {News = "Let you know special offer", Created = DateTime.Today.AddDays(-6)},
                    new WsShopNews() {News = "This is not there", Created = DateTime.Today.AddDays(-8)},

                };
            }
            else
            {
                _entity.ShopNews = customisation;
            }
            return this;
        }

        public WsShop Build()
        {
            return _entity;
        }



    }
}