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

        private int? _originId;

        [Required(ErrorMessage = "Please enter Id.")]
        public int? OriginId
        {
            get => _originId;
            set => SetProperty(ref _originId, value);
        }

        private int? _destinationId;

        [Required(ErrorMessage = "Please enter Id.")]
        public int? DestinationId
        {
            get => _destinationId;
            set => SetProperty(ref _destinationId, value);
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
                DestinationId = search.DestinationAirport.AirportId;
                OriginId = search.OriginAirport.AirportId;
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
                //Validate Airport1
                var airport1 = await db.Airports.SingleOrDefaultAsync(airport1 => airport1.AirportId == (int)OriginId);
                if (airport1 == null)
                {
                    Feedback = "No Origin Air Port";

                    return null;
                }

                //Validate Airport2
                var airport2 = await db.Airports.SingleOrDefaultAsync(airport2 => airport2.AirportId == (int)DestinationId);
                if (airport2 == null)
                {
                    Feedback = "No destination Air Port";

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
                    DepartureTime = Depart
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
