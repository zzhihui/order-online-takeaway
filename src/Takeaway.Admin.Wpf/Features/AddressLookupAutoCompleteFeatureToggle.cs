using FeatureToggle.Toggles;

namespace Takeaway.Admin.Wpf.Features
{

    public interface IAddressLookupAutoCompleteFeatureToggle : IFeatureToggle
    {

    }
    public class AddressLookupAutoCompleteFeatureToggle: SimpleFeatureToggle, IAddressLookupAutoCompleteFeatureToggle
    {
         
    }
}