using Air_3550.Models;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public sealed partial class AddFlightDialog : ContentDialog
    {
        public AddFlightDialog()
        {
            this.InitializeComponent();
        }

        readonly AddFlightViewModel ViewModel = new();

        private async void AddFlight_Click(object _, ContentDialogButtonClickEventArgs e)
        {
            Flight result = await ViewModel.CreateFlight();

            if (result == null)
            {
                e.Cancel = true;
            }
        }
    }
}
