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
    public sealed partial class EditPlaneDialog : ContentDialog
    {
        public Flight Result { get; private set; }

        readonly EditPlaneViewModel ViewModel;

        public EditPlaneDialog(Flight flight)
        {
            ViewModel = new(flight);
            this.InitializeComponent();
            Result = null;
        }

        public async void EditPlane_Click(object _, ContentDialogButtonClickEventArgs e)
        {
            var result = await ViewModel.EditFlight();

            if (result == null)
            {
                Result = result;

                e.Cancel = true;
            }
        }
    }

}
