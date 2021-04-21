using Air_3550.Models;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditPlanesPage : Page
    {

        //readonly EditScheduleViewModel ViewModel = new();

        public EditPlanesPage()
        {
            this.InitializeComponent();
            ViewModel.UpdateFlights();

        }

        readonly EditPlanesViewModel ViewModel = new();

        private async void EditFlight_Click(object sender, RoutedEventArgs e)
        {
            Flight edit = (Flight)displayedList.SelectedItem;
            if (edit != null)
            {
                EditPlaneDialog dialog1 = new EditPlaneDialog(edit);
                dialog1.XamlRoot = this.Content.XamlRoot;
                var result = await dialog1.ShowAsync();
                //Update if something changed
                if (dialog1.Result != null) ViewModel.UpdateFlights();
            }
        }


    }
}
