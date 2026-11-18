using System.Configuration;
using Takeway.Mobile.Core.Utilities;

namespace JsonFileGenerator
{
    public class BootStrap
    {
        public void Run()
        {
            Constants.SiteUrl = ConfigurationManager.AppSettings["ShopUrl"];
        } 
    }
}