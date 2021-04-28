// SummaryViewModel.cs - Air 3550 Project
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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Air_3550.Models;
using Air_3550.Repository;
using Air_3550.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace Air_3550.ViewModels
{
    public record ScheduledFlightWithManifest(ScheduledFlight ScheduledFlight, bool IsFlightManager);

    class SummaryViewModel : ObservableValidator
    {
        private readonly UserSessionService userSessionService;

        public ObservableCollection<ScheduledFlightWithManifest> ScheduledFlightsWithManifest = new();

        private bool _isFlightManager;

        public bool IsFlightManager
        {
            get => _isFlightManager;
            set => SetProperty(ref _isFlightManager, value);
        }

        private List<ScheduledFlight> _scheduledFlights;

        public List<ScheduledFlight> ScheduledFlights
        {
            get => _scheduledFlights;
            set => SetProperty(ref _scheduledFlights, value);
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

        public SummaryViewModel()
        {
            userSessionService = App.Current.Services.GetService<UserSessionService>();

            // If the marketing manager is on this page, we hide
            // the delete button because they are only allowed to
            // edit planes on flights and cannot delete a flight.
            IsFlightManager = userSessionService.Role == Role.FLIGHT_MANAGER;
        }

        public async Task UpdateScheduledFlights()
        {
            using var db = new AirContext();
            ScheduledFlights = await db.ScheduledFlights
                .Include(ScheduledFlight => ScheduledFlight.Flight.DestinationAirport)
                .Include(ScheduledFlight => ScheduledFlight.Flight.OriginAirport)
                .Include(ScheduledFlight => ScheduledFlight.Flight.Plane)
                .Include(ScheduledFlight => ScheduledFlight.Tickets)
                .ToListAsync();

            foreach (ScheduledFlight a in ScheduledFlights)
            {
                ScheduledFlightsWithManifest.Add(new ScheduledFlightWithManifest(a, IsFlightManager));
            }
        }

        public async Task UpdateScheduledFlightsDate()
        {
            DateTime Start = ((DateTimeOffset)StartDate).DateTime.Date;
            DateTime End = ((DateTimeOffset)EndDate).DateTime.Date;

            //Do null trick to Go to infinity/-infinity if one is not set
            using var db = new AirContext();
            ScheduledFlights = await db.ScheduledFlights
                .Include(ScheduledFlight => ScheduledFlight.Flight.DestinationAirport)
                .Include(ScheduledFlight => ScheduledFlight.Flight.OriginAirport)
                .Include(ScheduledFlight => ScheduledFlight.Flight.Plane)
                .Include(ScheduledFlight => ScheduledFlight.Tickets)
                .Where(ScheduledFlight => (ScheduledFlight.DepartureDate >= Start) && (ScheduledFlight.DepartureDate <= End))
                .ToListAsync();

            foreach (ScheduledFlight a in ScheduledFlights)
            {
                ScheduledFlightsWithManifest.Add(new ScheduledFlightWithManifest(a, IsFlightManager));
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
            savePicker.FileTypeChoices.Add(".csv", new List<string>() { "CSV (Comma delimited)" });

            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "Summary";

            StorageFile file = await savePicker.PickSaveFileAsync();

            List<string> lines = new();

            lines.Add("ScheduledFlightId,Capacity,Tickets,Cost Per Ticket");

            foreach (ScheduledFlight scheduledFlight in ScheduledFlights)
            {
                lines.Add(
                    scheduledFlight.ScheduledFlightId + "," + scheduledFlight.Flight.Plane.MaxSeats + "," + scheduledFlight.Tickets.Count + "," + scheduledFlight.Flight.GetCost()
                    );
            }

            //Grabs File Path to write using System.IO
            if (file != null)
            {
                string FilePath = file.Path;
                File.WriteAllLines(FilePath, lines);
            }

        }
    }
}
