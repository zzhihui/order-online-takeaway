using System;
using Android.Content;
using Android.Locations;
using Android.Util;
using Android.Webkit;

namespace Takeway.Mobile.AndroidAdmin.Controls
{
    public class BindableWebView : WebView
    {
        private string _text;
        private string _url;

        public BindableWebView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        public string Text
        {
            get { return _text; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;

                _text = value;
                LoadData(_text, "text/html", "utf-8");
                UpdatedHtmlContent();
            }
        }


        public string Url
        {
            get { return _url; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;

                _url = value;
                SetWebViewClient(new WebViewClient());
                Settings.JavaScriptEnabled = true;
                LoadUrl(_url);
                UpdatedHtmlContent();
            }
        }

        public event EventHandler HtmlContentChanged;

        private void UpdatedHtmlContent()
        {
            var handler = HtmlContentChanged;
            if (handler != null)
                handler(this, GpsStatus.EventArgs.Empty);
        }
    }
}