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
    public sealed partial class ChangePasswordSubPage : Page
    {
        public ChangePasswordSubPage()
        {
            this.InitializeComponent();
        }

        ChangePasswordViewModel ViewModel = new();

        private async void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (await ViewModel.ChangePassword())
            {
                // TODO: This is so ugly... how can we make
                // this better?
                // Successful password change, so clear the
                // backstack and send the user back to the login
                // page.
                var parentContentControl = Parent as ContentControl;

                var parentRelativePanel = parentContentControl.Parent as RelativePanel;

                var parentPage = parentRelativePanel.Parent as Page;

                parentPage.Frame.Navigate(typeof(MainPage));
                parentPage.Frame.BackStack.Clear();
                parentPage.Frame.Navigate(typeof(LoginPage), new LoginPage.Params.PasswordChanged());
            }
        }
    }
}
