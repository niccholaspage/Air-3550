﻿using Air_3550.ViewModels;
using Air_3550.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Air_3550.Repository;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditSchedulePage : Page
    {

        private List<Flight> FlightsA;

        public EditSchedulePage()
        {
            this.InitializeComponent();
            using (var db = new AirContext()) {
                FlightsA = db.Flights.ToList();
            }

        }

        private async void AddFlight_Click(object sender, RoutedEventArgs e)
        {
            AddFlightDialog dialog1 = new AddFlightDialog();
            dialog1.XamlRoot = this.Content.XamlRoot;
            var result = await dialog1.ShowAsync();
        }
        

    }
}
