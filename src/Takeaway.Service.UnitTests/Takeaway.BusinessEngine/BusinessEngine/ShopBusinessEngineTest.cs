//using System;
//using System.Collections.Generic;
//using System.Runtime;
//using Core.Test.Utilities.Fixtures;
//using Core.Test.Utilities.Utilities;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using Takeaway.BusinessEngine.BusinessEngine;
//using Takeaway.Data.Entities.Authentication;
//using Takeaway.Data.Entities.Enum;
//using Takeaway.Data.Entities.Settings;
//using Takeaway.Data.Entities.Shops;
//using Takeaway.Data.Repositories;
//using Takeaway.Service.Contract.Enum;

//namespace Takeaway.Service.UnitTests.Takeaway.BusinessEngine.BusinessEngine
//{
//    [TestClass]
//    public class ShopBusinessEngineTest : AutoMockFixture<ShopBusinessEngine>
//    {

//        [TestMethod]
//        public void CalculateIsOpenMonday()
//        {

//            var shopGenHour = CreateGeneralHour();
//            var shopSpecialHour = new List<ShopSpecialHour>();
            
//            //Monday 11: 59 Close
//            var actMondayClose1 = Sut.CalculateIsOpen(shopGenHour, shopSpecialHour, new DateTime(2015, 3, 2, 11, 59, 59));
//            Assert.IsFalse(actMondayClose1);

//            //Monday 12: 00 close
//            var actMondayOpen1 = Sut.CalculateIsOpen(shopGenHour, shopSpecialHour, new DateTime(2015, 3, 2, 12, 0, 0));
//            Assert.IsTrue(actMondayOpen1);

//            //Monday 4: 00 PM Open
//            var actMondayOpen2 = Sut.CalculateIsOpen(shopGenHour, shopSpecialHour, new DateTime(2015, 3, 2, 16, 0, 0));
//            Assert.IsTrue(actMondayOpen2);

//            //Monday 23: 01 PM Close
//            var actMondayClose2 = Sut.CalculateIsOpen(shopGenHour, shopSpecialHour, new DateTime(2015, 3, 2, 23, 0, 1));
//            Assert.IsFalse(actMondayClose2);



//        }



//        [TestMethod]
//        public void CalculateIsOpenSat()
//        {

//            var shopGenHour = CreateGeneralHour();
//            var shopSpecialHour = new List<ShopSpecialHour>();

//            //Monday 11: 59 Close
//            var actMondayClose1 = Sut.CalculateIsOpen(shopGenHour, shopSpecialHour, new DateTime(2015, 3, 7, 12, 59, 59));
//            Assert.IsFalse(actMondayClose1);

//            //Monday 12: 00 close
//            var actMondayOpen1 = Sut.CalculateIsOpen(shopGenHour, shopSpecialHour, new DateTime(2015, 3, 7, 13, 0, 0));
//            Assert.IsTrue(actMondayOpen1);

//            //Monday 4: 00 PM Open
//            var actMondayOpen2 = Sut.CalculateIsOpen(shopGenHour, shopSpecialHour, new DateTime(2015, 3, 7, 23, 59, 59));
//            Assert.IsTrue(actMondayOpen2);

//            //Monday 23: 01 PM Close
//            var actMondayClose2 = Sut.CalculateIsOpen(shopGenHour, shopSpecialHour, new DateTime(2015, 3, 8, 0, 0, 1));
//            Assert.IsFalse(actMondayClose2);



//        }


//        private List<ShopGeneralHour> CreateGeneralHour()
//        {
//            return new List<ShopGeneralHour>()
//            {
//                new ShopGeneralHour()
//                {
//                    DateOfWeek = DayOfWeek.Monday.ToString(),
//                    Created = DateTime.Now,
//                    EndingTime = new DateTime(2000,1,1,23,0,0),
//                    Id=1,
//                    StartingTime =new DateTime(2000,1,1,12,0,0)

//                },
//                new ShopGeneralHour()
//                {
//                    DateOfWeek = DayOfWeek.Tuesday.ToString(),
//                    Created = DateTime.Now,
//                    EndingTime = new DateTime(2000,1,2,23,0,0),
//                    Id=1,
//                    StartingTime =new DateTime(2000,1,2,12,0,0)

//                },
//                new ShopGeneralHour()
//                {
//                    DateOfWeek = DayOfWeek.Saturday.ToString(),
//                    Created = DateTime.Now,
//                    EndingTime = new DateTime(2000,1,7,0,0,0),
//                    Id=1,
//                    StartingTime =new DateTime(2000,1,6,13,0,0)

//                },
//                new ShopGeneralHour()
//                {
//                    DateOfWeek = DayOfWeek.Sunday.ToString(),
//                    Created = DateTime.Now,
//                    EndingTime = new DateTime(2000,1,7,23,0,0),
//                    Id=1,
//                    StartingTime =new DateTime(2000,1,7,12,0,0)

//                },
//                new ShopGeneralHour()
//                {
//                    DateOfWeek = DayOfWeek.Wednesday.ToString(),
//                    Created = DateTime.Now,
//                    EndingTime = new DateTime(2000,1,3,23,0,0),
//                    Id=1,
//                    StartingTime =new DateTime(2000,1,3,12,0,0)

//                },
//                new ShopGeneralHour()
//                {
//                    DateOfWeek = DayOfWeek.Thursday.ToString(),
//                    Created = DateTime.Now,
//                    EndingTime = new DateTime(2000,1,4,23,0,0),
//                    Id=1,
//                    StartingTime =new DateTime(2000,1,4,12,0,0)
//                },
//                new ShopGeneralHour()
//                {
//                    DateOfWeek = DayOfWeek.Friday.ToString(),
//                    Created = DateTime.Now,
//                    EndingTime = new DateTime(2000,1,6,0,0,0),
//                    Id=1,
//                    StartingTime =new DateTime(2000,1,5,13,0,0)
//                },
//            };
//        }


       



//        [TestMethod]
//        public void CalculateIsOpen_NoSpecialButBeforeOpen()
//        {
//            var shopGenHour = new List<ShopGeneralHour>()
//            {
//                new ShopGeneralHour()
//                {
//                    DateOfWeek = DayOfWeek.Monday.ToString(),
//                    Created = DateTime.Now,
//                    EndingTime = new DateTime(2000,1,2,0,30,0),
//                    Id=1,
//                    StartingTime =new DateTime(2000,1,1,16,30,0)

//                },
//                new ShopGeneralHour()
//                {
//                    DateOfWeek = DayOfWeek.Tuesday.ToString(),
//                    Created = DateTime.Now,
//                    EndingTime = new DateTime(2000,1,3,0,30,0),
//                    Id=1,
//                    StartingTime =new DateTime(2000,1,2,16,30,0)

//                },
//                 new ShopGeneralHour()
//                {
//                    DateOfWeek = DayOfWeek.Saturday.ToString(),
//                    Created = DateTime.Now,
//                    EndingTime = new DateTime(2000,1,7,1,0,0),
//                    Id=1,
//                    StartingTime =new DateTime(2000,1,6,16,30,0)

//                },
//                 new ShopGeneralHour()
//                {
//                    DateOfWeek = DayOfWeek.Sunday.ToString(),
//                    Created = DateTime.Now,
//                    EndingTime = new DateTime(2000,1,8,1,0,0),
//                    Id=1,
//                    StartingTime =new DateTime(2000,1,7,16,30,0)

//                },
//                 new ShopGeneralHour()
//                {
//                    DateOfWeek = DayOfWeek.Wednesday.ToString(),
//                    Created = DateTime.Now,
//                    EndingTime = new DateTime(2000,1,4,1,0,0),
//                    Id=1,
//                    StartingTime =new DateTime(2000,1,3,16,30,0)

//                },
//                 new ShopGeneralHour()
//                {
//                    DateOfWeek = DayOfWeek.Thursday.ToString(),
//                    Created = DateTime.Now,
//                    EndingTime = new DateTime(2000,1,5,1,0,0),
//                    Id=1,
//                    StartingTime =new DateTime(2000,1,4,16,30,0)
//                },
//                 new ShopGeneralHour()
//                {
//                    DateOfWeek = DayOfWeek.Friday.ToString(),
//                    Created = DateTime.Now,
//                    EndingTime = new DateTime(2000,1,6,1,0,0),
//                    Id=1,
//                    StartingTime =new DateTime(2000,1,5,16,30,0)
//                },
//            };
//            var shopSpecialHour = new List<ShopSpecialHour>();



//            var act = Sut.CalculateIsOpen(shopGenHour, shopSpecialHour, new DateTime(2000, 1, 2, 10, 0, 0));
//            Assert.IsFalse(act);

//        }


//        [TestMethod]
//        public void GetShopInfo_Verify()
//        {


//            var shopInfoFake = ObjectMother.CreateAndFill<ShopInfo>();
//            var shopInfoRepositoryMock = Container.Create<IShopInfoRepository>();
//            var shopNewRepositoryMock = Container.Create<IShopNewsRepository>();
//            var shopGeneralHourRepositoryMock = Container.Create<IShopGeneralHourRepository>();
//            var shopSpecialHourRepositoryMock = Container.Create<IShopSpecialHourRepository>();
//            var shopNewFake = new List<ShopNews>();
//            var shopGenHour = new List<ShopGeneralHour>()
//            {
//                new ShopGeneralHour()
//                {
//                    DateOfWeek = DayOfWeek.Monday.ToString(),
//                    Created = DateTime.Now,
//                    EndingTime = DateTime.Now.AddHours(5),
//                    Id=1,
//                    StartingTime =DateTime.Now.AddHours(-2)

//                },
//                new ShopGeneralHour()
//                {
//                    DateOfWeek = DayOfWeek.Tuesday.ToString(),
//                    Created = DateTime.Now,
//                    EndingTime = DateTime.Now.AddHours(5),
//                    Id=1,
//                    StartingTime =DateTime.Now.AddHours(-2)

//                },
//                 new ShopGeneralHour()
//                {
//                    DateOfWeek = DayOfWeek.Saturday.ToString(),
//                    Created = DateTime.Now,
//                    EndingTime = DateTime.Now.AddHours(5),
//                    Id=1,
//                    StartingTime =DateTime.Now.AddHours(-2)

//                },
//                 new ShopGeneralHour()
//                {
//                    DateOfWeek = DayOfWeek.Sunday.ToString(),
//                    Created = DateTime.Now,
//                    EndingTime = DateTime.Now.AddHours(5),
//                    Id=1,
//                    StartingTime =DateTime.Now.AddHours(-2)

//                },
//                 new ShopGeneralHour()
//                {
//                    DateOfWeek = DayOfWeek.Wednesday.ToString(),
//                    Created = DateTime.Now,
//                    EndingTime = DateTime.Now.AddHours(5),
//                    Id=1,
//                    StartingTime =DateTime.Now.AddHours(-2)

//                },
//                 new ShopGeneralHour()
//                {
//                    DateOfWeek = DayOfWeek.Thursday.ToString(),
//                    Created = DateTime.Now,
//                    EndingTime = DateTime.Now.AddHours(5),
//                    Id=1,
//                    StartingTime =DateTime.Now.AddHours(-2)

//                },
//                 new ShopGeneralHour()
//                {
//                    DateOfWeek = DayOfWeek.Friday.ToString(),
//                    Created = DateTime.Now,
//                    EndingTime = DateTime.Now.AddHours(5),
//                    Id=1,
//                    StartingTime =DateTime.Now.AddHours(-2)

//                },
//            };
//            var shopSpecialHour = new List<ShopSpecialHour>();


//            Container.GetMock<IDataRepositoryFactory>()
//               .Setup(s => s.CreateInstance<IShopInfoRepository>())
//               .Returns(shopInfoRepositoryMock);

//            Container.GetMock<IDataRepositoryFactory>()
//               .Setup(s => s.CreateInstance<IShopNewsRepository>())
//               .Returns(shopNewRepositoryMock);

//            Container.GetMock<IDataRepositoryFactory>()
//               .Setup(s => s.CreateInstance<IShopGeneralHourRepository>())
//               .Returns(shopGeneralHourRepositoryMock);

//            Container.GetMock<IDataRepositoryFactory>()
//              .Setup(s => s.CreateInstance<IShopSpecialHourRepository>())
//              .Returns(shopSpecialHourRepositoryMock);


//            Container.GetMock<IShopInfoRepository>()
//                .Setup(s => s.GetShopInfo())
//                .Returns(shopInfoFake);


//            Container.GetMock<IShopNewsRepository>()
//                .Setup(s => s.GetLast5News())
//                .Returns(shopNewFake);


//            Container.GetMock<IShopGeneralHourRepository>()
//                .Setup(s => s.GetAll())
//                .Returns(shopGenHour);


//            Container.GetMock<IShopSpecialHourRepository>()
//                .Setup(s => s.GetValidSpecials())
//                .Returns(shopSpecialHour);


//            var act = Sut.GetShopInfo();
//            Assert.AreSame(shopInfoFake, act);


//        }


//        [TestMethod]
//        public void GetMenuItemCategory_Success()
//        {
//            var menuItemRepostoryMock = Container.Create<IMenuItemCategoryRepository>();

//            var menuItemCategoryFake = new List<MenuItemCategory>()
//            {
//                ObjectMother.CreateAndFill<MenuItemCategory>(),
//                ObjectMother.CreateAndFill<MenuItemCategory>()
//            };


//            //Mock Repository factor

//            Container.GetMock<IDataRepositoryFactory>()
//                .Setup(s => s.CreateInstance<IMenuItemCategoryRepository>())
//                .Returns(menuItemRepostoryMock);
//            //Mock Repository 
//            Container.GetMock<IMenuItemCategoryRepository>()
//                .Setup(s => s.GetCategoriesByGroupId(It.IsAny<int>()))
//                .Returns(menuItemCategoryFake);

//            var act = Sut.GetMenuItemCategoryByGroupId((int)MenuItemCategoryGroupEnum.InternalStandard);

//            Assert.AreSame(menuItemCategoryFake, act);

//        }


//        [TestMethod]
//        public void GetSystemSettings_Success()
//        {
//            var settingObject = new List<ServerSetting>()
//            {
//               ObjectMother.CreateAndFill<ServerSetting>(),
//               ObjectMother.CreateAndFill<ServerSetting>(),
//               ObjectMother.CreateAndFill<ServerSetting>()

//            };

//            Container.GetMock<IServerSettingRepository>().Setup(s => s.GetAll()).Returns(settingObject);

//            var act = Sut.GetSystemSettings();
//            Assert.AreSame(settingObject,act);
//        }


//         [TestMethod]
//        public void GetMenuItemsByCategory_Success()
//         {
//             const int categoryId = 100;


//            var menuItems = new List<MenuItem>()
//            {
//               ObjectMother.CreateAndFill<MenuItem>(),
//               ObjectMother.CreateAndFill<MenuItem>(),
//               ObjectMother.CreateAndFill<MenuItem>()

//            };

//            Container.GetMock<IMenuItemRepository>().Setup(s => s.GetActiveMenuItemsByCategoryId(categoryId)).Returns(menuItems);

//            var act = Sut.GetMenuItemsByCategory(categoryId);
//            Assert.AreSame(menuItems, act);
//        }

//         [TestMethod]
//         public void GetOpenOrders_Success()
//         {
             


//             var orders = new List<Order>()
//            {
//               ObjectMother.CreateAndFill<Order>(),
//               ObjectMother.CreateAndFill<Order>(),
//               ObjectMother.CreateAndFill<Order>()

//            };

//             Container.GetMock<IOrderRepository>().Setup(s => s.GetOpenOrders()).Returns(orders);

//             var act = Sut.GetOpenOrders();
//             Assert.AreSame(orders, act);
//         }

//         [TestMethod]
//         public void GetOrder_Success()
//         {

//             var orders = ObjectMother.CreateAndFill<Order>();
            
//             Container.GetMock<IOrderRepository>().Setup(s => s.GetById(It.IsAny<int>())).Returns(orders);

//             var act = Sut.GetOrder(It.IsAny<int>());
//             Assert.AreSame(orders, act);
//         }

//         [TestMethod]
//         public void CloseOrder_Success()
//         {

//             var orders = ObjectMother.CreateAndFill<Order>(r=>r.StatusId=(int)OrderStatusIdEnum.Open);

//             Container.GetMock<IOrderRepository>().Setup(s => s.GetById(It.IsAny<int>())).Returns(orders);

//             var act = Sut.CloseOrder(1);
//             Assert.IsNotNull(act);
//             Assert.AreEqual(act.StatusId, (int)OrderStatusIdEnum.Completed);
//         }

//         [TestMethod]
//         public void CancelOrder_Success()
//         {

//             var orders = ObjectMother.CreateAndFill<Order>(r => r.StatusId = (int)OrderStatusIdEnum.Open);

//             Container.GetMock<IOrderRepository>().Setup(s => s.GetById(It.IsAny<int>())).Returns(orders);

//             var act = Sut.CancelOrder(1);
//             Assert.IsNotNull(act);
//             Assert.AreEqual(act.StatusId, (int)OrderStatusIdEnum.Cancelled);
//         }

//        [TestMethod]
//        public void GetDeliveryOptions_Success()
//        {

//            var deliveryOptions = new List<DeliveryOption>()
//            {
//                ObjectMother.CreateAndFill<DeliveryOption>(),
//                ObjectMother.CreateAndFill<DeliveryOption>(),
//                ObjectMother.CreateAndFill<DeliveryOption>(),
//            };

//            Container.GetMock<IDeliveryOptionRepository>().Setup(s => s.GetExternalDeliveryOption()).Returns(deliveryOptions);

//            var act = Sut.GetDeliveryOptions();

//            Assert.AreSame(act,deliveryOptions);
//        }
//    }
//}
