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
                //Get Max sets of plane and see if the any ScheduledFlights have more passangers
                var MaxSeats = await db.Planes.Where(plane => plane.PlaneId == (int)PlaneId).Select(plane => plane.MaxSeats).SingleAsync();
                var Passangers = await db.ScheduledFlights.Include(scheduledFlight => scheduledFlight.Tickets).Where(scheduledFlight => scheduledFlight.FlightId == FlightId).ToListAsync();
                var OverCap = Passangers.Where(scheduledFlight => scheduledFlight.FilledSeats > MaxSeats).ToList();
                if(OverCap.Count > 0)
                {
                    Feedback = "Select a bigger Plane";
                    return null;
                }
                //Updates Flight if valid
                var flight = await db.Flights.Include(Flight => Flight.Plane).SingleOrDefaultAsync(flight => flight.FlightId == FlightId);
                flight.PlaneId = (int)PlaneId;
                await db.SaveChangesAsync();
                return flight;
            }
        }
    }
}

