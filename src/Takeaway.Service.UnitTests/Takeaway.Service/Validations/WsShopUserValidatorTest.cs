using Core.Test.Utilities.Fixtures;
using FluentValidation;
using FluentValidation.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takeaway.Service.Contract.Entities.Authentication;
using Takeaway.Service.Validations;

namespace Takeaway.Service.UnitTests.Takeaway.Service.Validations
{
    [TestClass]
    public class WsShopUserValidatorTest : AutoMockFixture<WsShopUserValidator>
    {
        [TestMethod]
        public void ValidateAndThrow_Valid()
        {
            const string usernameData = "TestUserName";
            const string passwordData = "SamplePassword";
            const int shopId = 1;
            var wsShopUserRequest = new WsShopUser()
               {
                   UserName = usernameData,
                   Password = passwordData,
                   ShopId = shopId
               };

            var act = Sut.Validate(wsShopUserRequest);

            Assert.IsTrue(act.IsValid);

        }

        [TestMethod]
        public void ValidateAndThrow_UserNameNull()
        {
            
            const string passwordData = "SamplePassword";
            const int shopId = 1;
            var wsShopUserRequest = new WsShopUser()
            {
                UserName = null,
                Password = passwordData,
                ShopId = shopId
            };

            var act = Sut.Validate(wsShopUserRequest);

            Assert.IsFalse(act.IsValid);

            Assert.AreEqual(act.Errors[0].ErrorMessage,  "Username cannot be null");


        }


        [TestMethod]
        public void ValidateAndThrow_UserNameEmtpy()
        {
            var usernameData = string.Empty;
            const string passwordData = "SamplePassword";
            const int shopId = 1;
            var wsShopUserRequest = new WsShopUser()
            {
                UserName = usernameData,
                Password = passwordData,
                ShopId = shopId
            };

            var act = Sut.Validate(wsShopUserRequest);

            Assert.IsFalse(act.IsValid);

            Assert.AreEqual(act.Errors[0].ErrorMessage, "Username cannot be empty");

        }


        [TestMethod]
        public void ValidateAndThrow_UserNamePasswordSame()
        {
            const string usernameData = "SamplePassword";
            const string passwordData = "SamplePassword";
            const int shopId = 1;
            var wsShopUserRequest = new WsShopUser()
            {
                UserName = usernameData,
                Password = passwordData,
                ShopId = shopId
            };

            var act = Sut.Validate(wsShopUserRequest);

            Assert.IsFalse(act.IsValid);

            Assert.AreEqual(act.Errors[0].ErrorMessage, "Username and password cannot be the same");

        }


        [TestMethod]
        public void ValidateAndThrow_PasswordLessThan6()
        {
            const string usernameData = "SamplePassword";
            const string passwordData = "12345";
            const int shopId = 1;
            var wsShopUserRequest = new WsShopUser()
            {
                UserName = usernameData,
                Password = passwordData,
                ShopId = shopId
            };

            var act = Sut.Validate(wsShopUserRequest);

            Assert.IsFalse(act.IsValid);

            Assert.AreEqual(act.Errors[0].ErrorMessage, "Password must be more than 6 chars");

        }


        [TestMethod]
        public void ValidateAndThrow_RuleUserOnlyValid()
        {
            const string usernameData = "TestUserName";
            var wsShopUserRequest = new WsShopUser()
            {
                UserName = usernameData,
            };


            var validateContext = new ValidationContext<WsShopUser>(
                    wsShopUserRequest,
                    new PropertyChain(),
                    new RulesetValidatorSelector(WsShopUserValidatorRuleSet.UserNameOnly.ToString()));


            var act = Sut.Validate(validateContext);

            Assert.IsTrue(act.IsValid);

        }



        [TestMethod]
        public void ValidateAndThrow_RuleUserOnlyUserNameEmtpy()
        {
            const string usernameData = "";
            var wsShopUserRequest = new WsShopUser()
            {
                UserName = usernameData,
            };


            var validateContext = new ValidationContext<WsShopUser>(
                    wsShopUserRequest,
                    new PropertyChain(),
                    new RulesetValidatorSelector(WsShopUserValidatorRuleSet.UserNameOnly.ToString()));


            var act = Sut.Validate(validateContext);

            Assert.IsFalse(act.IsValid);

            Assert.AreEqual(act.Errors[0].ErrorMessage, "Username cannot be empty");

        }
    }
}
