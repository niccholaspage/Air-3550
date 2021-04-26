using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using System;
using Windows.System;

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

            this.Loaded += (_, __) =>
            {
                DepartureCityBox.Focus(FocusState.Programmatic);
            };

            DepartureDatePicker.MinDate = DateTime.Now;
            DepartureDatePicker.MaxDate = DateTime.Now.AddMonths(6);

            ReturnDatePicker.MinDate = DateTime.Now;
            ReturnDatePicker.MaxDate = DateTime.Now.AddMonths(6);
        }

        readonly MainViewModel ViewModel = new();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is not null)
            {
                BookedFlightsInfoBar.IsOpen = true;
            }
        }

        private void handleSearch()
        {
            if (ViewModel.CheckAndGiveFeedbackOnSearch())
            {
                var departureDate = ViewModel.DepartureDate.Value.Date;
                DateTime? returnDate = ViewModel.ReturnDate == null ? null : ViewModel.ReturnDate.Value.Date;
                Frame.Navigate(typeof(FlightSearchPage), new FlightSearchPage.Params(ViewModel.DepartureAirportId.Value, ViewModel.DestinationAirportId.Value, departureDate, returnDate, null));
            }
        }

        private void SearchButton_Click(object _, RoutedEventArgs __)
        {
            handleSearch();
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

        private void StackPanel_KeyDown(object _, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                handleSearch();
            }
        }
    }
}
