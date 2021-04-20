// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Windows.System;

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public sealed class Params
        {
            public class NewUser
            {
                public string LoginId;

                public NewUser(string LoginId)
                {
                    this.LoginId = LoginId;
                }
            }

            public class PasswordChanged { }
        }

        public LoginPage()
        {
            this.InitializeComponent();
        }

        readonly LoginViewModel ViewModel = new();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Params.NewUser param)
            {
                ViewModel.Username = param.LoginId;
                InfoBar.Title = "Account Created";
                InfoBar.Message = $"Your account has been successfully created. Your ID is {param.LoginId}.";
                InfoBar.IsOpen = true;
            }
            else if (e.Parameter is Params.PasswordChanged)
            {
                InfoBar.Title = "Password Changed";
                InfoBar.Message = $"Your password has been changed. Please login again.";
                InfoBar.IsOpen = true;
            }
        }

        public async void LoginButton_Clicked(object _, RoutedEventArgs __)
        {
            await PerformLogin();
        }

        private async void StackPanel_KeyDown(object _, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                await PerformLogin();
            }
        }

        private async Task PerformLogin()
        {
            if (await ViewModel.PerformLogin())
            {
                Frame.GoBack();
            }
        }

        private void RegisterButton_Click(object _, RoutedEventArgs __)
        {
            Frame.Navigate(typeof(RegisterPage));
        }
    }
}
