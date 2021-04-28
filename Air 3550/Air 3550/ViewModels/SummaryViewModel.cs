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
using Database.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace Air_3550.ViewModels
{
    public record ScheduledFlightWithManifest(ScheduledFlight ScheduledFlight, bool IsFlightManager)
    {
        public decimal Income => ScheduledFlight.Tickets.Sum(ticket => ticket.IsCanceled ? 0.0m : ticket.Price);

        public string FormattedIncome => Income.FormatAsMoney();
    }

    class SummaryViewModel : ObservableValidator
    {
        private readonly UserSessionService userSessionService;

        public ObservableCollection<ScheduledFlightWithManifest> ScheduledFlightsWithManifest = new();

        private bool _isFlightManager;
        public string DateTitle;
        public bool IsNotFlightManager => !IsFlightManager;

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

        private string _formattedTotalIncome;

        public string FormattedTotalIncome
        {
            get => _formattedTotalIncome;
            set
            {
                SetProperty(ref _formattedTotalIncome, value);
            }
        }

        public SummaryViewModel()
        {
            userSessionService = App.Current.Services.GetService<UserSessionService>();

            IsFlightManager = userSessionService.Role == Role.FLIGHT_MANAGER;

            if (IsFlightManager)
            {
                DateTitle = "Date";
            }
            else
            {
                DateTitle = "Start Date";
            }
        }

        public async Task UpdateScheduledFlightsDate()
        {
            ScheduledFlightsWithManifest.Clear();

            DateTime start = DateTime.MinValue;
            DateTime end = DateTime.MaxValue;

            if (StartDate == null && EndDate == null)
            {

            }
            else if (IsFlightManager && StartDate != null)
            {
                start = ((DateTimeOffset)StartDate).DateTime.Date;
                end = start;
            }
            else if (StartDate == null)
            {
                start = DateTime.MinValue;
                end = ((DateTimeOffset)EndDate).DateTime.Date;
            }
            else if (EndDate == null)
            {
                start = ((DateTimeOffset)StartDate).DateTime.Date;
                end = DateTime.MaxValue;
            }
            else
            {
                start = ((DateTimeOffset)StartDate).DateTime.Date;
                end = ((DateTimeOffset)EndDate).DateTime.Date;
            }

            using var db = new AirContext();

            ScheduledFlights = await db.ScheduledFlights
                .Include(scheduledFlight => scheduledFlight.Flight.DestinationAirport)
                .Include(scheduledFlight => scheduledFlight.Flight.OriginAirport)
                .Include(scheduledFlight => scheduledFlight.Flight.Plane)
                .Include(scheduledFlight => scheduledFlight.Tickets)
                .OrderBy(scheduledFlight => scheduledFlight.DepartureDate)
                .Where(scheduledFlight => scheduledFlight.DepartureDate >= start && scheduledFlight.DepartureDate <= end)
                .ToListAsync();

            decimal totalIncome = 0;

            foreach (ScheduledFlight scheduledFlight in ScheduledFlights)
            {
                if (!scheduledFlight.Tickets.Where(ticket => !ticket.IsCanceled).Any())
                {
                    continue;
                }

                var scheduledFlightWithManifest = new ScheduledFlightWithManifest(scheduledFlight, IsFlightManager);

                totalIncome += scheduledFlightWithManifest.Income;

                ScheduledFlightsWithManifest.Add(scheduledFlightWithManifest);
            }

            FormattedTotalIncome = totalIncome.FormatAsMoney();
        }

        public async Task SaveSummary()
        {
            FileSavePicker savePicker = new();

            MainWindow.FixPicker(savePicker);

            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("CSV (Comma delimited)", new List<string>() { ".csv" });

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
