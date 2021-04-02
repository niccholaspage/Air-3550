using System.ComponentModel.DataAnnotations;

namespace Air_3550.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string LoginId { get; set; }

        [Required]
        public string PasswordHash { get; set; } // TODO: Determine if this should be a string or not

        [Required]
        public Role Role { get; set; }
    }
}
