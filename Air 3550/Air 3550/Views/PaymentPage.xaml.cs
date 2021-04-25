﻿using Air_3550.Util;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PaymentPage : Page
    {
        private Params pageParams;

        public class Params
        {
            public FlightPath DepartingFlightPath;
            public FlightPath ReturnFlightPath;
            public DateTime DepartureDate;
            public DateTime? ReturnDate;

            public Params(FlightPath departingFlightPath, FlightPath returnFlightPath, DateTime departureDate, DateTime? returnDate)
            {
                DepartingFlightPath = departingFlightPath;
                ReturnFlightPath = returnFlightPath;
                DepartureDate = departureDate;
                ReturnDate = returnDate;
            }
        }

        public PaymentPage()
        {
            this.InitializeComponent();
        }

        readonly PaymentViewModel ViewModel = new();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            pageParams = e.Parameter as Params;

            ViewModel.DepartingFlightPath = pageParams.DepartingFlightPath;
            ViewModel.ReturnFlightPath = pageParams.ReturnFlightPath;

            DepartureFlightPathControl.DataContext = ViewModel.DepartingFlightPath;
            ViewModel.TotalCost = ViewModel.DepartingFlightPath.Price;

            if (ViewModel.ReturnFlightPath != null)
            {
                ReturnFlightPathControl.DataContext = ViewModel.ReturnFlightPath;
                ViewModel.TotalCost += ViewModel.DepartingFlightPath.Price;
            }
        }
    }
}
