using System;

namespace Air_3550.Util
{
    class FlightPathWithDate
    {
        private readonly FlightPath _flightPath;
        private readonly DateTime _date;

        public FlightPath FlightPath => _flightPath;
        public DateTime Date => _date;

        public string FormattedDate => _date.ToString("ddd, MM/dd");

        public FlightPathWithDate(FlightPath flightPath, DateTime date)
        {
            _flightPath = flightPath;
            _date = date;
        }
    }
}
