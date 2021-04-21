using Air_3550.Models;
using Air_3550.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Air_3550.ViewModels
{
    class EditPlanesViewModel : ObservableValidator
    {
        private string _feedback;

        public string Feedback
        {
            get => _feedback;
            set => SetProperty(ref _feedback, value);
        }

        private List<Flight> _flightsA;

        public List<Flight> FlightsA
        {
            get => _flightsA;
            set => SetProperty(ref _flightsA, value);
        }

        public void UpdateFlights()
        {
            using (var db = new AirContext())
            {
                FlightsA = db.Flights
                    .Include(Flight => Flight.OriginAirport)
                    .Include(Flight => Flight.DestinationAirport)
                    .Where(f => f.IsCanceled == false)
                    .ToList();
            }
        }


    }
}

