using System;

namespace Database.Util
{
    class Pricing
    {
        //TODO: Determine if this is a good resting place
        public static decimal GetDiscountPercentage(DateTime departureTimestamp, DateTime arrivalTimestamp)
        {
            if ((departureTimestamp.Hour > 0 && departureTimestamp.Hour < 5) || (arrivalTimestamp.Hour > 0 && arrivalTimestamp.Hour < 5))
            {
                return 0.20m;
            }
            else if (departureTimestamp.Hour < 8 || arrivalTimestamp.Hour > 19)
            {
                return 0.10m;
            }
            else
            {
                return 0.00m;
            }
        }

    }
}
