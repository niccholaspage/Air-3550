using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Air_3550.Models
{
    public class CustomerData
    {
        public int CustomerDataId { get; set; }

        public User User { get; set; }

        [Required]
        public string Name { get; set; }

        public int Age { get; set; }

        [Required]
        public string PhoneNumber { get; set; } // determine format to put it into then save it here

        [Required]
        public string Address { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; } // Enumeration potentially? This will be a dropdown in UI.

        [Required]
        public string CreditCardNumber { get; set; } // Don't ever store it like this in a real app...

        public int AccountBalance { get; set; } // Store account balance in cents, make it look like dollars in the UI.
        public int RewardPointsBalance { get; set; }
        public int RewardPointsUsed { get; set; }

        public List<Booking> Bookings { get; set; }
    }
}
