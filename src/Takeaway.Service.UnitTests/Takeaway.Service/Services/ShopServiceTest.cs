//using System;
//using System.Collections.Generic;
//using Core.Test.Utilities.Fixtures;
//using Core.Test.Utilities.Utilities;
//using FluentValidation;
//using FluentValidation.Results;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using Takeaway.BusinessEngine.BusinessEngine;
//using Takeaway.Data.Entities.Enum;
//using Takeaway.Data.Entities.Settings;
//using Takeaway.Data.Entities.Shops;
//using Takeaway.Service.Contract.Entities.Settings;
//using Takeaway.Service.Contract.Entities.Shop;
//using Takeaway.Service.Contract.Enum;
//using Takeaway.Service.Exceptions;
//using Takeaway.Service.Services;
//using Takeaway.Service.Transformers;
//using Takeaway.Service.Transformers.In;
//using Takeaway.Service.Transformers.Out;

//namespace Takeaway.Service.UnitTests.Takeaway.Service.Services
//{
//    [TestClass]
//    public class ShopServiceTest : AutoMockFixture<ShopService>
//    {
//        [TestMethod]
//        public void GetShopInfo_Verify()
//        {

//            var wsShopFake = ObjectMother.CreateAndFill<WsShop>();
//            var shopFake = ObjectMother.CreateAndFill<ShopInfo>();

//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.GetShopInfo()).Returns(shopFake);

//            Container.GetMock<ITransformerFactory>()
//.Setup(s => s.CreateInstance<IWsShopTransformer>())
//.Returns(Container.Create<IWsShopTransformer>());
//            Container.GetMock<IWsShopTransformer>()
//              .Setup(s => s.Transform<ShopInfo, WsShop>(It.IsAny<ShopInfo>()))
//              .Returns(wsShopFake);

//            var act = Sut.GetShopInfo();
//            Assert.AreSame(act, wsShopFake);

//        }


//        [TestMethod]
//        public void GetMenuItemCategory_Success()
//        {
//            var wsObjectFake = new List<WsMenuItemCategory>()
//            {
//                  ObjectMother.CreateAndFill<WsMenuItemCategory>(),
//                  ObjectMother.CreateAndFill<WsMenuItemCategory>(),
//                  ObjectMother.CreateAndFill<WsMenuItemCategory>()
//            };


//            var objectFake = new List<MenuItemCategory>()
//            {
//                 ObjectMother.CreateAndFill<MenuItemCategory>(),
//                 ObjectMother.CreateAndFill<MenuItemCategory>(),
//                 ObjectMother.CreateAndFill<MenuItemCategory>()
//            };

//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.GetMenuItemCategoryByGroupId(It.IsAny<int>())).Returns(objectFake);


//            Container.GetMock<ITransformerFactory>()
//              .Setup(s => s.CreateInstance<IWsMenuItemCategoryTransformer>())
//              .Returns(Container.Create<IWsMenuItemCategoryTransformer>());

//            Container.GetMock<IWsMenuItemCategoryTransformer>()
//              .Setup(s => s.Transform<List<MenuItemCategory>, List<WsMenuItemCategory>>(It.IsAny<List<MenuItemCategory>>()))
//              .Returns(wsObjectFake);

//            var act = Sut.GetMenuItemCategoryByGroupId((int)MenuItemCategoryGroupEnum.InternalStandard);
//            Assert.AreSame(act, wsObjectFake);


//        }

//        [TestMethod]
//        public void GetSystemSettings_Success()
//        {

//            var wsSystemSettings = new List<WsSetting>(){
//                ObjectMother.CreateAndFill<WsSetting>(),
//                ObjectMother.CreateAndFill<WsSetting>(),
//                ObjectMother.CreateAndFill<WsSetting>(),
//            };


//            var systemSettings = new List<ServerSetting>(){
//                ObjectMother.CreateAndFill<ServerSetting>(),
//                ObjectMother.CreateAndFill<ServerSetting>(),
//                ObjectMother.CreateAndFill<ServerSetting>(),
//            };

//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.GetSystemSettings()).Returns(systemSettings);


//            Container.GetMock<ITransformerFactory>()
//            .Setup(s => s.CreateInstance<IWsSettingTransformer>())
//            .Returns(Container.Create<IWsSettingTransformer>());
//            Container.GetMock<IWsSettingTransformer>().Setup(s => s.Transform<List<ServerSetting>, List<WsSetting>>(It.IsAny<List<ServerSetting>>())).Returns(wsSystemSettings);

//            var act = Sut.GetSystemSettings();

//            Assert.AreSame(wsSystemSettings, act);

//        }


//        [ExpectedException(typeof(ServiceException))]
//        [TestMethod]
//        public void GetSystemSettings_BusinessEngineException()
//        {
//            var wsSystemSettings = new List<WsSetting>(){
//                ObjectMother.CreateAndFill<WsSetting>(),
//                ObjectMother.CreateAndFill<WsSetting>(),
//                ObjectMother.CreateAndFill<WsSetting>(),
//            };


//            var systemSettings = new List<ServerSetting>(){
//                ObjectMother.CreateAndFill<ServerSetting>(),
//                ObjectMother.CreateAndFill<ServerSetting>(),
//                ObjectMother.CreateAndFill<ServerSetting>(),
//            };

//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.GetSystemSettings()).Throws(new Exception(""));
//            Container.GetMock<IWsSettingTransformer>().Setup(s => s.Transform<List<ServerSetting>, List<WsSetting>>(It.IsAny<List<ServerSetting>>())).Returns(wsSystemSettings);

//            var act = Sut.GetSystemSettings();


//        }

//        [TestMethod]
//        [ExpectedException(typeof(ServiceException))]
//        public void GetSystemSettings_TransformerException()
//        {
//            var wsSystemSettings = new List<WsSetting>(){
//                ObjectMother.CreateAndFill<WsSetting>(),
//                ObjectMother.CreateAndFill<WsSetting>(),
//                ObjectMother.CreateAndFill<WsSetting>(),
//            };


//            var systemSettings = new List<ServerSetting>(){
//                ObjectMother.CreateAndFill<ServerSetting>(),
//                ObjectMother.CreateAndFill<ServerSetting>(),
//                ObjectMother.CreateAndFill<ServerSetting>(),
//            };

//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.GetSystemSettings()).Returns(systemSettings);
//            Container.GetMock<IWsSettingTransformer>().Setup(s => s.Transform<List<ServerSetting>, List<WsSetting>>(It.IsAny<List<ServerSetting>>())).Throws(new Exception(""));

//            var act = Sut.GetSystemSettings();
//        }



//        [TestMethod]
//        public void GetMenuItemsByCategory_Success()
//        {
//            const int categoryId = 100;
//            var wsMenuItems = new List<WsMenuItem>()
//            {
//                ObjectMother.CreateAndFill<WsMenuItem>(),
//                ObjectMother.CreateAndFill<WsMenuItem>(),
//                ObjectMother.CreateAndFill<WsMenuItem>()
//            };


//            var menuItems = new List<MenuItem>()
//            {
//                ObjectMother.CreateAndFill<MenuItem>(),
//                ObjectMother.CreateAndFill<MenuItem>(),
//                ObjectMother.CreateAndFill<MenuItem>()
//            };


//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.GetMenuItemsByCategory(categoryId)).Returns(menuItems);

//            Container.GetMock<ITransformerFactory>()
//             .Setup(s => s.CreateInstance<IWsMenuItemTransformer>())
//             .Returns(Container.Create<IWsMenuItemTransformer>());


//            Container.GetMock<IWsMenuItemTransformer>()
//                .Setup(s => s.Transform<List<MenuItem>, List<WsMenuItem>>(menuItems))
//                .Returns(wsMenuItems);

//            var act = Sut.GetMenuItemsByCategory(categoryId);
//            Assert.AreSame(wsMenuItems, act);


//        }


//        [TestMethod]
//        public void GetMenuItemsByCategory_Empty()
//        {

//            const int categoryId = 100;


//            List<MenuItem> menuItems = null;

//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.GetMenuItemsByCategory(categoryId)).Returns(menuItems);
//            Container.GetMock<IWsMenuItemTransformer>().Verify(s => s.Transform<List<MenuItem>, List<WsMenuItem>>(menuItems), Times.Never());

//            var act = Sut.GetMenuItemsByCategory(categoryId);
//            Assert.AreEqual(null, act);
//        }


//        [TestMethod]
//        [ExpectedException(typeof(ServiceException))]
//        public void GetMenuItemsByCategory_ValidateException()
//        {

//            const int categoryId = -1;

//            var act = Sut.GetMenuItemsByCategory(categoryId);

//        }


//        [TestMethod]
//        [ExpectedException(typeof(ServiceException))]
//        public void GetMenuItemsByCategory_BusinessEngineException()
//        {

//            const int categoryId = 100;
//            var wsMenuItems = new List<WsMenuItem>()
//            {
//                ObjectMother.CreateAndFill<WsMenuItem>(),
//                ObjectMother.CreateAndFill<WsMenuItem>(),
//                ObjectMother.CreateAndFill<WsMenuItem>()
//            };


//            var menuItems = new List<MenuItem>()
//            {
//                ObjectMother.CreateAndFill<MenuItem>(),
//                ObjectMother.CreateAndFill<MenuItem>(),
//                ObjectMother.CreateAndFill<MenuItem>()
//            };


//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.GetMenuItemsByCategory(categoryId)).Throws(new Exception("Unknown"));
//            Container.GetMock<IWsMenuItemTransformer>()
//                .Setup(s => s.Transform<List<MenuItem>, List<WsMenuItem>>(menuItems))
//                .Returns(wsMenuItems);

//            var act = Sut.GetMenuItemsByCategory(categoryId);

//        }

//        [TestMethod]
//        [ExpectedException(typeof(ServiceException))]
//        public void GetMenuItemsByCategory_TransformationOutException()
//        {

//            const int categoryId = 100;
//            var wsMenuItems = new List<WsMenuItem>()
//            {
//                ObjectMother.CreateAndFill<WsMenuItem>(),
//                ObjectMother.CreateAndFill<WsMenuItem>(),
//                ObjectMother.CreateAndFill<WsMenuItem>()
//            };


//            var menuItems = new List<MenuItem>()
//            {
//                ObjectMother.CreateAndFill<MenuItem>(),
//                ObjectMother.CreateAndFill<MenuItem>(),
//                ObjectMother.CreateAndFill<MenuItem>()
//            };


//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.GetMenuItemsByCategory(categoryId)).Returns(menuItems);
//            Container.GetMock<IWsMenuItemTransformer>()
//                .Setup(s => s.Transform<List<MenuItem>, List<WsMenuItem>>(menuItems))
//                .Throws(new Exception("UnKnown"));

//            var act = Sut.GetMenuItemsByCategory(categoryId);
//            Assert.AreSame(wsMenuItems, act);
//        }



//        [TestMethod]
//        public void GetOpenOrders_Success()
//        {
//            var openOrdersFake = new List<WsOrder>()
//            {
//                ObjectMother.CreateAndFill<WsOrder>(),
//                ObjectMother.CreateAndFill<WsOrder>(),
//                ObjectMother.CreateAndFill<WsOrder>(),
//                ObjectMother.CreateAndFill<WsOrder>(),

//            };

//            var openOrderObjectFake = new List<Order>()
//            {
//                ObjectMother.CreateAndFill<Order>(),
//                ObjectMother.CreateAndFill<Order>(),
//                ObjectMother.CreateAndFill<Order>(),
//            };


//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.GetOpenOrders()).Returns(openOrderObjectFake);

//            Container.GetMock<ITransformerFactory>()
//           .Setup(s => s.CreateInstance<IWsOrderTransformer>())
//           .Returns(Container.Create<IWsOrderTransformer>());

//            Container.GetMock<IWsOrderTransformer>().Setup(s => s.Transform<List<Order>, List<WsOrder>>(openOrderObjectFake)).Returns(openOrdersFake);

//            var act = Sut.GetOpenOrders();

//            Assert.AreSame(openOrdersFake, act);


//        }


//        [TestMethod]
//        [ExpectedException(typeof(ServiceException))]
//        public void GetOpenOrders_ShopEngineException()
//        {
//            var openOrdersFake = new List<WsOrder>()
//            {
//                ObjectMother.CreateAndFill<WsOrder>(),
//                ObjectMother.CreateAndFill<WsOrder>(),
//                ObjectMother.CreateAndFill<WsOrder>(),
//                ObjectMother.CreateAndFill<WsOrder>(),

//            };

//            var openOrderObjectFake = new List<Order>()
//            {
//                ObjectMother.CreateAndFill<Order>(),
//                ObjectMother.CreateAndFill<Order>(),
//                ObjectMother.CreateAndFill<Order>(),
//            };


//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.GetOpenOrders()).Throws(new Exception("Some Unknow Exception"));
//            Container.GetMock<IWsOrderTransformer>().Setup(s => s.Transform<List<Order>, List<WsOrder>>(openOrderObjectFake)).Returns(openOrdersFake);

//            var act = Sut.GetOpenOrders();

//        }



//        [TestMethod]
//        [ExpectedException(typeof(ServiceException))]
//        public void GetOpenOrders_TransformerException()
//        {
//            var openOrdersFake = new List<WsOrder>()
//            {
//                ObjectMother.CreateAndFill<WsOrder>(),
//                ObjectMother.CreateAndFill<WsOrder>(),
//                ObjectMother.CreateAndFill<WsOrder>(),
//                ObjectMother.CreateAndFill<WsOrder>(),

//            };

//            var openOrderObjectFake = new List<Order>()
//            {
//                ObjectMother.CreateAndFill<Order>(),
//                ObjectMother.CreateAndFill<Order>(),
//                ObjectMother.CreateAndFill<Order>(),
//            };


//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.GetOpenOrders()).Returns(openOrderObjectFake);

//            Container.GetMock<IWsOrderTransformer>().Setup(s => s.Transform<List<Order>, List<WsOrder>>(openOrderObjectFake)).Throws(new Exception("Some Exception"));


//            var act = Sut.GetOpenOrders();

//            Assert.AreSame(openOrdersFake, act);


//        }



//        [TestMethod]
//        public void GetOrderById_Success()
//        {
//            const int orderId = 1;
//            var fake = ObjectMother.CreateAndFill<WsOrder>();
//            var fakeOrder = ObjectMother.CreateAndFill<Order>();


//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.GetOrder(orderId)).Returns(fakeOrder);

//            Container.GetMock<ITransformerFactory>()
//.Setup(s => s.CreateInstance<IWsOrderTransformer>())
//.Returns(Container.Create<IWsOrderTransformer>());
//            Container.GetMock<IWsOrderTransformer>().Setup(s => s.Transform<Order, WsOrder>(fakeOrder)).Returns(fake);

//            var act = Sut.GetOrderById(orderId);
//            Assert.AreSame(fake, act);
//        }


//        [TestMethod]
//        [ExpectedException(typeof(ServiceException))]
//        public void GetOrderById_ValidationException()
//        {
//            const int orderId = 0;
//            var fake = ObjectMother.CreateAndFill<WsOrder>();
//            var fakeOrder = ObjectMother.CreateAndFill<Order>();


//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.GetOrder(orderId)).Returns(fakeOrder);
//            Container.GetMock<IWsOrderTransformer>().Setup(s => s.Transform<Order, WsOrder>(fakeOrder)).Returns(fake);

//            var act = Sut.GetOrderById(orderId);

//        }

//        [TestMethod]
//        [ExpectedException(typeof(ServiceException))]
//        public void GetOrderById_BusinessEngineException()
//        {
//            const int orderId = 1;
//            var fake = ObjectMother.CreateAndFill<WsOrder>();
//            var fakeOrder = ObjectMother.CreateAndFill<Order>();


//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.GetOrder(orderId)).Throws(new Exception("Some Exception"));
//            Container.GetMock<IWsOrderTransformer>().Setup(s => s.Transform<Order, WsOrder>(fakeOrder)).Returns(fake);

//            var act = Sut.GetOrderById(orderId);
//        }

//        [TestMethod]
//        public void GetOrderById_EmptyResults()
//        {
//            const int orderId = 1;
//            WsOrder fake = null;
//            var fakeOrder = ObjectMother.CreateAndFill<Order>();


//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.GetOrder(orderId)).Returns(fakeOrder);

//            Container.GetMock<ITransformerFactory>()
//         .Setup(s => s.CreateInstance<IWsOrderTransformer>())
//         .Returns(Container.Create<IWsOrderTransformer>());

//            Container.GetMock<IWsOrderTransformer>().Setup(s => s.Transform<Order, WsOrder>(fakeOrder)).Returns((WsOrder)null);

//            var act = Sut.GetOrderById(orderId);
//            Assert.IsNull(act);

//        }


//        [TestMethod]
//        [ExpectedException(typeof(ServiceException))]
//        public void GetOrderById_TransformerException()
//        {
//            const int orderId = 1;
//            var fake = ObjectMother.CreateAndFill<WsOrder>();
//            var fakeOrder = ObjectMother.CreateAndFill<Order>();


//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.GetOrder(orderId)).Returns(fakeOrder);
//            Container.GetMock<IWsOrderTransformer>()
//                .Setup(s => s.Transform<Order, WsOrder>(fakeOrder))
//                .Throws(new Exception("Some Transformer Exception"));

//            var act = Sut.GetOrderById(orderId);
//            Assert.AreSame(fake, act);
//        }


//        [TestMethod]
//        public void CloseOrderById_Success()
//        {
//            const int orderId = 1;
//            var fake = ObjectMother.CreateAndFill<WsOrder>(r=>r.Id=orderId);
//            var fakeOrder = ObjectMother.CreateAndFill<Order>();
//            //Validate
//            var validationResultFake = new ValidationResult();
//            var validatorFakeMock = Container.Create<IValidator>();

//            Container.GetMock<IValidator>()
//                .Setup(s => s.Validate(It.IsAny<ValidationContext>())).Returns(validationResultFake);

//            Container.GetMock<IValidatorFactory>()
//               .Setup(s => s.GetValidator(typeof(WsOrder)))
//               .Returns(validatorFakeMock);



//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.CloseOrder(orderId)).Returns(fakeOrder);
//            Container.GetMock<ITransformerFactory>()
//                .Setup(s => s.CreateInstance<IWsOrderTransformer>())
//                .Returns(Container.Create<IWsOrderTransformer>());
//            Container.GetMock<IWsOrderTransformer>().Setup(s => s.Transform<Order, WsOrder>(It.IsAny<Order>())).Returns(fake);

//            var act = Sut.CloseOrderById(fake);
//            Assert.AreSame(fake, act);
//        }


//        [TestMethod]
//        public void CancelOrderById_Success()
//        {
//            const int orderId = 1;
//            var fake = ObjectMother.CreateAndFill<WsOrder>(r => { r.StatusId = (int) OrderStatusIdEnum.Cancelled;
//                                                                    r.Id = orderId;
//            });
//            var fakeOrder = ObjectMother.CreateAndFill<Order>(r => r.StatusId = (int)OrderStatusIdEnum.Cancelled);


//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.CancelOrder(orderId)).Returns(fakeOrder);
//            Container.GetMock<ITransformerFactory>()
//                .Setup(s => s.CreateInstance<IWsOrderTransformer>())
//                .Returns(Container.Create<IWsOrderTransformer>());
//            Container.GetMock<IWsOrderTransformer>().Setup(s => s.Transform<Order, WsOrder>(fakeOrder)).Returns(fake);

//            //Validate
//            var validationResultFake = new ValidationResult();
//            var validatorFakeMock = Container.Create<IValidator>();

//            Container.GetMock<IValidator>()
//                .Setup(s => s.Validate(It.IsAny<ValidationContext>())).Returns(validationResultFake);

//            Container.GetMock<IValidatorFactory>()
//               .Setup(s => s.GetValidator(typeof(WsOrder)))
//               .Returns(validatorFakeMock);


//            var act = Sut.CancelOrderById(fake);
//            Assert.AreSame(fake, act);
//        }

//        [TestMethod]
//        public void SubmitCustomerOrder_Success()
//        {
//            var wsOrder = ObjectMother.CreateAndFill<WsOrder>(r=>
//            {
//                r.Id = 1;
                
//            });
//            var orderFake=ObjectMother.CreateAndFill<Order>();


//            var validationResultFake = new ValidationResult();
//            var validatorFakeMock = Container.Create<IValidator>();


//            //Validate
//            Container.GetMock<IValidator>()
//                .Setup(s => s.Validate(It.IsAny<ValidationContext>())).Returns(validationResultFake);

//            Container.GetMock<IValidatorFactory>()
//               .Setup(s => s.GetValidator(typeof(WsOrder)))
//               .Returns(validatorFakeMock);



//            Container.GetMock<ITransformerFactory>()
//                .Setup(s => s.CreateInstance<IOrderTransformer>())
//                .Returns(Container.Create<IOrderTransformer>());

//            Container.GetMock<ITransformerFactory>()
//                .Setup(s => s.CreateInstance<IWsOrderTransformer>())
//                .Returns(Container.Create<IWsOrderTransformer>());



//            Container.GetMock<IOrderTransformer>().Setup(s => s.Transform<WsOrder, Order>(wsOrder)).Returns(orderFake);
//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.SaveOrder(It.IsAny<Order>())).Returns(orderFake);
//            Container.GetMock<IWsOrderTransformer>()
//                .Setup(s => s.Transform<Order, WsOrder>(It.IsAny<Order>()))
//                .Returns(wsOrder);


//            var act = Sut.SubmitCustomerOrder(wsOrder);

//            Assert.IsNotNull(act);
//            Assert.IsTrue(act.Id>0);
//            Assert.AreEqual(wsOrder.OrderItems.Count,act.OrderItems.Count);



//        }
//        [TestMethod]
//        [ExpectedException(typeof(ServiceException))]
//        public void SubmitCustomerOrder_ShopEngineException()
//        {
//            var wsOrder = ObjectMother.CreateAndFill<WsOrder>(r => r.Id = 1);
//            var orderFake = ObjectMother.CreateAndFill<Order>();


//            var validationResultFake = new ValidationResult();
//            var validatorFakeMock = Container.Create<IValidator>();


//            //Validate
//            Container.GetMock<IValidator>()
//                .Setup(s => s.Validate(It.IsAny<WsOrder>())).Returns(validationResultFake);

//            Container.GetMock<IValidatorFactory>()
//               .Setup(s => s.GetValidator(typeof(WsOrder)))
//               .Returns(validatorFakeMock);



//            Container.GetMock<ITransformerFactory>()
//                .Setup(s => s.CreateInstance<IOrderTransformer>())
//                .Returns(Container.Create<IOrderTransformer>());

//            Container.GetMock<ITransformerFactory>()
//                .Setup(s => s.CreateInstance<IWsOrderTransformer>())
//                .Returns(Container.Create<IWsOrderTransformer>());



//            Container.GetMock<IOrderTransformer>().Setup(s => s.Transform<WsOrder, Order>(wsOrder)).Returns(orderFake);
//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.SaveOrder(It.IsAny<Order>())).Throws(new Exception("Some Shop Engine"));
//            Container.GetMock<IWsOrderTransformer>()
//                .Setup(s => s.Transform<Order, WsOrder>(It.IsAny<Order>()))
//                .Returns(wsOrder);


//            var act = Sut.SubmitCustomerOrder(wsOrder);


//        }


//        [TestMethod]
//        [ExpectedException(typeof(ServiceException))]
//        public void SubmitCustomerOrder_TransformInException()
//        {
//            var wsOrder = ObjectMother.CreateAndFill<WsOrder>(r => r.Id = 1);
//            var orderFake = ObjectMother.CreateAndFill<Order>();


//            var validationResultFake = new ValidationResult();
//            var validatorFakeMock = Container.Create<IValidator>();


//            //Validate
//            Container.GetMock<IValidator>()
//                .Setup(s => s.Validate(It.IsAny<WsOrder>())).Returns(validationResultFake);

//            Container.GetMock<IValidatorFactory>()
//               .Setup(s => s.GetValidator(typeof(WsOrder)))
//               .Returns(validatorFakeMock);



//            Container.GetMock<ITransformerFactory>()
//                .Setup(s => s.CreateInstance<IOrderTransformer>())
//                .Returns(Container.Create<IOrderTransformer>());

//            Container.GetMock<ITransformerFactory>()
//                .Setup(s => s.CreateInstance<IWsOrderTransformer>())
//                .Returns(Container.Create<IWsOrderTransformer>());



//            Container.GetMock<IOrderTransformer>()
//                .Setup(s => s.Transform<WsOrder, Order>(wsOrder))
//                .Throws(new Exception("Some In Exception"));
//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.SaveOrder(It.IsAny<Order>())).Returns(orderFake);
//            Container.GetMock<IWsOrderTransformer>()
//                .Setup(s => s.Transform<Order, WsOrder>(It.IsAny<Order>()))
//                .Returns(wsOrder);


//            var act = Sut.SubmitCustomerOrder(wsOrder);

         


//        }

//        [TestMethod]
//        [ExpectedException(typeof(ServiceException))]
//        public void SubmitCustomerOrder_TransformOutException()
//        {

//            var wsOrder = ObjectMother.CreateAndFill<WsOrder>(r => r.Id = 1);
//            var orderFake = ObjectMother.CreateAndFill<Order>();


//            var validationResultFake = new ValidationResult();
//            var validatorFakeMock = Container.Create<IValidator>();


//            //Validate
//            Container.GetMock<IValidator>()
//                .Setup(s => s.Validate(It.IsAny<WsOrder>())).Returns(validationResultFake);

//            Container.GetMock<IValidatorFactory>()
//               .Setup(s => s.GetValidator(typeof(WsOrder)))
//               .Returns(validatorFakeMock);



//            Container.GetMock<ITransformerFactory>()
//                .Setup(s => s.CreateInstance<IOrderTransformer>())
//                .Returns(Container.Create<IOrderTransformer>());

//            Container.GetMock<ITransformerFactory>()
//                .Setup(s => s.CreateInstance<IWsOrderTransformer>())
//                .Returns(Container.Create<IWsOrderTransformer>());



//            Container.GetMock<IOrderTransformer>().Setup(s => s.Transform<WsOrder, Order>(wsOrder)).Returns(orderFake);
//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.SaveOrder(It.IsAny<Order>())).Returns(orderFake);
//            Container.GetMock<IWsOrderTransformer>()
//                .Setup(s => s.Transform<Order, WsOrder>(It.IsAny<Order>()))
//                .Throws(new Exception("Some Out Transformer Exception"));


//            var act = Sut.SubmitCustomerOrder(wsOrder);

           

//        }


//        [TestMethod]
//        public void GetDeliveryOptions_Success()
//        {
//            var deliveryOption = new List<DeliveryOption>()
//            {
//                ObjectMother.CreateAndFill<DeliveryOption>(),
//                ObjectMother.CreateAndFill<DeliveryOption>(),
//                ObjectMother.CreateAndFill<DeliveryOption>(),
//            };


//            var wsDeliveryOption = new List<WsDeliveryOption>()
//            {
//                ObjectMother.CreateAndFill<WsDeliveryOption>(),
//                ObjectMother.CreateAndFill<WsDeliveryOption>(),
//                ObjectMother.CreateAndFill<WsDeliveryOption>(),
//            };


//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.GetDeliveryOptions()).Returns(deliveryOption);

//            Container.GetMock<ITransformerFactory>()
//                .Setup(s => s.CreateInstance<IWsDeliveryOptionTransformer>())
//                .Returns(Container.Create<IWsDeliveryOptionTransformer>());


//            Container.GetMock<IWsDeliveryOptionTransformer>()
//                .Setup(s => s.Transform<List<DeliveryOption>, List<WsDeliveryOption>>(deliveryOption))
//                .Returns(wsDeliveryOption);

//            var act = Sut.GetDeliveryOptions();

//            Assert.AreSame(act, wsDeliveryOption);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ServiceException))]
//        public void GetDeliveryOptions_ShopEngineException()
//        {
//            var deliveryOption = new List<DeliveryOption>()
//            {
//                ObjectMother.CreateAndFill<DeliveryOption>(),
//                ObjectMother.CreateAndFill<DeliveryOption>(),
//                ObjectMother.CreateAndFill<DeliveryOption>(),
//            };


//            var wsDeliveryOption = new List<WsDeliveryOption>()
//            {
//                ObjectMother.CreateAndFill<WsDeliveryOption>(),
//                ObjectMother.CreateAndFill<WsDeliveryOption>(),
//                ObjectMother.CreateAndFill<WsDeliveryOption>(),
//            };


//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.GetDeliveryOptions()).Throws(new Exception("Some Exception"));

//            Container.GetMock<ITransformerFactory>()
//                .Setup(s => s.CreateInstance<IWsDeliveryOptionTransformer>())
//                .Returns(Container.Create<IWsDeliveryOptionTransformer>());


//            Container.GetMock<IWsDeliveryOptionTransformer>()
//                .Setup(s => s.Transform<List<DeliveryOption>, List<WsDeliveryOption>>(deliveryOption))
//                .Returns(wsDeliveryOption);

//            var act = Sut.GetDeliveryOptions();

            
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ServiceException))]
//        public void GetDeliveryOptions_TransformOutException()
//        {
//            var deliveryOption = new List<DeliveryOption>()
//            {
//                ObjectMother.CreateAndFill<DeliveryOption>(),
//                ObjectMother.CreateAndFill<DeliveryOption>(),
//                ObjectMother.CreateAndFill<DeliveryOption>(),
//            };


//            var wsDeliveryOption = new List<WsDeliveryOption>()
//            {
//                ObjectMother.CreateAndFill<WsDeliveryOption>(),
//                ObjectMother.CreateAndFill<WsDeliveryOption>(),
//                ObjectMother.CreateAndFill<WsDeliveryOption>(),
//            };


//            Container.GetMock<IShopBusinessEngine>().Setup(s => s.GetDeliveryOptions()).Returns(deliveryOption);

//            Container.GetMock<ITransformerFactory>()
//                .Setup(s => s.CreateInstance<IWsDeliveryOptionTransformer>())
//                .Returns(Container.Create<IWsDeliveryOptionTransformer>());


//            Container.GetMock<IWsDeliveryOptionTransformer>()
//                .Setup(s => s.Transform<List<DeliveryOption>, List<WsDeliveryOption>>(deliveryOption))
//                .Throws(new Exception("Some Transform Exception"));

//            var act = Sut.GetDeliveryOptions();

//            Assert.AreSame(act, wsDeliveryOption);
//        }


//    }
//}
