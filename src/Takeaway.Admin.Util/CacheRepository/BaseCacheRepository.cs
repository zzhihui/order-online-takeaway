using System.Configuration;

namespace Takeaway.Admin.Util.CacheRepository
{

    public interface IBaseCacheRepository
    {

    }


    public class BaseCacheRepository: IBaseCacheRepository
    {
        public static string Host = ConfigurationManager.AppSettings["DbHost"];
    }
}