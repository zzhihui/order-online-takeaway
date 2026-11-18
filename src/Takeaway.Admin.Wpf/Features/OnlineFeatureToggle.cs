using FeatureToggle.Toggles;

namespace Takeaway.Admin.Wpf.Features
{

    public interface IOnlineFeatureToggle : IFeatureToggle
    {
       
    }
    public class OnlineFeatureToggle : SimpleFeatureToggle, IOnlineFeatureToggle
    {

        
         
    }
}