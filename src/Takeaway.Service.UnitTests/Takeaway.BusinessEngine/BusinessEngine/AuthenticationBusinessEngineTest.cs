using System;
using Core.Test.Utilities.Fixtures;
using Core.Test.Utilities.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Takeaway.BusinessEngine.BusinessEngine;
using Takeaway.BusinessEngine.Exception;
using Takeaway.Data.Entities.Authentication;
using Takeaway.Data.Repositories;

namespace Takeaway.Service.UnitTests.Takeaway.BusinessEngine.BusinessEngine
{
    [TestClass]
    public class AuthenticationBusinessEngineTest : AutoMockFixture<AuthenticationBusinessEngine>
    {
        
        [TestMethod]
        public void GetUserByName_Verify()
        {
            var shopUserFake = ObjectMother.CreateAndFill<ShopUser>();
            var act = Sut.GetUserByName(shopUserFake, It.IsAny<int>());
            Container.GetMock<IShopUserRepository>().Verify(s => s.GetActiveShopUserByName(It.IsAny<string>(), It.IsAny<int>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessEngineException))]
        public void GetUserByName_Exception()
        {
            var shopUserFake = ObjectMother.CreateAndFill<ShopUser>();
            Container.GetMock<IShopUserRepository>().Setup(s => s.GetActiveShopUserByName(It.IsAny<string>(), It.IsAny<int>())).Throws(new Exception(""));
            var act = Sut.GetUserByName(shopUserFake, It.IsAny<int>());
         
        }



    }
}
