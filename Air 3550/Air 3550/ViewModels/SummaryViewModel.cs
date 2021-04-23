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
using System.IO;

namespace Air_3550.Views
{
    class SummaryViewModel : ObservableValidator
    {
        private List<ScheduledFlight> _sflights;
        private List<string> lines = new List<string>();

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
                    .Include(ScheduledFlight => ScheduledFlight.Flight.Plane)
                    .Include(ScheduledFlight => ScheduledFlight.Tickets)
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
            savePicker.FileTypeChoices.Add(".csv", new List<string>() { ".csv" });

            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "New Document";

            StorageFile file = await savePicker.PickSaveFileAsync();

            lines.Add("ScheduledFlightId,Capacity,Tickets,Cost Per Ticket");

            foreach (ScheduledFlight sF in Sflights)
            {
                lines.Add(
                    sF.ScheduledFlightId + "," + sF.Flight.Plane.MaxSeats + "," + sF.Tickets.Count + "," + sF.Flight.GetCost()
                    );
            }

            //Grabs File Path to write using System.IO
            String FilePath = file.Path;
            File.WriteAllLines(FilePath, lines);
            
        }
    }
}
