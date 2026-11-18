using System.Windows;
using Caliburn.Micro;

namespace Takeaway.Admin.Wpf.Utility.Extension
{
    public class AppWindowManager : WindowManager
    {
        protected override Window EnsureWindow(object model, object view, bool isDialog)
        {
            Window window = base.EnsureWindow(model, view, isDialog);

            window.SizeToContent = SizeToContent.Manual;
         

            return window;
        }
    }
}