using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Binding.BindingContext;
using Takeway.Mobile.Core.ViewModels.Client;

namespace Takeway.Mobile.AndroidClient.Views
{
    [Activity(Label = "@string/app_page_categoryitemsub_title", NoHistory = true)]
    public class CategoryItemSubItemView : ViewBase<ShopClientCategoryItemSubItemViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.CategoryItemSubItem);
            BindableProgress = new BindableProgress(this);
            var set = this.CreateBindingSet<CategoryItemSubItemView, ShopClientCategoryItemSubItemViewModel>();
            set.Bind(BindableProgress).For(p => p.Visible).To(vm => vm.IsBusy);
            set.Apply();
            
        }


    }
}