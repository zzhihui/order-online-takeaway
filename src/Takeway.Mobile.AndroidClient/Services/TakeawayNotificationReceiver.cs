using Android.Content;
using Android.App;
using Android.OS;
using Cirrious.CrossCore;
using Takeway.Mobile.AndroidClient.Views;
using Takeway.Mobile.Core.ViewModels;

namespace Takeway.Mobile.AndroidClient.Services
{
    [BroadcastReceiver]
    [IntentFilter(new string[] { TakeawayService.SpecialOfferUpdateAction }, Priority = (int)IntentFilterPriority.LowPriority)]
    public class TakeawayNotificationReceiver : BroadcastReceiver
    {

        public override void OnReceive(Context context, Intent intent)
        {
            var header = context.Resources.GetString(Resource.String.app_notification_header);
            var title=context.Resources.GetString(Resource.String.app_notification_title);

            var globalViewModel = Mvx.Resolve<IGlobalModel>();
            if (globalViewModel != null && globalViewModel.LatestOffer != null)
                title = globalViewModel.LatestOffer.Description;

            var nMgr = (NotificationManager)context.GetSystemService(Context.NotificationService);
            var notification = new Notification(Resource.Drawable.Icon_notification, title);
            var pendingIntent = PendingIntent.GetActivity(context, 0, new Intent(context, typeof(SplashScreen)), 0);
            notification.SetLatestEventInfo(context, header, title, pendingIntent);
            nMgr.Notify(0, notification);
        }
    }
}