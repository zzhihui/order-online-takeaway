using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Validation;
using FluentValidation;
using Takeaway.BusinessEngine.BusinessEngine;
using Takeaway.Data.Entities.Authentication;
using Takeaway.Service.Contract.Entities.Authentication;
using Takeaway.Service.Transformers;
using Takeaway.Service.Transformers.In;
using Takeaway.Service.Transformers.Out;
using Takeaway.Service.Validations;

namespace Takeaway.Service.Services
{
    public interface IAuthenticationService : IService
    {
        WsShopUser GetUserByName(string userName, int shopId);
        bool AuthenticateShopAdmin(string userName, string password, int shopId);
    }

    [ServiceHandler]
    public class AuthenticationService : ServiceBase, IAuthenticationService
    {
        private readonly ITransformer _transformer;
        private readonly IAuthenticationBusinessEngine _authenticationBusinessEngine;

        public AuthenticationService(IValidatorFactory validatorFactory,
                                     IAuthenticationBusinessEngine authenticationBusinessEngine,
                                     IUnitOfWork unitOfWork)
            : base(validatorFactory, unitOfWork)
        {

            _authenticationBusinessEngine = authenticationBusinessEngine;
        }

        public WsShopUser GetUserByName(string userName, int shopId)
        {
            return Execute(() =>
            {
                //Wrap request 
                var wsShopUserRequest = new WsShopUser()
                {
                    UserName = userName,
                };

                //Validate Request
                Validate(wsShopUserRequest, WsShopUserValidatorRuleSet.UserNameOnly.ToString());


                //Transform Request
                var shopUserTransformed = _transformer.Transform<WsShopUser, ShopUser>(wsShopUserRequest);


                //Process
                var shopUserResponse = _authenticationBusinessEngine.GetUserByName(shopUserTransformed, shopId);
                if (shopUserResponse == null)
                    return null;


                //Transform Response
                return _transformer.Transform<ShopUser, WsShopUser>(shopUserResponse);

            });
        }

        public bool AuthenticateShopAdmin(string userName, string password, int shopId)
        {
            return Execute(() =>
            {

                Guard.NotNull(userName);
                Guard.NotNull(password);
                Guard.GreaterThanZero(shopId);

                var shopUserResponse = _authenticationBusinessEngine.AuthenticateShopAdmin(userName, password, shopId);
                return shopUserResponse != null;
            });
        }
    }
}