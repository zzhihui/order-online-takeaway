using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Utility;
using Takeaway.Data.Entities.Authentication;
using Takeaway.Data.Repositories;

namespace Takeaway.BusinessEngine.BusinessEngine
{

    public interface IAuthenticationBusinessEngine : IBusinessEngine<IAuthenticationBusinessEngine>
    {
        ShopUser GetUserByName(ShopUser shopUserTransformed, int shopId);
        ShopUser AuthenticateShopAdmin(string userName, string password, int shopId);
    }

    [BusinessEngine]
    public class AuthenticationBusinessEngine : BusinessEngineBase, IAuthenticationBusinessEngine
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger("AuthenticationBusinessEngine");

        private readonly IShopUserRepository _shopUserRepository;
        private readonly IEncryptionHelper _encryptionHelper;
        private readonly IAppSettingInstance _appSetting;

        public AuthenticationBusinessEngine(IEncryptionHelper encryptionHelper,
                                            IAppSettingInstance appSetting,
                                            IShopUserRepository shopUserRepository)
        {
            _encryptionHelper = encryptionHelper;
            _appSetting = appSetting;
            _shopUserRepository = shopUserRepository;
        }

        public ShopUser GetUserByName(ShopUser shopUserRequest, int shopId)
        {
            return Execute(() => _shopUserRepository.GetActiveShopUserByName(shopUserRequest.UserName, shopId));
        }

        public ShopUser AuthenticateShopAdmin(string userName, string password, int shopId)
        {
            var encryptedPassword = _encryptionHelper.EncryptXml(password);
            return Execute(() => _shopUserRepository.GetShopAdmin(userName, encryptedPassword,shopId));
        }
    }
}