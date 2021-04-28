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

using System;
using System.Collections.ObjectModel;
using System.Linq;
using Air_3550.Models;
using Air_3550.Repository;
using Microsoft.EntityFrameworkCore;

namespace Air_3550.ViewModels
{
    class ShowManifestViewModel
    {
        public ObservableCollection<String> TicketNames = new();

        public ScheduledFlight SFlight;

        public ShowManifestViewModel(ScheduledFlight sFlight)
        {
            //Hold the flight of interested
            SFlight = sFlight;
            //Get all names on tickets onto ObservableCollection
            getTicketNames();
        }

        public async void getTicketNames()
        {
            //Clear TicketNames
            TicketNames.Clear();

            //Grab DataBase
            using var db = new AirContext();

            // Get all customer names of current selected flight
            var customerNames = await db.Tickets.Where(Ticket => (Ticket.ScheduledFlight.ScheduledFlightId == SFlight.ScheduledFlightId) && (!Ticket.IsCanceled)).Select(Ticket => Ticket.Booking.CustomerData.Name).ToListAsync();


            //Add Ticket name to Observable collection to be displayed
            foreach (String a in customerNames)
            {
                TicketNames.Add(a);
            }

        }
    }
}
