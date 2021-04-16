using Air_3550.ViewModels;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyAccountPage : Page
    {
        public MyAccountPage()
        {
            this.InitializeComponent();
        }

        MyAccountViewModel ViewModel = new();

        private void BookingsButton_Click(object _, Microsoft.UI.Xaml.RoutedEventArgs _1)
        {
            ViewModel.ViewingBookings = true;
        }

        private void AccountInfoButton_Click(object _, Microsoft.UI.Xaml.RoutedEventArgs _1)
        {
            ViewModel.ViewingBookings = false;
        }
    }
}
