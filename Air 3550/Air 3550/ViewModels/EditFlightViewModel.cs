// EditFlightViewModel.cs - Air 3550 Project
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
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Air_3550.Models;
using Air_3550.Repository;
using Air_3550.Util;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Air_3550.ViewModels
{
    class EditFlightViewModel : ObservableValidator
    {
        private readonly int FlightId;

        public EditFlightViewModel(Flight flight)
        {
            FlightId = flight.FlightId;

            Depart = flight.DepartureTime;
            OriginId = flight.OriginAirportId;
            DestinationId = flight.DestinationAirportId;
            PlaneId = flight.PlaneId;
        }
        private TimeSpan _depart;

        public TimeSpan Depart
        {
            get => _depart;
            set => SetProperty(ref _depart, value);
        }

        private int? _originId;

        [Required(ErrorMessage = "Please enter a valid origin airport.")]
        public int? OriginId
        {
            get => _originId;
            set => SetProperty(ref _originId, value);
        }

        private int? _destinationId;

        [Required(ErrorMessage = "Please enter a valid destination airport.")]
        [NotEqualTo(nameof(OriginId), ErrorMessage = "Departure and arrival airports cannot match.")]
        public int? DestinationId
        {
            get => _destinationId;
            set => SetProperty(ref _destinationId, value);
        }

        private int? _planeId;

        [Required(ErrorMessage = "Please enter a valid plane.")]
        public int? PlaneId
        {
            get => _planeId;
            set => SetProperty(ref _planeId, value);
        }

        private string _feedback;

        public string Feedback
        {
            get => _feedback;
            set => SetProperty(ref _feedback, value);
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
                // Cancel previous flight
                var search = await db.Flights.FindAsync(FlightId);
                search.IsCanceled = true;

                // Add New Flight
                var flight = new Flight
                {
                    OriginAirportId = (int)OriginId,
                    DestinationAirportId = (int)DestinationId,
                    DepartureTime = Depart,
                    PlaneId = (int)PlaneId
                };

                await db.AddAsync(flight);
                await db.SaveChangesAsync();

                // Set the flight number to be the flight ID,
                // since we don't have any system for rolling
                // over flight numbers.
                flight.Number = flight.FlightId;

                await db.SaveChangesAsync();

                Feedback = "Sucess";
                return flight;
            }
        }

    }

}
