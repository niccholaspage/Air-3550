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

/**
 * This control is an extension to the AutoSuggestBox,
 * loading its suggestions from either the airports or
 * planes available in the database.
 */

using System.Collections.Generic;
using System.Linq;
using Air_3550.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Air_3550.Controls
{
    // An enumeration defining the supported
    // entity types. Technically we could make
    // this generic and allow the use of any
    // type in the database, but that would
    // be overkill for these two simple cases.
    public enum EntityType
    {
        Airport,
        Plane
    }

    public sealed partial class EntitySuggestBox : UserControl
    {
        // We create two lists, one for
        // the primary IDs of each entity,
        // and another for the entity names
        // for display in the list.
        private readonly List<int> EntityIds = new();
        private readonly List<string> EntityNames = new();

        // We expose a property so that users in the
        // XAML can determine which entity type they
        // would like to show in the dropdowns.
        public EntityType? EntityType { get; set; }

        // We expose the Text to allow XAML users
        // of the control to see what text is in the
        // AutoSuggestBox.
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        // We expose the label that appears above the
        // AutoSuggestBox to allow users to set it.
        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        // Finally, we expose the selected entity ID
        // to allow for consumers to easily get the entity
        // ID they currently have selected.
        public int? SelectedEntityId
        {
            get => (int?)GetValue(SelectedEntityIdProperty);
            set => SetValue(SelectedEntityIdProperty, value);
        }

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(nameof(Label), typeof(string), typeof(EntitySuggestBox), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(EntitySuggestBox), new PropertyMetadata(default(string), OnTextPropertyChanged));
        public static readonly DependencyProperty SelectedEntityIdProperty = DependencyProperty.Register(nameof(SelectedEntityId), typeof(int?), typeof(EntitySuggestBox), new PropertyMetadata(default(int?)));

        // When the text property changes, we need
        // to update the SelectedEntityId to the
        // proper index.
        private static void OnTextPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            // We get the entity suggest box and then grab the index
            // the input text corresponds to.
            var entitySuggestBox = (EntitySuggestBox)dependencyObject;

            // We get the index of the entity with the name from the suggest box text.
            var index = entitySuggestBox.EntityNames.IndexOf(entitySuggestBox.Text);

            // If there is no index,
            if (index == -1)
            {
                // we just set the SelectedEntityId to null
                // because we couldn't find an item in the
                // list.
                entitySuggestBox.SelectedEntityId = null;
            }
            else
            {
                // Otherwise we just set the selected entity
                // ID to be based on the index plus 1, because
                // SQL is one-indexed.
                entitySuggestBox.SelectedEntityId = index + 1;
            }
        }

        public EntitySuggestBox()
        {
            this.InitializeComponent();

            // We listen for the Loaded event and populate the
            // entity names once your ready.
            this.Loaded += PopulateEntityNames;
        }

        // This method populates the list of entity names and IDs from the database.
        private async void PopulateEntityNames(object sender, RoutedEventArgs e)
        {
            using var db = new AirContext(); // We get a DB context
            //
            // If we are working with an airport,
            if (EntityType == Controls.EntityType.Airport)
            {
                // we simply pull all the airports and add
                // each ID and name to the proper entity
                // lists.
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
                suitableItems.Add("No results found");
            }
            sender.ItemsSource = suitableItems;
        }
    }
}
