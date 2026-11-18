using System.Windows;
using MahApps.Metro.Controls;

namespace Takeaway.Admin.Wpf.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : MetroWindow
    {
        public ShellView()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
        }
      
    }
}
