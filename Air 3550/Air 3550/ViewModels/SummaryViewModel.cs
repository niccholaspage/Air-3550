using Air_3550.Models;
using Air_3550.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using FileSavePicker = Windows.Storage.Pickers.FileSavePicker;

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

        private DateTimeOffset? _startDate;

        public DateTimeOffset? StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        private DateTimeOffset? _endDate;

        public DateTimeOffset? EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        private string _feedback;

        public string Feedback
        {
            get => _feedback;
            set => SetProperty(ref _feedback, value);
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

        public async Task updateSflightsDate()
        {
            DateTime Start = ((DateTimeOffset)StartDate).DateTime.Date;
            DateTime End = ((DateTimeOffset)EndDate).DateTime.Date;
            //Do null trick to Go to infinity/-infinity if one is not set
            using (var db = new AirContext())
            {
                Sflights = await db.ScheduledFlights
                    .Include(ScheduledFlight => ScheduledFlight.Flight.DestinationAirport)
                    .Include(ScheduledFlight => ScheduledFlight.Flight.OriginAirport)
                    .Include(ScheduledFlight => ScheduledFlight.Flight.Plane)
                    .Include(ScheduledFlight => ScheduledFlight.Tickets)
                    .Where(ScheduledFlight => (ScheduledFlight.DepartureDate >= Start)&&(ScheduledFlight.DepartureDate <= End))
                    .ToListAsync();
            }
        }

        public async Task SaveSummary()
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
            if (file != null)
            {
                String FilePath = file.Path;
                File.WriteAllLines(FilePath, lines);
            }

        }
    }
}
