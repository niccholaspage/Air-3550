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
        public AirportSuggestBox()
        {
            this.DefaultStyleKey = typeof(AirportSuggestBox);
        }

        // List of cats
        private List<string> Cats = new List<string>()
        {
            "Abyssinian",
            "Aegean",
            "American Bobtail"
        };

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
            foreach (var cat in Cats)
            {
                var found = splitText.All((key) =>
                {
                    return cat.ToLower().Contains(key);
                });
                if (found)
                {
                    suitableItems.Add(cat);
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
