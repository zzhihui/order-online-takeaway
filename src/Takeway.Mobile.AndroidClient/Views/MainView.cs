using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Binding.BindingContext;
using Takeway.Mobile.Core.ViewModels.Client;

namespace Takeway.Mobile.AndroidClient.Views
{
    [Activity(Label = "@string/app_page_main_title")]
    public class MainView : ViewBase<ShopClientMainViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.Main);

            BindableProgress = new BindableProgress(this);
            var set = this.CreateBindingSet<MainView, ShopClientMainViewModel>();
            set.Bind(BindableProgress).For(p => p.Visible).To(vm => vm.IsBusy);
            set.Apply();

        }

       
    }
}