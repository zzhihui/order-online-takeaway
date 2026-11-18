using FeatureToggle.Toggles;

namespace Takeaway.Admin.Wpf.Features
{

    public interface IPosFeatureToggle : IFeatureToggle
    {
        
    }


    public class PosFeatureToggle : SimpleFeatureToggle, IPosFeatureToggle
    {
         
    }
}