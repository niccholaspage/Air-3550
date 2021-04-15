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
                var airport1 = await db.Airports.SingleOrDefaultAsync(airport1 => airport1.AirportId == (int)OriginId);
                if (airport1 == null)
                {
                    Feedback = "No Origin Air Port";

                    return false;
                }

                var airport2 = await db.Airports.SingleOrDefaultAsync(airport2 => airport2.AirportId == (int)DestinationId);
                if (airport2 == null)
                {
                    Feedback = "No destination Air Port";

                    return false;
                }

                var departureTime = new DateTime(1, 1, 1, Depart.Hours, Depart.Minutes, Depart.Seconds);

                var flight = new Flight
                {
                    Number = (int)Number,
                    OriginAirport = airport1,
                    DestinationAirport = airport2
                };

                await db.AddAsync(flight);

                await db.SaveChangesAsync();
                Feedback = "Sucess";
                return true;
            }
        }
    }
}
