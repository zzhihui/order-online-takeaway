using Core.Test.Utilities.Fixtures;
using Core.Test.Utilities.Utilities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Takeaway.BusinessEngine.BusinessEngine;
using Takeaway.Data.Entities.Authentication;
using Takeaway.Service.Contract.Entities.Authentication;
using Takeaway.Service.Services;
using Takeaway.Service.Transformers;
using Takeaway.Service.Transformers.In;
using Takeaway.Service.Transformers.Out;

namespace Takeaway.Service.UnitTests.Takeaway.Service.Services
{
    [TestClass]
    public class AuthenticationServiceTest : AutoMockFixture<AuthenticationService>
    {
        //[TestMethod]
        //public void AuthenticateShopOwner_Verify()
        //{

        //    const string usernameData = "TestUserName";
        //    const string passwordData = "SamplePassword";
        //    const int shopId = 1;

        //    //Property
        //    var shopUserTransformedFake = ObjectMother.CreateAndFill<ShopUser>();
        //    var wsShopUserTransformedFake = ObjectMother.CreateAndFill<WsShopUser>();
        //    var validationResultFake = new ValidationResult();
        //    var validatorFakeMock = Container.Create<IValidator>();


        //    //Validate
        //    Container.GetMock<IValidator>()
        //        .Setup(s => s.Validate(It.IsAny<WsShopUser>())).Returns(validationResultFake);

        //    Container.GetMock<IValidatorFactory>()
        //       .Setup(s => s.GetValidator(typeof(WsShopUser)))
        //       .Returns(validatorFakeMock);


        //    //Transform Request
        //    Container.GetMock<ITransformerFactory>()
        //        .Setup(s => s.CreateInstance<IShopUserTransformer>())
        //        .Returns(Container.Create<IShopUserTransformer>());

        //    Container.GetMock<IShopUserTransformer>()
        //        .Setup(s => s.Transform<WsShopUser, ShopUser>(It.IsAny<WsShopUser>()))
        //        .Returns(shopUserTransformedFake);

            

        //    //Process
        //    Container.GetMock<IAuthenticationBusinessEngine>()
        //        .Setup(s => s.AuthenticateShopOwner(It.IsAny<ShopUser>()))
        //        .Returns(shopUserTransformedFake);


        //    //Transform Response
        //    Container.GetMock<ITransformerFactory>()
        //        .Setup(s => s.CreateInstance<IWsShopUserTransformer>())
        //        .Returns(Container.Create<IWsShopUserTransformer>());
        //    Container.GetMock<IWsShopUserTransformer>()
        //       .Setup(s => s.Transform<ShopUser, WsShopUser>(It.IsAny<ShopUser>()))
        //       .Returns(wsShopUserTransformedFake);

        //    var act = Sut.AuthenticateShopOwner(usernameData, passwordData, shopId);

        //    Assert.AreSame(act, wsShopUserTransformedFake);

        //}


        //[TestMethod]
        //public void AGetUserByName_Verify()
        //{
        //    const string usernameData = "TestUserName";
          
        //    //Property
        //    var shopUserTransformedFake = ObjectMother.CreateAndFill<ShopUser>();
        //    var wsShopUserTransformedFake = ObjectMother.CreateAndFill<WsShopUser>();
        //    var validationResultFake = new ValidationResult();

        //    var validatorFakeMock = Container.Create<IValidator>();


        //    //Validate
        //    Container.GetMock<IValidator>()
        //        .Setup(s => s.Validate(It.IsAny<ValidationContext>())).Returns(validationResultFake);

        //    Container.GetMock<IValidatorFactory>()
        //       .Setup(s => s.GetValidator(typeof(WsShopUser)))
        //       .Returns(validatorFakeMock);


        //    //Transform Request
        //    Container.GetMock<ITransformerFactory>()
        //        .Setup(s => s.CreateInstance<IShopUserTransformer>())
        //        .Returns(Container.Create<IShopUserTransformer>());

        //    Container.GetMock<IShopUserTransformer>()
        //        .Setup(s => s.Transform<WsShopUser, ShopUser>(It.IsAny<WsShopUser>()))
        //        .Returns(shopUserTransformedFake);

            

        //    //Process
        //    Container.GetMock<IAuthenticationBusinessEngine>()
        //        .Setup(s => s.GetUserByName(It.IsAny<ShopUser>()))
        //        .Returns(shopUserTransformedFake);


        //    //Transform Response

            
        //    Container.GetMock<ITransformerFactory>()
        //        .Setup(s => s.CreateInstance<IWsShopUserTransformer>())
        //        .Returns(Container.Create<IWsShopUserTransformer>());
        //    Container.GetMock<IWsShopUserTransformer>()
        //       .Setup(s => s.Transform<ShopUser, WsShopUser>(It.IsAny<ShopUser>()))
        //       .Returns(wsShopUserTransformedFake);

        //    var act = Sut.GetUserByName(usernameData);

        //    Assert.AreSame(act, wsShopUserTransformedFake);
        //}

        
    }
}
