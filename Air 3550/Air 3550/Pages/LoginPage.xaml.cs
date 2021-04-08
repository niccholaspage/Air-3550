// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using Air_3550.Repository;
using Database.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

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

        public async void LoginButton_Clicked(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Feedback.Text = "Please enter a username or password.";
                Feedback.Visibility = Visibility.Visible;
            }

            using (var db = new AirContext())
            {
                var user = await db.Users.SingleOrDefaultAsync(user => user.LoginId == username);

                if (user == null)
                {
                    Feedback.Visibility = Visibility.Visible;
                    Feedback.Text = "The username or password is incorrect.";
                }
                else
                {
                    if (PasswordHandling.CheckPassword(password, user.PasswordHash))
                    {
                        Feedback.Visibility = Visibility.Visible;
                        Feedback.Text = "Good work! :)";
                    }
                    else
                    {
                        Feedback.Visibility = Visibility.Visible;
                        Feedback.Text = "The username or password is incorrect.";
                    }
                }
            }
        }
    }
}
