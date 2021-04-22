using Air_3550.Models;
using Air_3550.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Air_3550.ViewModels
{
    class EditPlaneViewModel : ObservableValidator
    {
        private string _feedback;

        public string Feedback
        {
            get => _feedback;
            set => SetProperty(ref _feedback, value);
        }

        private string _planeString;

        public string PlaneString
        {
            get => _planeString;
            set => SetProperty(ref _planeString, value);
        }

        public void GrabValues(Flight editting)
        {
            using (var db = new AirContext())
            {
                var search = db.Flights
                    .Include(Flight => Flight.Plane)
                    .Where(f => f.IsCanceled == false).Single(search => search.FlightId == editting.FlightId);
                if (search.Plane != null) PlaneString = search.Plane.Model;
                else PlaneString = "";
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
                //See if plane is valid
                var plane1 = await db.Planes.SingleOrDefaultAsync(plane1 => plane1.Model == PlaneString);
                if (plane1 == null)
                {
                    Feedback = "No Such airplane";

                    return null;
                }

                //Update Plane on selected Flight
                var search = await db.Flights.Include(Flight => Flight.Plane).SingleOrDefaultAsync(search => search.FlightId == editting.FlightId);
                search.Plane = plane1;
                await db.SaveChangesAsync();

                //Save Changes
                await db.SaveChangesAsync();
                Feedback = "Sucess";
                return search;
            }
        }


    }
}

