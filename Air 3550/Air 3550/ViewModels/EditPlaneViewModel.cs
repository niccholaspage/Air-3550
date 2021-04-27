using Air_3550.Models;
using Air_3550.Repository;
using Air_3550.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Air_3550.ViewModels
{
    class EditPlaneViewModel : ObservableValidator
    {
        private readonly int FlightId;

        public EditPlaneViewModel(Flight flight)
        {
            FlightId = flight.FlightId;
            PlaneId = flight.PlaneId;
        }

        private string _feedback;

        public string Feedback
        {
            get => _feedback;
            set => SetProperty(ref _feedback, value);
        }

        private int? _planeId;

        [Required(ErrorMessage = "Please enter a valid plane.")]
        public int? PlaneId
        {
            get => _planeId;
            set => SetProperty(ref _planeId, value);
        }

        public async Task<Flight> EditFlight()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                Feedback = this.GetFirstError();

                return null;
            }

            using (var db = new AirContext())
            {
                //Update Plane on selected Flight
                var flight = await db.Flights.Include(Flight => Flight.Plane).SingleOrDefaultAsync(flight => flight.FlightId == FlightId);
                flight.PlaneId = (int)PlaneId;
                await db.SaveChangesAsync();
                return flight;
            }
        }
    }
}

