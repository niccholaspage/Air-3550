// FlightSearchPage.xaml.cs - Air 3550 Project
//
// This program emulates a flight reservation system for a new airline,
// allowing customers to book and manage trips,as well as allowing employees
// to update the available flights and view stats on them.
//
// Authors:		Nicholas Nassar, Jacob Hammitte, Nikesh Dhital
// Class:		EECS 3550-001 Software Engineering, Spring 2021
// Instructor:	Dr. Thomas
// Date:		April 28, 2021
// Copyright:	Copyright 2021 by Nicholas Nassar, Jacob Hammitte, and Nikesh Dhital. All rights reserved.

/**
 * The page that shows the search results
 * for flights for a given departure and
 * destination city and date.
 */

using System;
using System.Threading.Tasks;
using Air_3550.Repository;
using Air_3550.Services;
using Air_3550.ViewModels;
using Database.Util;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Air_3550.Views
{
    public sealed partial class FlightSearchPage : Page
    {
        // This page uses the user session service to
        // determine if we need to navigate the user
        // to the login page or not when they are going
        // to the payment page.
        private readonly UserSessionService userSession;

        // The instance of the page parameters for the
        // flight search page, set when this page is navigated
        // to.
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

        // When this page is navigated to, we need to check
        // set our pageParams to the navigation parameteres.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            pageParams = e.Parameter as Params;

            // We need to search for flights with the
            // view model so we can show the results
            // to the user.
            Task.Run(async () =>
            {
                using var db = new AirContext();

                // We first fetch the departure and destination
                // airport based on the IDs passed into the page.
                var departureAirport = await db.Airports.FindAsync(pageParams.DepartureAirportId);
                var destinationAirport = await db.Airports.FindAsync(pageParams.DestinationAirportId);

                // If a departure flight path has already been chosen,
                // we search for return flights on the return date.
                // Otherwise, we search for departure flights on the
                // departure date.
                if (pageParams.DepartureFlightPath != null)
                {
                    await ViewModel.SearchForFlights(departureAirport, destinationAirport, (DateTime)pageParams.ReturnDate);
                }
                else
                {
                    await ViewModel.SearchForFlights(departureAirport, destinationAirport, pageParams.DepartureDate);
                }
            }).Wait();
        }

        // In the constructor, we make sure to get the
        // user session, as we will need it to determine
        // whether we can navigate the user straight to
        // the payment screen or not.
        public FlightSearchPage()
        {
            userSession = App.Current.Services.GetService<UserSessionService>();

            this.InitializeComponent();
        }

        readonly FlightSearchViewModel ViewModel = new(); // Construct the view model.

        // A computed property that the UI binds to
        // so that the user knows whether they are
        // booking a departure or return flight path.
        public string PathType => pageParams.DepartureFlightPath == null ? "Depart:" : "Return:";

        // A computed property used in the UI to show
        // the user what locations they are travelling to
        // and from.
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

        // A method used to send the user to the payment
        // page if they are signed in.
        private void proceedToPayment(FlightPath flightPath)
        {
            // We first setup a local variable for
            // payment page parameters.
            PaymentPage.Params paymentPageParams;

            // If a return date is specified,
            // we working with a round trip,
            // so we construct our two flight
            // paths with dates and make a new
            // set of payment page parameters passing
            // on those new flight paths with dates.
            if (pageParams.ReturnDate != null)
            {
                var departureFlightPathWithDate = new FlightPathWithDate(pageParams.DepartureFlightPath, pageParams.DepartureDate);

                var returnFlightPathWithDate = new FlightPathWithDate(flightPath, (DateTime)pageParams.ReturnDate);

                paymentPageParams = new PaymentPage.Params(departureFlightPathWithDate, returnFlightPathWithDate);
            }
            else
            {
                // If no return date is specified,
                // we only are focusing on a one-way,
                // so we just construct our one flight
                // path with dates and make a new
                // set of payment page parameters passing
                // on that new flight path with date.
                var departureFlightPathWithDate = new FlightPathWithDate(flightPath, pageParams.DepartureDate);

                paymentPageParams = new PaymentPage.Params(departureFlightPathWithDate, null);
            }

            // If we aren't logged in, we navigate
            // the user to the login page, passing
            // in redirect parameters so they get
            // sent back to the payment page
            // afterwards.
            if (!userSession.IsLoggedIn)
            {
                Frame.Navigate(typeof(LoginPage), new LoginPage.Params.RedirectToPage(typeof(PaymentPage), paymentPageParams));
            }
            else // otherwise, just navigate to the payment page.
            {
                Frame.Navigate(typeof(PaymentPage), paymentPageParams);
            }
        }

        // This method handles the continue button click. This will either
        // take you to the search page again to pick a return flight, or
        // process your payment if you have picked all necessary flights.
        private void ContinueButton_Click(object _, RoutedEventArgs __)
        {
            // We get the selected flight path.
            FlightPath flightPath = (FlightPath)FlightList.SelectedItem;

            // If we need to specify a return date, we check if
            // the user has already chosen a departure flight path.
            // If so, we proceed to payment. Otherwise, we navigate
            // the user to the flight search page again so they can
            // choose their return flight path.
            if (pageParams.ReturnDate != null)
            {
                if (pageParams.DepartureFlightPath == null)
                {
                    Frame.Navigate(typeof(FlightSearchPage), new Params(pageParams.DestinationAirportId, pageParams.DepartureAirportId, pageParams.DepartureDate, pageParams.ReturnDate, flightPath));
                }
                else
                {
                    proceedToPayment(flightPath);
                }
            }
            else
            {
                proceedToPayment(flightPath);
            }
        }
    }
}
