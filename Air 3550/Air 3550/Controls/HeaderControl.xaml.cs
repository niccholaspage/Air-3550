using Air_3550.Services;
using Air_3550.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Controls
{
    public sealed partial class HeaderControl : UserControl
    {
        private readonly UserSessionService userSession;

        public Frame ContentFrame { get; set; }

        public static readonly DependencyProperty ContentFrameProperty = DependencyProperty.Register(nameof(ContentFrame), typeof(Frame), typeof(AirportSuggestBox), new PropertyMetadata(default(Frame)));

        public HeaderControl()
        {
            userSession = App.Current.Services.GetService<UserSessionService>();

            this.InitializeComponent();
        }

        private void LoginButton_Click(object _sender, RoutedEventArgs _1)
        {
            ContentFrame.Navigate(typeof(LoginPage));
        }

        private void AccountInfoButton_Click(object _, RoutedEventArgs _1)
        {
            ContentFrame.Navigate(typeof(MyAccountPage));
        }

        private void LogoutButton_Click(object _, RoutedEventArgs _1)
        {
            userSession.Logout();

            ContentFrame.Navigate(typeof(MainPage));

            ContentFrame.BackStack.Clear();
        }

        private void BackButton_Click(object _, RoutedEventArgs _2)
        {
            if (ContentFrame.BackStackDepth > 0)
            {
                ContentFrame.GoBack();
            }
        }
    }
}
