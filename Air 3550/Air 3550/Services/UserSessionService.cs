using Air_3550.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Air_3550.Services
{
    class UserSessionService : ObservableObject
    {
        private int? _userId;

        public int? UserId
        {
            get => _userId;
            private set
            {
                SetProperty(ref _userId, value);

                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }

        private int? _customerDataId;

        public int? CustomerDataId
        {
            get => _customerDataId;
            private set => SetProperty(ref _customerDataId, value);
        }

        private Role? _role;

        public Role? Role
        {
            get => _role;
            private set => SetProperty(ref _role, value);
        }

        public void Login(User user, int? customerDataId)
        {
            UserId = user.UserId;

            Role = user.Role;

            CustomerDataId = customerDataId;
        }

        public bool IsLoggedIn
        {
            get => UserId != null;
        }

        public void Logout()
        {
            UserId = null;
            CustomerDataId = null;
            Role = null;
        }
    }
}
