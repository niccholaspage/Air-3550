// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using Air_3550.Services;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Windows.System;
using Microsoft.Extensions.DependencyInjection;
using Air_3550.Models;

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

        private readonly UserSessionService userSession;

        public LoginPage()
        {
            userSession = App.Current.Services.GetService<UserSessionService>();

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
                // If the user that logged in is a customer,
                // then we just take them back to the previous
                // page. Otherwise, we take them to the proper
                // page based on their role.
                var role = userSession.Role;

                if (role == Role.CUSTOMER)
                {
                    Frame.GoBack();
                }
                else
                {
                    Frame.Navigate(typeof(MainPage));

                    if (role == Role.ACCOUNTANT || role == Role.FLIGHT_MANAGER)
                    {
                        Frame.Navigate(typeof(SummaryPage));
                    }
                    else if (role == Role.MARKETING_MANAGER || role == Role.LOAD_ENGINEER)
                    {
                        Frame.Navigate(typeof(EditSchedulePage));
                    }

                    // Clear every item in the backstack except for the first one, the main page.
                    while (Frame.BackStack.Count > 1)
                    {
                        Frame.BackStack.RemoveAt(0);
                    }
                }
            }
        }

        private void RegisterButton_Click(object _, RoutedEventArgs __)
        {
            Frame.Navigate(typeof(RegisterPage));
        }
    }
}
