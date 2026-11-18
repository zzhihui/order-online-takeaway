using Android.App;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Widget;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Core;
using Cirrious.MvvmCross.Droid.Views;
using Java.Lang;
using Takeaway.Service.Services;
using Takeway.Mobile.AndroidClient.Utils;
using Takeway.Mobile.Core.Services.RestServices;
using Takeway.Mobile.Core.Utilities;
using Takeway.Mobile.Core.ViewModels;

namespace Takeway.Mobile.AndroidClient
{
    [Activity(
        Label = "@string/app_name"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {

        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
            
        }

        public override void InitializationComplete()
        {

            try
            {
                //if (!Constants.OnlineMode) return;
                //var backgroundImage = string.Format("{0}/content/androidBackground.jpg", Constants.SiteUrl);
                //var image = ImageHelper.GetImageBitmapFromUrl(backgroundImage);
                //Drawable dr = new BitmapDrawable(image);
                //var form = FindViewById<LinearLayout>(Resource.Id.SplashScreen);
                //RunOnUiThread(() => form.SetBackgroundDrawable(dr));
            
            }
            catch (Exception ex)
            {
                Log.Warn("SplashScreen", ex.Message);
            }
            finally
            {
                base.InitializationComplete();
            }


        }

    }
}