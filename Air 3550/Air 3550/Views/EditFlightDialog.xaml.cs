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
        private readonly Flight Editting;
        public Flight Result { get; private set; }

        public EditFlightDialog(Flight editting)
        {
            this.InitializeComponent();
            ViewModel.GrabValues(editting);
            Editting = editting;
            Result = null;
        }

        readonly EditFlightViewModel ViewModel = new();

        public async void EditFlight_Click(object sender, RoutedEventArgs e)
        {
            var result = await ViewModel.EditFlight(Editting);
            if (result != null) this.Hide();
            Result = result;
        }
    }

}
