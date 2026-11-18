//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Takeaway.Builder.WsDataContract.Builders;
//using Takeaway.Data.Entities.Enum;
//using Takeaway.Service.Contract.Entities.Payment;
//using Takeaway.Service.Contract.Entities.Reports;
//using Takeaway.Service.Contract.Entities.Shop;
//using Takeaway.Service.Contract.Enum;
//using Takeway.Mobile.Core.Services.RestServices;

//namespace Takeaway.Service.ServiceTests.Api
//{
//    [TestClass]
//    public class ShopServiceTest : ServiceTestBase
//    {
//        [TestMethod]
//        public void GetShopInfo_Test()
//        {

//            var shopService = new ShopRestService();
//            var act = shopService.GetShopInfo();

//            Assert.IsNotNull(act);
//            Assert.IsFalse(string.IsNullOrEmpty(act.ShopName));
//            Assert.IsFalse(string.IsNullOrEmpty(act.ShopNumber));
//        }

//        [TestMethod]
//        public void GetMenuItemCategory_Test()
//        {

//            var shopService = new ShopRestService();
//            var act = shopService.GetMenuItemCategoryByGroupId((int)MenuItemCategoryGroupEnum.External);

//            Assert.IsNotNull(act);
//            Assert.IsTrue(act.Count > 0);
//            Assert.IsNotNull(act[0].Name);
//            Assert.IsNotNull(act[0].Description);
//        }

//        [TestMethod]
//        public void GetMenuItemCategoryInternal_Test()
//        {

//            var shopService = new ShopRestService();
//            var act = shopService.GetMenuItemCategoryByGroupId((int)MenuItemCategoryGroupEnum.InternalStandard);

//            Assert.IsNotNull(act);
//            Assert.IsTrue(act.Count > 0);
//            Assert.IsNotNull(act[0].Name);
//            Assert.IsNotNull(act[0].Description);
//        }


//        [TestMethod]
//        public void GetMenuItemsByCategory_Test()
//        {
//            var shopService = new ShopRestService();
//            var act = shopService.GetMenuItemsByCategory(2);

//            Assert.IsNotNull(act);
//            Assert.IsTrue(act.Count > 0);
//            Assert.IsNotNull(act[0].Name);
//            Assert.IsNotNull(act[0].Description);
//        }





//        [TestMethod]
//        public void SubmitCustomerOrder_Test()
//        {
//            var shopService = new ShopRestService();
//            1. Create an Order
//            var order = CreateOrder();

//            2. Submit it 
//            var orderSubmit = shopService.SubmitCustomerOrder(order);
//            3. Assert that order id is valid
//            Assert.IsNotNull(orderSubmit);
//            Assert.IsTrue(orderSubmit.Id > 0);
//            Assert.IsTrue(orderSubmit.StatusId == (int)OrderStatusIdEnum.Open);


//            Cancel order
//            shopService.CancelOrderById(orderSubmit);

//            var orderCancelled = shopService.GetOrderById(orderSubmit.Id);

//            Assert.IsNotNull(orderCancelled);
//            Assert.IsTrue(orderCancelled.StatusId == (int)OrderStatusIdEnum.Cancelled);


//        }

//        private WsOrder CreateOrder()
//        {
//            var shopService = new ShopRestService();
//            var deliveryOption = shopService.GetDeliveryOptions().FirstOrDefault();
//            var orderItems = shopService.GetMenuItemsByCategory(2);
//            var orderItem = orderItems.FirstOrDefault();
//            if (orderItem.HasSubOrder.HasValue && orderItem.HasSubOrder.Value)
//            {
//                foreach (var subItem in orderItem.SubItemSelections)
//                {
//                    subItem.Selected = subItem.Selections.FirstOrDefault();
//                }
//            }
//            var submitOrder =
//                new WsOrderBuilder().WithDeliveryOption(deliveryOption)
//                    .WithOrderItem(new List<WsMenuItem>() { orderItem })
//                    .Build();
//            return submitOrder;
//        }


//        [TestMethod]
//        public void GetOpenOrders_Test()
//        {
//            var shopService = new ShopRestService();
//            1. Create an Order
//            var order = CreateOrder();

//            2. Submit it 
//            var orderSubmit = shopService.SubmitCustomerOrder(order);

//            Call Open Order and verify exists
//            var openOrder = shopService.GetOpenOrders();
//            Assert.IsTrue(ContainOrder(openOrder, orderSubmit));


//            Cancel order
//            var orderCancelled = shopService.CancelOrderById(orderSubmit);
//            Assert.IsNotNull(orderCancelled);
//            Assert.IsTrue(orderCancelled.StatusId == (int)OrderStatusIdEnum.Cancelled);

//            var openOrderagain = shopService.GetOpenOrders();
//            Assert.IsFalse(ContainOrder(openOrderagain, orderCancelled));


//        }

//        private bool ContainOrder(List<WsOrder> openOrder, WsOrder orderSubmit)
//        {
//            return openOrder.Any(order => order.Id == orderSubmit.Id);
//        }


//        [TestMethod]
//        public void GetOrderById_Test()
//        {
//            var shopService = new ShopRestService();
//            1. Create an Order
//            var order = CreateOrder();

//            2. Submit it 
//            var orderSubmit = shopService.SubmitCustomerOrder(order);
//            3. Assert that order id is valid
//            Assert.IsNotNull(orderSubmit);
//            Assert.IsTrue(orderSubmit.Id > 0);
//            Assert.IsTrue(orderSubmit.StatusId == (int)OrderStatusIdEnum.Open);


//            var orderCheck = shopService.GetOrderById(orderSubmit.Id);
//            Assert.IsNotNull(orderCheck);
//            Assert.IsTrue(orderCheck.Id > 0);
//            Assert.IsTrue(orderCheck.StatusId == (int)OrderStatusIdEnum.Open);
//            Assert.AreEqual(orderCheck.Id, orderSubmit.Id);


//            Cancel order
//            var orderCancelled = shopService.CancelOrderById(orderSubmit);
//            Assert.IsNotNull(orderCancelled);
//            Assert.IsTrue(orderCancelled.StatusId == (int)OrderStatusIdEnum.Cancelled);
//        }


//        [TestMethod]
//        public void CloseOrderById_Test()
//        {
//            var shopService = new ShopRestService();
//            1. Create an Order
//            var order = CreateOrder();

//            2. Submit it 
//            var orderSubmit = shopService.SubmitCustomerOrder(order);
//            3. Assert that order id is valid
//            Assert.IsNotNull(orderSubmit);
//            Assert.IsTrue(orderSubmit.Id > 0);
//            Assert.IsTrue(orderSubmit.StatusId == (int)OrderStatusIdEnum.Open);


//            Cancel order
//            var orderCancelled = shopService.CloseOrderById(orderSubmit);
//            Assert.IsNotNull(orderCancelled);
//            Assert.IsTrue(orderCancelled.StatusId == (int)OrderStatusIdEnum.Completed);
//        }

//        [TestMethod]
//        public void CancelOrderById_Test()
//        {
//            var shopService = new ShopRestService();
//            1. Create an Order
//            var order = CreateOrder();

//            2. Submit it 
//            var orderSubmit = shopService.SubmitCustomerOrder(order);
//            3. Assert that order id is valid
//            Assert.IsNotNull(orderSubmit);
//            Assert.IsTrue(orderSubmit.Id > 0);
//            Assert.IsTrue(orderSubmit.StatusId == (int)OrderStatusIdEnum.Open);


//            Cancel order
//            shopService.CancelOrderById(orderSubmit);

//            var orderCancelled = shopService.GetOrderById(orderSubmit.Id);

//            Assert.IsNotNull(orderCancelled);
//            Assert.IsTrue(orderCancelled.StatusId == (int)OrderStatusIdEnum.Cancelled);
//        }


//        [TestMethod]
//        public void GetLatestOffer_Test()
//        {
//            var shopService = new ShopRestService();
//            var act = shopService.GetLatestOffer(0, 0);
//            Assert.IsNotNull(act);

//        }

//        [TestMethod]
//        public void GetSystemSettings_Test()
//        {
//            var shopService = new ShopRestService();
//            var act = shopService.GetSystemSettings();

//            Assert.IsNotNull(act);
//            Assert.IsTrue(act.Count > 0);
//            Assert.IsNotNull(act[0].Name);

//        }


//        [TestMethod]
//        public void GetDeliveryOptions_Test()
//        {
//            var shopService = new ShopRestService();
//            var act = shopService.GetDeliveryOptions();

//            Assert.IsNotNull(act);
//            Assert.IsTrue(act.Count > 0);
//            Assert.IsNotNull(act[0].Name);

//        }


//        [TestMethod]
//        public void GetDeliveryOptionsInternal_Test()
//        {
//            var shopService = new ShopRestService();
//            var act = shopService.GetDeliveryOptionsInternal();

//            Assert.IsNotNull(act);
//            Assert.IsTrue(act.Count > 0);
//            Assert.IsNotNull(act[0].Name);

//        }



//        [TestMethod]
//        public void DiscountCustomerOrder_Test()
//        {

//            var shopService = new ShopRestService();
//            1. Create an Order
//            var order = CreateOrder();


//            var orderDiscount = shopService.DiscountCustomerOrder(order);
//            Assert.AreEqual(orderDiscount.Id, orderDiscount.Id);

//            2. Submit it 
//            var orderSubmit = shopService.SubmitCustomerOrder(orderDiscount);
//            3. Assert that order id is valid
//            Assert.IsNotNull(orderSubmit);
//            Assert.IsTrue(orderSubmit.Id > 0);
//            Assert.IsTrue(orderSubmit.StatusId == (int)OrderStatusIdEnum.Open);



//            Cancel order
//            var orderCancelled = shopService.CancelOrderById(orderSubmit);
//            Assert.IsNotNull(orderCancelled);
//            Assert.IsTrue(orderCancelled.StatusId == (int)OrderStatusIdEnum.Cancelled);
//        }


//        [TestMethod]
//        public void LoginShopAgent_Test()
//        {

//            var shopService = new ShopRestService();
//            var act = shopService.LoginShopAgent("1234", true);

//            Assert.IsNotNull(act);
//            Assert.IsNotNull(act.UserName);

//        }


//        [TestMethod]
//        public void GetHotMenuItems_Test()
//        {
//            var shopService = new ShopRestService();
//            var act = shopService.GetHotMenuItems();

//            Assert.IsNotNull(act);
//            Assert.IsTrue(act.Count > 0);
//            Assert.IsNotNull(act[0].Name);
//        }


//        [TestMethod]
//        public void GetHotMenuItemsInternal_Test()
//        {
//            var shopService = new ShopRestService();
//            var act = shopService.GetHotMenuItemsInternal();

//            Assert.IsNotNull(act);
//            Assert.IsTrue(act.Count > 0);
//            Assert.IsNotNull(act[0].Name);
//        }


//        [TestMethod]
//        public void LoadPaymentMonths_Test()
//        {
//            var shopService = new ShopRestService();
//            var act = shopService.LoadPaymentMonths();

//            Assert.IsNotNull(act);
//            Assert.IsTrue(act.Count > 0);
//            Assert.IsNotNull(act[0].Name);
//        }


//        [TestMethod]
//        public void LoadPaymentYears_Test()
//        {
//            var shopService = new ShopRestService();
//            var act = shopService.LoadPaymentYears();

//            Assert.IsNotNull(act);
//            Assert.IsTrue(act.Count > 0);
//            Assert.IsNotNull(act[0].Name);

//        }


//        [TestMethod]
//        public void SyncStatus_Test()
//        {
//            var shopService = new ShopRestService();
//            var act = shopService.SyncStatus();

//            Assert.IsNotNull(act);
//            Assert.IsTrue(act.Result);

//        }



//        [TestMethod]
//        public void EmailException_Test()
//        {
//            var shopService = new ShopRestService();
//            var act = shopService.EmailException("Sample Failed Exception");

//            Assert.IsNotNull(act);
//            Assert.IsTrue(act.IsSuccess);

//        }


//        [TestMethod]
//        public void GetCustomerByPhoneNumber_Test()
//        {
//            var shopService = new ShopRestService();
//            var act = shopService.GetCustomerByPhoneNumber("087999997");

//            Assert.IsNotNull(act);


//        }


//        [TestMethod]
//        public void TakePayment_Test()
//        {
//            var shopService = new ShopRestService();
//            var payment = new WsPayment
//            {
//                CardExpirationMonth = "12",
//                CardExpirationYear = "2014",
//                CardCvc = "1234",
//                CardNumber = "1234567812345678",
//                Amount = 100
//            };

//            var act = shopService.TakePayment(payment);

//            Assert.IsNotNull(act);
//            Assert.IsFalse(act.IsSuccess);
//            Assert.IsNotNull(act.ErrorMessage);

//        }


//        [TestMethod]
//        public void DoDailyReport_Test()
//        {
//            var shopService = new ShopRestService();
//            var report = new WsReport
//            {
//                ReportTypeId = 2,

//            };

//            var currentDate = DateTime.Now;
//            var yesterday = DateTime.Now.AddDays(-1);


//            if (DateTime.Now.Hour <= 7)
//                report.FromDate = new DateTime(yesterday.Year, yesterday.Month, yesterday.Day);
//            else
//                report.FromDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);

//            var act = shopService.GetReport(report);

//            Assert.IsNotNull(act);


//        }

//        [TestMethod]
//        public void TableCommand_Test()
//        {
//            var shopService = new ShopRestService();


//            var act = shopService.GetTableCommand();

//            Assert.IsNotNull(act);


//        }


//        [TestMethod]
//        public void GetDriver_Test()
//        {
//            var shopService = new ShopRestService();


//            var act = shopService.GetAllDrivers();

//            Assert.IsNotNull(act);


//        }


//        [TestMethod]
//        public void UpdateDriverTest_Test()
//        {
//            var shopService = new ShopRestService();
//            1. Create an Order
//            var order = CreateOrder();

//            2. Submit it 
//            var orderSubmit = shopService.SubmitCustomerOrder(order);
//            3. Assert that order id is valid
//            Assert.IsNotNull(orderSubmit);
//            Assert.IsTrue(orderSubmit.Id > 0);
//            Assert.IsTrue(orderSubmit.StatusId == (int)OrderStatusIdEnum.Open);



//            orderSubmit.DriverName = "Test1";


//            var orderUpdate1 = shopService.UpdateOrder(orderSubmit, (int)UpdateOrderOption.UpdateDriver);

//            Assert.IsNotNull(orderUpdate1);
//            Assert.AreEqual(orderSubmit.DriverName, orderUpdate1.DriverName);


//            orderSubmit.DriverName = "NewTest";
//            var orderUpdate2 = shopService.UpdateOrder(orderSubmit, (int)UpdateOrderOption.UpdateDriver);

//            Assert.IsNotNull(orderUpdate2);
//            Assert.AreEqual(orderSubmit.DriverName, orderUpdate2.DriverName);





//            Cancel order
//            shopService.CancelOrderById(orderSubmit);

//            var orderCancelled = shopService.GetOrderById(orderSubmit.Id);

//            Assert.IsNotNull(orderCancelled);
//            Assert.IsTrue(orderCancelled.StatusId == (int)OrderStatusIdEnum.Cancelled);


//        }


//        [TestMethod]
//        public void GetAllPaymentTypes_Test()
//        {
//            var shopService = new ShopRestService();


//            var act = shopService.GetAllPaymentTypes();

//            Assert.IsNotNull(act);


//        }

//    }




//}
