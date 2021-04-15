using Air_3550.Models;

namespace Air_3550.Services
{
    class UserSessionService
    {
        public int? UserId { get; private set; }
        public Role? Role { get; private set; }

        public void Login(User user)
        {
            UserId = user.UserId;

            Role = user.Role;
        }

        public bool IsLoggedIn()
        {
            return UserId != null;
        }

        public void Logout()
        {
            UserId = null;
            Role = null;
        }
    }
}
