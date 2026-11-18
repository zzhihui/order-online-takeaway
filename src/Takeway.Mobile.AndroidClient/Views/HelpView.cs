using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Binding.BindingContext;
using Takeway.Mobile.Core.ViewModels.Client;

namespace Takeway.Mobile.AndroidClient.Views
{

    [Activity(Label = "Help")]
    public class HelpView : ViewBase<ShopClientHelpViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.Help);
            BindableProgress = new BindableProgress(this);
            var set = this.CreateBindingSet<HelpView, ShopClientHelpViewModel>();
            set.Bind(BindableProgress).For(p => p.Visible).To(vm => vm.IsBusy);
            set.Apply();

        }
         
    }
}