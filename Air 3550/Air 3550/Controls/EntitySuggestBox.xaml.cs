// EntitySuggestBox.xaml.cs - Air 3550 Project
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

using System.Collections.Generic;
using System.Linq;
using Air_3550.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Controls
{
    public enum EntityType
    {
        Airport,
        Plane
    }

    public sealed partial class EntitySuggestBox : UserControl
    {
        private readonly List<int> EntityIds = new();
        private readonly List<string> EntityNames = new();

        public EntityType? EntityType { get; set; }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public int? SelectedEntityId
        {
            get => (int?)GetValue(SelectedEntityIdProperty);
            set => SetValue(SelectedEntityIdProperty, value);
        }

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(nameof(Label), typeof(string), typeof(EntitySuggestBox), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(EntitySuggestBox), new PropertyMetadata(default(string), OnTextPropertyChanged));
        public static readonly DependencyProperty SelectedEntityIdProperty = DependencyProperty.Register(nameof(SelectedEntityId), typeof(int?), typeof(EntitySuggestBox), new PropertyMetadata(default(int?)));

        private static void OnTextPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var airportSuggestBox = (EntitySuggestBox)dependencyObject;
            var index = airportSuggestBox.EntityNames.IndexOf(airportSuggestBox.Text);

            if (index == -1)
            {
                airportSuggestBox.SelectedEntityId = null;
            }
            else
            {
                airportSuggestBox.SelectedEntityId = index + 1;
            }
        }

        public EntitySuggestBox()
        {
            this.InitializeComponent();

            this.Loaded += PopulateEntityNames;
        }

        private async void PopulateEntityNames(object sender, RoutedEventArgs e)
        {
            using var db = new AirContext();
            if (EntityType == Controls.EntityType.Airport)
            {
                var airports = await db.Airports.ToListAsync();

                foreach (var airport in airports)
                {
                    EntityIds.Add(airport.AirportId);
                    EntityNames.Add(airport.CityWithStateWithCode);
                }
            }
            else
            {
                var planes = await db.Planes.ToListAsync();

                foreach (var plane in planes)
                {
                    EntityIds.Add(plane.PlaneId);
                    EntityNames.Add(plane.Model + " (" + plane.MaxSeats + " seats)");
                }
            }

            if (SelectedEntityId is int index)
            {
                Text = EntityNames[index - 1];
            }
        }

        // Handle text change and present suitable items
        private void SuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var suitableItems = new List<string>();
            var splitText = sender.Text.ToLower().Split(" ");

            foreach (var airportName in EntityNames)
            {
                var found = splitText.All((key) =>
                {
                    return airportName.ToLower().Contains(key);
                });
                if (found)
                {
                    suitableItems.Add(airportName);
                }
            }
            if (suitableItems.Count == 0)
            {
                suitableItems.Add("No results found"); // Stop this from being selectable somehow
            }
            sender.ItemsSource = suitableItems;
        }
    }
}
