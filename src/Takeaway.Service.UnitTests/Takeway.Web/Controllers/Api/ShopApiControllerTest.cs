//using Core.Common.Core;
//using Core.Test.Utilities.Fixtures;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using Takeaway.Service.Contract.Entities.Shop;
//using Takeaway.Service.Contract.Enum;
//using Takeaway.Service.Services;
//using Takeway.Web.Controllers.Api;

//namespace Takeaway.Service.UnitTests.Takeway.Web.Controllers.Api
//{
//    [TestClass]
//    public class ShopApiControllerTest : AutoMockFixture<ShopApiController>
//    {
//        [TestMethod]
//        public void GetShopInfo_Verify()
//        {

//            var act = Sut.GetShopInfo();
//            Container.GetMock<IShopService>().Verify(s=>s.GetShopInfo(),Times.Once());
//            Container.GetMock<IUnitOfWork>().Verify(s => s.Commit(), Times.Never());
//        }

//        [TestMethod]
//        public void GetMenuItemCategory_Verify()
//        {
//            var act = Sut.GetMenuItemCategoryByGroupId((int)MenuItemCategoryGroupEnum.InternalStandard);
//            Container.GetMock<IShopService>().Verify(s => s.GetMenuItemCategoryByGroupId(It.IsAny<int>()), Times.Once());
//            Container.GetMock<IUnitOfWork>().Verify(s => s.Commit(), Times.Never());
//        }


//        [TestMethod]
//        public void GetMenuItemsByCategory_Verify()
//        {
//            var act = Sut.GetMenuItemsByCategory(It.IsAny<int>());
//            Container.GetMock<IShopService>().Verify(s => s.GetMenuItemsByCategory(It.IsAny<int>()), Times.Once());
//            Container.GetMock<IUnitOfWork>().Verify(s => s.Commit(), Times.Never());
//        }

//        [TestMethod]
//        public void SubmitCustomerOrder_Verify()
//        {
//            var act = Sut.SubmitCustomerOrder(It.IsAny<WsOrder>());
//            Container.GetMock<IShopService>().Verify(s => s.SubmitCustomerOrder(It.IsAny<WsOrder>()), Times.Once());
     
//        }

//        [TestMethod]
//        public void GetOpenOrders_Verify()
//        {
//            var act = Sut.GetOpenOrders();
//            Container.GetMock<IShopService>().Verify(s => s.GetOpenOrders(), Times.Once());
//            Container.GetMock<IUnitOfWork>().Verify(s => s.Commit(), Times.Never());

//        }

//        [TestMethod]
//        public void GetOrderById_Verify()
//        {
//            var act = Sut.GetOrderById(It.IsAny<int>());
//            Container.GetMock<IShopService>().Verify(s => s.GetOrderById(It.IsAny<int>()), Times.Once());
//            Container.GetMock<IUnitOfWork>().Verify(s => s.Commit(), Times.Never());

//        }

//        [TestMethod]
//        public void CloseOrderById_Verify()
//        {
//            var act = Sut.CloseOrderById(It.IsAny<WsOrder>());
//            Container.GetMock<IShopService>().Verify(s => s.CloseOrderById(It.IsAny<WsOrder>()), Times.Once());
            

//        }

//        [TestMethod]
//        public void CancelOrderById_Verify()
//        {
//            var act = Sut.CancelOrderById(It.IsAny<WsOrder>());
//            Container.GetMock<IShopService>().Verify(s => s.CancelOrderById(It.IsAny<WsOrder>()), Times.Once());
            

//        }

//        [TestMethod]
//        public void GetLatestOffer_Verify()
//        {
//            var act = Sut.GetLatestOffer(It.IsAny<int>(), It.IsAny<int>());
//            Container.GetMock<IShopService>().Verify(s => s.GetLatestOffer(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
//            Container.GetMock<IUnitOfWork>().Verify(s => s.Commit(), Times.Never());

//        }


//        [TestMethod]
//        public void GetSystemSettings_Verify()
//        {
//            var act = Sut.GetSystemSettings();
//            Container.GetMock<IShopService>().Verify(s => s.GetSystemSettings(), Times.Once());
//            Container.GetMock<IUnitOfWork>().Verify(s => s.Commit(), Times.Never());

//        }


//        [TestMethod]
//        public void GetDeliveryOptions_Verify()
//        {
//            var act = Sut.GetDeliveryOptions();
//            Container.GetMock<IShopService>().Verify(s => s.GetDeliveryOptions(), Times.Once());
//            Container.GetMock<IUnitOfWork>().Verify(s => s.Commit(), Times.Never());

//        }

//    }
//}


