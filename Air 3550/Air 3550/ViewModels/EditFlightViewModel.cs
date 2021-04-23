using Air_3550.Models;
using Air_3550.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Air_3550.ViewModels
{
    class EditFlightViewModel : ObservableValidator
    {
        private TimeSpan _depart;

        public TimeSpan Depart
        {
            get => _depart;
            set => SetProperty(ref _depart, value);
        }

        private string _originCity;

        [Required(ErrorMessage = "Please enter Origin city.")]
        public string OriginCity
        {
            get => _originCity;
            set => SetProperty(ref _originCity, value);
        }

        private string _destinationCity;

        [Required(ErrorMessage = "Please enter Id.")]
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

        public void GrabValues(Flight editting)
        {
            using (var db = new AirContext())
            {
                var search = db.Flights
                    .Include(Flight => Flight.OriginAirport)
                    .Include(Flight => Flight.DestinationAirport)
                    .Where(f => f.IsCanceled == false).Single(search => search.FlightId == editting.FlightId);
                Depart = search.DepartureTime;
                Number = search.Number;
                DestinationCity = search.DestinationAirport.City + ", " + search.DestinationAirport.State + " (" + search.DestinationAirport.Code + ")";
                OriginCity = search.OriginAirport.City + ", " + search.OriginAirport.State + " (" + search.OriginAirport.Code + ")";
            }

        }

        public async Task<Flight> EditFlight(Flight editting)
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
                    Feedback = "No Origin Airport";

                    return null;
                }

                //Find destinition airport
                var airport2 = await db.Airports.SingleOrDefaultAsync(airport2 => DestinationCity.Contains(airport2.City));
                if (airport2 == null)
                {
                    Feedback = "No destination Airport";

                    return null;
                }

                //Remove Previous Flight
                var search = await db.Flights.SingleOrDefaultAsync(search => search.FlightId == editting.FlightId);
                search.IsCanceled = true;
                await db.SaveChangesAsync();

                //Add New Flight
                var flight = new Flight
                {
                    Number = (int)Number,
                    OriginAirport = airport1,
                    DestinationAirport = airport2,
                    DepartureTime = Depart,
                    PlaneId = editting.PlaneId
                };
                await db.AddAsync(flight);

                //Save Changes
                await db.SaveChangesAsync();
                Feedback = "Sucess";
                return flight;
            }
        }

    }

}
