﻿// FlightSearchPage.xaml.cs - Air 3550 Project
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FlightSearchPage : Page
    {
        private readonly UserSessionService userSession;

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
                using var db = new AirContext();
                var departureAirport = await db.Airports.FindAsync(pageParams.DepartureAirportId);
                var destinationAirport = await db.Airports.FindAsync(pageParams.DestinationAirportId);

                if (pageParams.DepartureFlightPath != null)
                {
                    await ViewModel.SearchForFlights(departureAirport, destinationAirport, (DateTime) pageParams.ReturnDate);
                }
                else
                {
                    await ViewModel.SearchForFlights(departureAirport, destinationAirport, pageParams.DepartureDate);
                }
            }).Wait();
        }

        public FlightSearchPage()
        {
            userSession = App.Current.Services.GetService<UserSessionService>();

            this.InitializeComponent();
        }

        readonly FlightSearchViewModel ViewModel = new();

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

        private void proceedToPayment(FlightPath flightPath)
        {
            PaymentPage.Params paymentPageParams;

            if (pageParams.ReturnDate != null)
            {
                var departureFlightPathWithDate = new FlightPathWithDate(pageParams.DepartureFlightPath, pageParams.DepartureDate);

                var returnFlightPathWithDate = new FlightPathWithDate(flightPath, (DateTime)pageParams.ReturnDate);

                paymentPageParams = new PaymentPage.Params(departureFlightPathWithDate, returnFlightPathWithDate);
            }
            else
            {
                var departureFlightPathWithDate = new FlightPathWithDate(flightPath, pageParams.DepartureDate);

                paymentPageParams = new PaymentPage.Params(departureFlightPathWithDate, null);
            }

            if (!userSession.IsLoggedIn)
            {
                Frame.Navigate(typeof(LoginPage), new LoginPage.Params.RedirectToPage(typeof(PaymentPage), paymentPageParams));
            }
            else
            {
                Frame.Navigate(typeof(PaymentPage), paymentPageParams);
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
