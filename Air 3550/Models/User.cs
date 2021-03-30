namespace Air_3550.Models
{
    class User
    {
        public int UserId { get; set; }
        public string LoginId { get; set; }
        public string PasswordHash { get; set; } // TODO: Determine if this should be a string or not
        public Role Role { get; set; }
    }
}
