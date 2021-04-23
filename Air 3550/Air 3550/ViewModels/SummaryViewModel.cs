using Air_3550.Models;
using Air_3550.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Provider;
using FileSavePicker = Windows.Storage.Pickers.FileSavePicker;

namespace Air_3550.Views
{
    class SummaryViewModel : ObservableValidator
    {
        private List<ScheduledFlight> _sflights;

        public List<ScheduledFlight> Sflights
        {
            get => _sflights;
            set => SetProperty(ref _sflights, value);
        }

        public async Task updateSflights()
        {
            using (var db = new AirContext())
            {
                Sflights = await db.ScheduledFlights
                    .Include(ScheduledFlight => ScheduledFlight.Flight.DestinationAirport)
                    .Include(ScheduledFlight => ScheduledFlight.Flight.OriginAirport)
                    .ToListAsync();
            }
        }

        public async Task SaveFile()
        {
            FileSavePicker savePicker = new FileSavePicker
            {
                //SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };

            MainWindow.FixPicker(savePicker);

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
