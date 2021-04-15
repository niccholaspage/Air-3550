using Air_3550.Models;
using Air_3550.Repository;
using Database.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Air_3550.ViewModels
{
    class EditScheduleViewModel : ObservableValidator
    {
        private TimeSpan _depart;

        public TimeSpan Depart
        {
            get => _depart;
            set => SetProperty(ref _depart, value);
        }

        private int _originId;

        public int OriginId
        {
            get => _originId;
            set => SetProperty(ref _originId, value);
        }

        private int _destinationId;

        public int DestinationId
        {
            get => _destinationId;
            set => SetProperty(ref _destinationId, value);
        }

        private int _number;

        public int Number
        {
            get => _number;
            set => SetProperty(ref _number, value);
        }

        private string _feedback;

        public string Feedback
        {
            get => _feedback;
            set => SetProperty(ref _feedback, value);
        }

        public async Task<bool> CreateFlight()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                Feedback = "Has Errors";

                return false;
            }

            using (var db = new AirContext())
            {
                Random random = new();

                int generatedId;

                while (true)
                {
                    // Generates an ID between 100000 (because user IDs cannot start with zero) and 1,000,000 exclusive
                    generatedId = random.Next(100_000, 1_000_000);
                    if (await db.Flights.SingleOrDefaultAsync(Flight => Flight.FlightId == generatedId) == null)
                    {
                        break;
                    }
                }

                var airport1 = await db.Airports.SingleOrDefaultAsync(airport1 => airport1.AirportId == OriginId);
                if (airport1 == null)
                {
                    Feedback = "No Origin Air Port";

                    return false;
                }

                var airport2 = await db.Airports.SingleOrDefaultAsync(airport2 => airport2.AirportId == DestinationId);
                if (airport2 == null)
                {
                    Feedback = "No destination Air Port";

                    return false;
                }

                var departureTime = new DateTime(1, 1, 1, Depart.Hours, Depart.Minutes, Depart.Seconds);

                var flight = new Flight
                {
                    FlightId = generatedId,
                    Number = Number,
                    OriginAirport = airport1,
                    DestinationAirport = airport2
                };

                await db.AddAsync(flight);

                db.SaveChanges();
                Feedback = "Sucess";
                return true;
            }
        }
    }
}
