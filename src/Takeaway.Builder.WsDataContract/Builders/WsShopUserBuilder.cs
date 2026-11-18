using Takeaway.Service.Contract.Entities.Authentication;

namespace Takeaway.Builder.WsDataContract.Builders
{
    public class WsShopUserBuilder
    {
        private readonly WsShopUser _entity;

        public WsShopUserBuilder()
        {
            _entity = new WsShopUser
            {
                AuthToken =
                    "21,194,38,195,165,73,232,74,86,241,186,248,38,199,86,61,153,228,160,42,249,210,129,231,233,198,142,40,180,11,13,228,191,6,13,134,128,63,73,142,164,249,140,184,133,236,205,8,188,195,51,200,216,199,233,58,28,88,254,193,70,178,81,83,212,79,209,80,197,38,54,108,160,192,181,45,152,216,15,8,188,244,36,123,118,247,64,232,106,215,248,208,171,124,97,159,95,146,215,93,211,84,130,46,244,216,149,113,244,197,59,53,244,21,105,241,137,140,110,171,54,75,52,27,167,85,213,136",
                Id = 1,
                ShopId = 1,
                UserName = "Bingliang Chen",
                ServerUrl = @"https://indasoftfusion.azurewebsites.net"
            };

        }


        public WsShopUser Build()
        {
            return _entity;
        }
    }
}