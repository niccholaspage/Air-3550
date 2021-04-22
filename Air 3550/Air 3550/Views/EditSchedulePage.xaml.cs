
using Air_3550.Models;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using FileSavePicker = Windows.Storage.Pickers.FileSavePicker;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditSchedulePage : Page
    {

        //readonly EditScheduleViewModel ViewModel = new();

        public EditSchedulePage()
        {
            this.InitializeComponent();
            this.Loaded += ViewModel.UpdateFlights;

        }

        readonly EditScheduleViewModel ViewModel = new();


        private async void AddFlight_Click(object sender, RoutedEventArgs e)
        {
            AddFlightDialog dialog1 = new();
            dialog1.XamlRoot = this.Content.XamlRoot;
            var result = await dialog1.ShowAsync();
            //Update if something changed
            if (dialog1.Result != null)
            {
                await ViewModel.UpdateFlights();
                ViewModel.Feedback = "FlightID:" + dialog1.Result.Number;
            }
        }

        private async void RemoveFlight_Click(object sender, RoutedEventArgs e)
        {
            Flight cancel = (Flight)displayedList.SelectedItem;
            if (cancel != null)
            {
                ViewModel.CancelFlight(cancel);
                await ViewModel.UpdateFlights();
            }
        }

        private async void EditFlight_Click(object sender, RoutedEventArgs e)
        {
            Flight edit = (Flight)displayedList.SelectedItem;
            if (edit != null)
            {
                EditFlightDialog dialog1 = new(edit);
                dialog1.XamlRoot = this.Content.XamlRoot;
                var result = await dialog1.ShowAsync();
                //Update if something changed
                if (dialog1.Result != null) await ViewModel.UpdateFlights();
            }
        }

        private async void EditPlane_Click(object sender, RoutedEventArgs e)
        {
            Flight edit = (Flight)displayedList.SelectedItem;
            if (edit != null)
            {
                EditPlaneDialog dialog1 = new EditPlaneDialog(edit);
                dialog1.XamlRoot = this.Content.XamlRoot;
                var result = await dialog1.ShowAsync();
                //Update if something changed
                if (dialog1.Result != null) await ViewModel.UpdateFlights();
            }
        }

        private async void GetSummary_Click(object sender, RoutedEventArgs e)
        {
            FileSavePicker savePicker = new FileSavePicker
            {
                //SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };

            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("Rich Text", new List<string>() { ".rtf" });

            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "New Document";



            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                // Prevent updates to the remote version of the file until we 
                // finish making changes and call CompleteUpdatesAsync.
                CachedFileManager.DeferUpdates(file);
                // write to file
                using (Windows.Storage.Streams.IRandomAccessStream randAccStream =
                    await file.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite))
                {
                    //editor.Document.SaveToStream(Windows.UI.Text.TextGetOptions.FormatRtf, randAccStream);
                }

                // Let Windows know that we're finished changing the file so the 
                // other app can update the remote version of the file.
                FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
                if (status != FileUpdateStatus.Complete)
                {
                    Windows.UI.Popups.MessageDialog errorBox =
                        new Windows.UI.Popups.MessageDialog("File " + file.Name + " couldn't be saved.");
                    await errorBox.ShowAsync();
                }
            }
        }

    }
}
