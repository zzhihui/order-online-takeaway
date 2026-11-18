using Autofac;
using Core.Common.Core;
using Takeaway.Service.Contract.Entities.Authentication;
using Takeaway.Service.Services;

namespace Takeway.Web.Infrastructure.Utility
{
    public class ShopUserValidator
    {
        private ILifetimeScope _scope;

        public ShopUserValidator()
        {
            var applicationScope = new ApplicationScopeInstance();
            _scope = applicationScope.Container.BeginLifetimeScope();
        }

        public void Dispose()
        {
            if (_scope == null) return;

            _scope.Dispose();
            _scope = null;
        }

        public WsShopUser GetUserByName(string userName)
        {
            var authService = _scope.Resolve<IAuthenticationService>();
            return authService.GetUserByName(userName,1);

        }

        public bool AuthenticateShopAdmin(string userName, string password, int shopId)
        {
            var authService = _scope.Resolve<IAuthenticationService>();
            return authService.AuthenticateShopAdmin(userName, password, shopId);
        }
    }
}