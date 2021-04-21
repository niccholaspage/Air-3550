using Air_3550.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Linq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Controls
{
    public sealed partial class AirportSuggestBox : UserControl
    {
        private readonly List<int> AirportIds = new();
        private readonly List<string> AirportNames = new();

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

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(nameof(Label), typeof(string), typeof(AirportSuggestBox), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(AirportSuggestBox), new PropertyMetadata(default(string)));

        public AirportSuggestBox()
        {
            this.InitializeComponent();

            this.Loaded += PopulateAirportNames;
        }

        public int? GetSelectedAirportId()
        {
            var index = AirportNames.IndexOf(Text);

            if (index == -1)
            {
                return null;
            } else
            {
                return index;
            }
        }

        private async void PopulateAirportNames(object sender, RoutedEventArgs e)
        {
            using (var db = new AirContext())
            {
                var airports = await db.Airports.ToListAsync();

                foreach (var airport in airports)
                {
                    AirportIds.Add(airport.AirportId);
                    AirportNames.Add(airport.City + ", " + airport.State + " (" + airport.Code + ")");
                }
            }
        }

        // Handle text change and present suitable items
        private void SuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
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
