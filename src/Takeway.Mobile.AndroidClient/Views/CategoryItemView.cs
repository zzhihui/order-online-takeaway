using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Binding.BindingContext;
using Takeway.Mobile.Core.ViewModels.Client;

namespace Takeway.Mobile.AndroidClient.Views
{
    [Activity(Label = "@string/app_page_categoryitem_title")]
    public class CategoryItemView : ViewBase<ShopClientCategoryItemViewModel>
    {

        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.CategoryItem);
            BindableProgress = new BindableProgress(this);
            var set = this.CreateBindingSet<CategoryItemView, ShopClientCategoryItemViewModel>();
            set.Bind(BindableProgress).For(p => p.Visible).To(vm => vm.IsBusy);
            set.Apply();
        }
    }
}