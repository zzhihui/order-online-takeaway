using System;

namespace Takeaway.BusinessEngine.Utility
{
    public interface IAuthSmsCodeUtil
    {
        string GenerateAuthCode();
    }


    public class AuthSmsCodeUtil: IAuthSmsCodeUtil
    {
        public string GenerateAuthCode()
        {
            var random = new Random();
            var password = random.Next(1000, 9999);
            return password.ToString();
        }
    }
}