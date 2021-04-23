using Air_3550.Models;
using Air_3550.Repository;
using Air_3550.Util;
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

        private int? _originId;

        [Required(ErrorMessage = "Please enter a valid origin airport.")]
        public int? OriginId
        {
            get => _originId;
            set => SetProperty(ref _originId, value);
        }

        private int? _destinationId;

        [Required(ErrorMessage = "Please enter a valid destination airport.")]
        public int? DestinationId
        {
            get => _destinationId;
            set => SetProperty(ref _destinationId, value);
        }

        private int? _number;

        [Required(ErrorMessage = "Please enter number.")]
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
                Feedback = this.GetFirstError();

                return null;
            }

            using (var db = new AirContext())
            {
                var plane1 = await db.Planes.SingleOrDefaultAsync(plane1 => plane1.Model == "Boeing 777");

                var flightN = new Flight
                {
                    Number = (int)Number,
                    OriginAirportId = (int)OriginId,
                    DestinationAirportId = (int)DestinationId,
                    DepartureTime = Depart,
                    Plane = plane1,
                    PlaneId = 3
                };

                await db.Flights.AddAsync(flightN);

                await db.SaveChangesAsync();
                Feedback = "Success";
                return flightN;
            }
        }
    }
}
