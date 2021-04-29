// EditPlaneViewModel.cs - Air 3550 Project
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
 * This view model allows a marketing manager
 * to edit just the plane of a given flight.
 * This edit view will edit the flight directly.
 */

using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Air_3550.Models;
using Air_3550.Repository;
using Air_3550.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Air_3550.ViewModels
{
    class EditPlaneViewModel : ObservableValidator
    {
        private readonly int FlightId;

        public EditPlaneViewModel(Flight flight)
        {
            FlightId = flight.FlightId;
            PlaneId = flight.PlaneId;
        }

        private string _feedback;

        public string Feedback
        {
            get => _feedback;
            set => SetProperty(ref _feedback, value);
        }

        private int? _planeId;

        [Required(ErrorMessage = "Please enter a valid plane.")]
        public int? PlaneId
        {
            get => _planeId;
            set => SetProperty(ref _planeId, value);
        }

        public async Task<Flight> EditFlight()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                Feedback = this.GetFirstError();

                return null;
            }

            using (var db = new AirContext())
            {
                // Get the max sets of the chosen plane and
                // see if any scheduled flights have more
                // passengers than the plane could fit.
                var maxSeats = await db.Planes.Where(plane => plane.PlaneId == PlaneId).Select(plane => plane.MaxSeats).SingleAsync();

                var passengers = await db.ScheduledFlights
                    .Include(scheduledFlight => scheduledFlight.Tickets)
                    .Where(scheduledFlight => scheduledFlight.FlightId == FlightId)
                    .ToListAsync();

                if (passengers.Where(scheduledFlight => scheduledFlight.FilledSeats > maxSeats).Any())
                {
                    Feedback = "Please choose a bigger plane.";

                    return null;
                }

                // Now we go ahead and grab the flight from the DB
                // and update it, finally saving our changes.
                var flight = await db
                    .Flights.Include(Flight => Flight.Plane)
                    .SingleOrDefaultAsync(flight => flight.FlightId == FlightId);

                flight.PlaneId = (int)PlaneId;

                await db.SaveChangesAsync();

                return flight;
            }
        }
    }
}

