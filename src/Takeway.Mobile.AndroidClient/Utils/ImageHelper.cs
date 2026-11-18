using System.Net;
using Android.Graphics;
using Android.Util;
using Exception = System.Exception;

namespace Takeway.Mobile.AndroidClient.Utils
{
    public class ImageHelper
    {
        public static Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;
            try
            {
                using (var webClient = new WebClient())
                {
                    var imageBytes = webClient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Warn("ImageHelper", ex.Message);
            }

            return imageBitmap;
        }
    }
}