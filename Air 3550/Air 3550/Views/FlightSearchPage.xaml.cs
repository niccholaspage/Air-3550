﻿using Air_3550.ViewModels;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FlightSearchPage : Page
    {
        public FlightSearchPage()
        {
            this.InitializeComponent();
        }

        FlightSearchViewModel ViewModel = new();

        public string Subtitle => ViewModel.OriginAirport.CityWithState + " to " + ViewModel.DestinationAirport.CityWithState;
    }
}
