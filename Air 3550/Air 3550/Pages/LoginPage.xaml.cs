// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
//test jh
using Air_3550.Repository;
using Database.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Threading.Tasks;
using Windows.System;

namespace Air_3550.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private async Task PerformLogin()
        {
            void ShowFeedback(string text)
            {
                Feedback.Text = text;
                Feedback.Visibility = Visibility.Visible;
            }

            string username = UsernameBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowFeedback("Please enter a username or password.");
            }

            using (var db = new AirContext())
            {
                var user = await db.Users.SingleOrDefaultAsync(user => user.LoginId == username);

                if (user == null)
                {
                    ShowFeedback("The username or password is incorrect.");
                }
                else
                {
                    if (PasswordHandling.CheckPassword(password, user.PasswordHash))
                    {
                        // TODO: Actually sign you in.
                        Frame.Navigate(typeof(MainPage));
                    }
                    else
                    {
                        ShowFeedback("The username or password is incorrect.");
                    }
                }
            }
        }

        public async void LoginButton_Clicked(object sender, RoutedEventArgs e)
        {
            await PerformLogin();
        }

        private async void StackPanel_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                await PerformLogin();
            }
        }
    }
}
