
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
    public sealed partial class EditSchedulePage : Page
    {

        //readonly EditScheduleViewModel ViewModel = new();

        public EditSchedulePage()
        {
            this.InitializeComponent();
            this.Loaded += async (_, __) => await ViewModel.UpdateFlights();

        }

        readonly EditScheduleViewModel ViewModel = new();


        private async void AddFlight_Click(object sender, RoutedEventArgs e)
        {
            AddFlightDialog dialog1 = new();
            dialog1.XamlRoot = this.Content.XamlRoot;
            var result = await dialog1.ShowAsync();
            //Update if something changed
            if (dialog1.Result != null)
            {
                await ViewModel.UpdateFlights();
                ViewModel.Feedback = "FlightID:" + dialog1.Result.Number;
            }
        }

        private async void RemoveFlight_Click(object sender, RoutedEventArgs e)
        {
            Flight cancel = (Flight)displayedList.SelectedItem;
            if (cancel != null)
            {
                ViewModel.CancelFlight(cancel);
                await ViewModel.UpdateFlights();
            }
        }

        private async void EditFlight_Click(object sender, RoutedEventArgs e)
        {
            Flight edit = (Flight)displayedList.SelectedItem;
            if (edit != null)
            {
                EditFlightDialog dialog1 = new(edit);
                dialog1.XamlRoot = this.Content.XamlRoot;
                var result = await dialog1.ShowAsync();
                //Update if something changed
                if (dialog1.Result != null) await ViewModel.UpdateFlights();
            }
        }

        private async void EditPlane_Click(object sender, RoutedEventArgs e)
        {
            Flight edit = (Flight)displayedList.SelectedItem;
            if (edit != null)
            {
                EditPlaneDialog dialog1 = new EditPlaneDialog(edit);
                dialog1.XamlRoot = this.Content.XamlRoot;
                var result = await dialog1.ShowAsync();
                //Update if something changed
                if (dialog1.Result != null) await ViewModel.UpdateFlights();
            }
        }

        private void GetSummary_Click(object _, RoutedEventArgs __)
        {
            Frame.Navigate(typeof(SummaryPage));
        }

    }
}
