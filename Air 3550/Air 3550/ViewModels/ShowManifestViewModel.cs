// ShowManifestViewModel.cs - Air 3550 Project
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
 * This view model simply holds the logic
 * to fetch customer names for a certain
 * scheduled flight for the retrieval
 * of a flight manifest. Only non-canceled
 * tickets are taken into consideratino,
 * and it keeps track of all ticket
 * names for a scheduled flight.
 */

using System.Collections.ObjectModel;
using System.Linq;
using Air_3550.Models;
using Air_3550.Repository;
using Microsoft.EntityFrameworkCore;

namespace Air_3550.ViewModels
{
    class ShowManifestViewModel
    {
        public ObservableCollection<string> TicketNames = new();

        public ScheduledFlight ScheduledFlight;

        public ShowManifestViewModel(ScheduledFlight scheduledFlight)
        {
            //Hold the scheduled flight we are taking a look at
            ScheduledFlight = scheduledFlight;

            // Get all names on the tickets
            GetTicketNames();
        }

        public async void GetTicketNames()
        {
            // Clear all the ticket names
            TicketNames.Clear();

            // Get a new database context
            using var db = new AirContext();

            // Get all customer names of non-canceled tickets from currently selected scheduled flight
            var customerNames = await db.Tickets.Where(Ticket => Ticket.ScheduledFlight.ScheduledFlightId == ScheduledFlight.ScheduledFlightId && !Ticket.IsCanceled).Select(Ticket => Ticket.Booking.CustomerData.Name).ToListAsync();


            // Add ticket name to TicketNames collection for future use
            foreach (string customerName in customerNames)
            {
                TicketNames.Add(customerName);
            }

        }
    }
}
