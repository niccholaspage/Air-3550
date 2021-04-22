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
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            DepartureDatePicker.MinDate = DateTime.Now;
            DepartureDatePicker.MaxDate = DateTime.Now.AddMonths(6);

            ReturnDatePicker.MinDate = DateTime.Now;
            ReturnDatePicker.MaxDate = DateTime.Now.AddMonths(6);
        }

        readonly MainViewModel ViewModel = new();

        private void SearchButton_Click(object _, RoutedEventArgs __)
        {
            if (ViewModel.CheckAndGiveFeedbackOnSearch())
            {
                var departureDate = ViewModel.DepartureDate.Value.Date;
                DateTime? returnDate = ViewModel.ReturnDate == null ? null : ViewModel.ReturnDate.Value.Date;
                Frame.Navigate(typeof(FlightSearchPage), new FlightSearchPage.Params(ViewModel.DepartureAirportId.Value, ViewModel.DestinationAirportId.Value, departureDate, returnDate));
            }
        }

        private void EditButton_Click(object _, RoutedEventArgs __)
        {
            Frame.Navigate(typeof(EditSchedulePage));
        }

        private void TripTypeRadioButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is RadioButtons radioButtons)
            {
                // Round trip
                if (radioButtons.SelectedIndex == 0)
                {
                    ViewModel.IsRoundTrip = true;
                    ReturnDatePicker.IsEnabled = true;
                }
                else // One-way
                {
                    ViewModel.IsRoundTrip = false;
                    ViewModel.ReturnDate = null;
                    ReturnDatePicker.IsEnabled = false;
                }
            }
        }
    }
}
