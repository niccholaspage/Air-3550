using Air_3550.Repository;
using Air_3550.Util;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FlightSearchPage : Page
    {
        public class Params
        {
            public int DepartureAirportId;
            public int DestinationAirportId;
            public DateTime DepartureDate;
            public DateTime? ReturnDate;

            public Params(int departureAirportId, int destinationAirportId, DateTime departureDate, DateTime? returnDate)
            {
                this.DepartureAirportId = departureAirportId;
                this.DestinationAirportId = destinationAirportId;
                this.DepartureDate = departureDate;
                this.ReturnDate = returnDate;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var param = e.Parameter as Params;

            Task.Run(async () =>
            {
                using (var db = new AirContext())
                {
                    var departureAirport = await db.Airports.FindAsync(param.DepartureAirportId);
                    var destinationAirport = await db.Airports.FindAsync(param.DestinationAirportId);

                    await ViewModel.SearchForFlights(departureAirport, destinationAirport, param.DepartureDate);
                }
            }).Wait();
        }

        public FlightSearchPage()
        {
            this.InitializeComponent();
        }

        FlightSearchViewModel ViewModel = new();

        public string Subtitle
        {
            get
            {
                if (ViewModel.OriginAirport == null || ViewModel.DestinationAirport == null)
                {
                    return "";
                }
                else
                {
                    return ViewModel.OriginAirport.CityWithState + " to " + ViewModel.DestinationAirport.CityWithState;
                }
            }
        }

        private void OpenPayment_Click(object _, RoutedEventArgs __)
        {
            FlightPath departingFlightPath = (FlightPath)FlightList.SelectedItem;

            // TODO: Take care of picking the return flight then going to the payment page.
            Frame.Navigate(typeof(PaymentPage), new PaymentPage.Params(departingFlightPath, null));
        }
    }
}
