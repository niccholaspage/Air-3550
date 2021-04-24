using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SummaryPage : Page
    {
        public SummaryPage()
        {
            this.InitializeComponent();
            this.Loaded += async (_, __) => await ViewModel.updateSflights();
        }

        readonly SummaryViewModel ViewModel = new();

        public async void SaveCSV_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.SaveSummary();
        }

        public async void UpdateDates_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.updateSflightsDate();
        }
    }
}
