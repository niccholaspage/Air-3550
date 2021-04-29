// FlightSearchViewModel.cs - Air 3550 Project
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
 * This view model contains the logic for
 * searching for flight paths, including
 * calling on the flight search algorithm
 * to retrieve valid and optimal flight
 * paths.
 */

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Air_3550.Models;
using Air_3550.Util;
using Database.Util;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Air_3550.ViewModels
{
    class FlightSearchViewModel : ObservableObject
    {
        public Airport OriginAirport;

        public Airport DestinationAirport;

        public DateTime Date;

        public string FormattedDate => Date.ToString("ddd, MMMM dd");

        public ObservableCollection<FlightPath> Paths = new();

        private int _selectedPathIndex = -1;

        public int SelectedPathIndex
        {
            get => _selectedPathIndex;
            set
            {
                SetProperty(ref _selectedPathIndex, value);

                OnPropertyChanged(nameof(CanContinue));
            }
        }

        public bool HasPaths => Paths.Count > 0;

        public bool CanContinue => SelectedPathIndex != -1;

        public async Task SearchForFlights(Airport originAirport, Airport destinationAirport, DateTime date)
        {
            OriginAirport = originAirport;
            DestinationAirport = destinationAirport;
            Date = date;

            var possiblePaths = await FlightSearchAlgorithm.FindFlightPaths(originAirport.AirportId, destinationAirport.AirportId);

            var optimizedPaths = await FlightSearchAlgorithm.GetValidAndOptimizedFlightPaths(possiblePaths, date);

            foreach (var path in optimizedPaths)
            {
                Paths.Add(path);
            }

            OnPropertyChanged(nameof(HasPaths));
        }
    }
}
