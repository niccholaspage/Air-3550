using Air_3550.Models;
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
    public sealed partial class EditFlightDialog : ContentDialog
    {
        readonly EditFlightViewModel ViewModel;

        public Flight Result { get; private set; }

        public EditFlightDialog(Flight flight)
        {
            this.InitializeComponent();
            Result = null;
            ViewModel = new(flight);
        }

        public async void EditFlight_Click(object _, ContentDialogButtonClickEventArgs __)
        {
            var result = await ViewModel.EditFlight();
            if (result != null) this.Hide();
            Result = result;
        }
    }

}
