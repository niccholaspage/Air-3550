using System.Collections.Generic;

namespace Air_3550.Models
{
    class CustomerData
    {
        public int CustomerDataId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; } // determine format to put it into then save it here
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; } // Enumeration potentially? This will be a dropdown in UI.
        public string CreditCardNumber { get; set; } // Don't ever store it like this in a real app...
        public int AccountBalance { get; set; } // Store account balance in cents, make it look like dollars in the UI.
        public int RewardPointsBalance { get; set; }
        public int RewardPointsUsed { get; set; }

        public List<int> BookingIds { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
