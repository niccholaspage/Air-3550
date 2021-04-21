using Air_3550.Models;
using Air_3550.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Air_3550.ViewModels
{
    class AddFlightViewModel : ObservableValidator
    {
        private TimeSpan _depart;

        public TimeSpan Depart
        {
            get => _depart;
            set => SetProperty(ref _depart, value);
        }

        private string _originCity;

        //[Required(ErrorMessage = "Please enter City.")]
        public string OriginCity
        {
            get => _originCity;
            set => SetProperty(ref _originCity, value);
        }

        private string _destinationCity;

        //[Required(ErrorMessage = "Please enter City.")]
        public string DestinationCity
        {
            get => _destinationCity;
            set => SetProperty(ref _destinationCity, value);
        }

        private int? _number;

        [Required(ErrorMessage = "Please enter Number.")]
        public int? Number
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

        public async Task<Flight> CreateFlight()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                Feedback = "Has Errors";

                return null;
            }

            using (var db = new AirContext())
            {
                //Find origin Airport
                var airport1 = await db.Airports.SingleOrDefaultAsync(airport1 => OriginCity.Contains(airport1.City));
                if (airport1 == null)
                {
                    Feedback = "Entered City: " + OriginCity;

                    return null;
                }

                //Find destinition airport
                var airport2 = await db.Airports.SingleOrDefaultAsync(airport2 => DestinationCity.Contains(airport2.City));
                if (airport2 == null)
                {
                    Feedback = "No destination Air Port";

                    return null;
                }

                var flight = new Flight
                {
                    Number = (int)Number,
                    OriginAirport = airport1,
                    DestinationAirport = airport2,
                    DepartureTime = Depart
                };

                await db.AddAsync(flight);

                await db.SaveChangesAsync();
                Feedback = "Sucess";
                return flight;
            }
        }
    }
}
