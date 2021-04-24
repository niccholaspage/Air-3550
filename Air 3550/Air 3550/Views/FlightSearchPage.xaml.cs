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
        private Params pageParams;

        public class Params
        {
            public int DepartureAirportId;
            public int DestinationAirportId;
            public DateTime DepartureDate;
            public DateTime? ReturnDate;
            public FlightPath DepartureFlightPath;

            public Params(int departureAirportId, int destinationAirportId, DateTime departureDate, DateTime? returnDate, FlightPath departureFlightPath)
            {
                DepartureAirportId = departureAirportId;
                DestinationAirportId = destinationAirportId;
                DepartureDate = departureDate;
                ReturnDate = returnDate;
                DepartureFlightPath = departureFlightPath;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            pageParams = e.Parameter as Params;


            // TODO: Don't await.
            Task.Run(async () =>
            {
                using (var db = new AirContext())
                {
                    var departureAirport = await db.Airports.FindAsync(pageParams.DepartureAirportId);
                    var destinationAirport = await db.Airports.FindAsync(pageParams.DestinationAirportId);

                    await ViewModel.SearchForFlights(departureAirport, destinationAirport, pageParams.DepartureDate);
                }
            }).Wait();
        }

        public FlightSearchPage()
        {
            this.InitializeComponent();
        }

        FlightSearchViewModel ViewModel = new();

        public string PathType => pageParams.DepartureFlightPath == null ? "Depart:" : "Return:";

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

        private void ContinueButton_Click(object _, RoutedEventArgs __)
        {
            FlightPath flightPath = (FlightPath)FlightList.SelectedItem;

            if (pageParams.ReturnDate != null)
            {
                if (pageParams.DepartureFlightPath == null)
                {
                    Frame.Navigate(typeof(FlightSearchPage), new Params(pageParams.DestinationAirportId, pageParams.DepartureAirportId, pageParams.DepartureDate, pageParams.ReturnDate, flightPath));
                }
                else
                {
                    Frame.Navigate(typeof(PaymentPage), new PaymentPage.Params(pageParams.DepartureFlightPath, flightPath,
                        pageParams.DepartureDate, pageParams.ReturnDate));
                }
            }
            else
            {
                Frame.Navigate(typeof(PaymentPage), new PaymentPage.Params(flightPath, null,
                    pageParams.DepartureDate, pageParams.ReturnDate));
            }
        }
    }
}
