using Android.App;
using Android.Content;

namespace Takeway.Mobile.AndroidAdmin.Services
{
    [BroadcastReceiver]
    [IntentFilter(new string[] { TakeawayService.NewOrderUpdateAction }, Priority = (int)IntentFilterPriority.LowPriority)]
    public class TakeawayNotificationReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var header = context.Resources.GetString(Resource.String.app_notification_header);
            var title = context.Resources.GetString(Resource.String.app_notification_title);

            var nMgr = (NotificationManager)context.GetSystemService(Context.NotificationService);
            var notification = new Notification(Resource.Drawable.Icon_notification, title);
            var pendingIntent = PendingIntent.GetActivity(context, 0, new Intent(context, typeof(SplashScreen)), 0);
            notification.SetLatestEventInfo(context, header, title, pendingIntent);
            nMgr.Notify(0, notification);
        }
    }
}