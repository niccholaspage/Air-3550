using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
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

        readonly MyAccountViewModel ViewModel = new();

        private void BookingsButton_Click(object _, RoutedEventArgs __)
        {
            ViewModel.SetCurrentTab(MyAccountViewModel.Tab.BOOKINGS);
        }

        private void ProfileButton_Click(object _, RoutedEventArgs __)
        {
            ViewModel.SetCurrentTab(MyAccountViewModel.Tab.PROFILE);
        }

        private void ChangePasswordButton_Click(object _, RoutedEventArgs __)
        {
            ViewModel.SetCurrentTab(MyAccountViewModel.Tab.CHANGE_PASSWORD);
        }
    }
}
