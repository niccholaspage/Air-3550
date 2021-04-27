using System;

namespace Database.Util
{
    public class FlightPathWithDate
    {
        private readonly FlightPath _flightPath;
        private readonly DateTime _date;

        public FlightPath FlightPath => _flightPath;
        public DateTime Date => _date;

        public string FormattedDate => _date.FormatNicely();

        public FlightPathWithDate(FlightPath flightPath, DateTime date)
        {
            _flightPath = flightPath;
            _date = date;
        }

        public DateTime FirstDepartureFlightTimestamp => _date + _flightPath.FirstFlightDepartureTime;

        public bool HasFirstFlightDeparted()
        {
            return DateTime.Now >= FirstDepartureFlightTimestamp;
        }
    }
}
