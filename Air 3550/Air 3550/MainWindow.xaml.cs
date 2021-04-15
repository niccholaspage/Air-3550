using Air_3550.Services;
using Air_3550.Views;
using Microsoft.UI.Xaml;
using Microsoft.Extensions.DependencyInjection;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private readonly UserSessionService userSession;

        public MainWindow()
        {
            this.InitializeComponent();

            userSession = App.Current.Services.GetService<UserSessionService>();

            Title = "Air 3550";

            ContentFrame.Navigate(typeof(MainPage));
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(LoginPage));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (ContentFrame.BackStackDepth > 0)
            {
                ContentFrame.GoBack();
            }
        }
    }
}
