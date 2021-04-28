// PaymentPage.xaml.cs - Air 3550 Project
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
using Air_3550.ViewModels;
using Database.Util;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    public sealed partial class PaymentPage : Page
    {
        private Params pageParams;

        public class Params
        {
            public FlightPathWithDate DepartingFlightPathWithDate;
            public FlightPathWithDate ReturnFlightPathWithDate;

            public Params(FlightPathWithDate departingFlightPathWithDate, FlightPathWithDate returnFlightPathWithDate)
            {
                DepartingFlightPathWithDate = departingFlightPathWithDate;
                ReturnFlightPathWithDate = returnFlightPathWithDate;
            }
        }

        public PaymentPage()
        {
            this.InitializeComponent();

            this.Loaded += async (_, __) => await ViewModel.FetchBalances();
        }

        public string GetFormattedTotalCost()
        {
            return ViewModel.TotalCost.FormatAsMoney();
        }

        readonly PaymentViewModel ViewModel = new();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            pageParams = e.Parameter as Params;

            ViewModel.DepartingFlightPathWithDate = pageParams.DepartingFlightPathWithDate;
            ViewModel.ReturnFlightPathWithDate = pageParams.ReturnFlightPathWithDate;

            DepartureFlightPathControl.DataContext = ViewModel.DepartingFlightPathWithDate;

            if (ViewModel.ReturnFlightPathWithDate != null)
            {
                ReturnFlightPathControl.DataContext = ViewModel.ReturnFlightPathWithDate;
            }
        }

        private async void PurchaseButton_Click(object _, RoutedEventArgs __)
        {
            if (await ViewModel.PurchaseTrip())
            {
                Frame.Navigate(typeof(MainPage), new MainPage.Params(true));

                Frame.BackStack.Clear();
            }
        }
    }
}
