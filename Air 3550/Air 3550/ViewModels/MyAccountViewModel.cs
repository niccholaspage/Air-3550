using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Air_3550.ViewModels
{
    class MyAccountViewModel : ObservableValidator
    {
        private bool _viewingBookings = true;

        public bool ViewingBookings
        {
            get => _viewingBookings;
            set
            {
                SetProperty(ref _viewingBookings, value);

                OnPropertyChanged(nameof(ViewingAccountInfo));
            }
        }

        public bool ViewingAccountInfo
        {
            get => !ViewingBookings;
        }
    }
}
