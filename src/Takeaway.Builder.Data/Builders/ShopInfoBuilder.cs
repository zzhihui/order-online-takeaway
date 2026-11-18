using System;
using System.Collections.Generic;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Builder.Data.Builders
{
    public class ShopInfoBuilder
    {

        private readonly ShopInfo _entity = new ShopInfo();

        public ShopInfoBuilder()
        {
            _entity.Created = DateTime.Now;
            _entity.Name = "Fusion";
            _entity.Description = "";//Not Being Userd
            _entity.ShopNumber = "0144443";
            _entity.ShopMinOrder = Convert.ToDecimal(12);
            _entity.ShopMobile = "0879188888";
            _entity.ShopWeb = "http://www.sina.com.cn";
            _entity.ShopImage = "https://indasoftfusion.azurewebsites.net/content/background2.jpg";
            _entity.Address1 = "1 St Agnes Rd Crumlin village";
            _entity.Address2 = "Dublin Crumlin";
            _entity.SpecialOffer = string.Format("We have this A on sale.{0}We have this A on sale.You can buy one get one free{0}Free chips", Environment.NewLine);

        }


        public ShopInfoBuilder WithShopNews(List<ShopNews> customisation = null)
        {

            if (customisation == null)
            {


                _entity.ShopNews = new List<ShopNews>()
                {
                    new ShopNews() {News = "We closed on ", Created = DateTime.Today.AddDays(-3)},
                    new ShopNews() {News = "New special offer on this", Created = DateTime.Today.AddDays(-5)},
                    new ShopNews() {News = "Let you know special offer", Created = DateTime.Today.AddDays(-6)},
                    new ShopNews() {News = "This is not there", Created = DateTime.Today.AddDays(-8)},

                };
            }
            else
            {
                _entity.ShopNews = customisation;
            }
            return this;
        }

        public ShopInfo Build()
        {
            return _entity;
        }
    }
}