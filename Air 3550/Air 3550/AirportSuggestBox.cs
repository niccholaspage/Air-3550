using Air_3550.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550
{
    public sealed class AirportSuggestBox : Control
    {
        private List<string> AirportNames = new List<string>();

        public AirportSuggestBox()
        {
            this.DefaultStyleKey = typeof(AirportSuggestBox);

            this.Loaded += PopulateAirportNames;
        }

        private async void PopulateAirportNames(object sender, RoutedEventArgs e)
        {
            using (var db = new AirContext())
            {
                var airports = await db.Airports.ToListAsync();

                AirportNames.AddRange(airports.Select(airport => airport.Code));
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var autoSuggestBox = GetTemplateChild("SuggestBox") as AutoSuggestBox;
            autoSuggestBox.TextChanged += AutoSuggestBox_TextChanged;
        }

        // Handle text change and present suitable items
        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var suitableItems = new List<string>();
            var splitText = sender.Text.ToLower().Split(" ");
            foreach (var airportName in AirportNames)
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
