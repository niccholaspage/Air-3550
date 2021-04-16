using Air_3550.Views;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;

namespace Air_3550.ViewModels
{
    class MyAccountViewModel : ObservableObject
    {
        private bool _viewingBookings = true;

        public bool ViewingBookings
        {
            get => _viewingBookings;
            set
            {
                SetProperty(ref _viewingBookings, value);

                OnPropertyChanged(nameof(ViewingAccountInfo));
                OnPropertyChanged(nameof(DisplayedPage));
            }
        }

        public bool ViewingAccountInfo
        {
            get => !ViewingBookings;
        }


        public Page DisplayedPage
        {
            get => ViewingBookings ? new LoginPage() : new EditAccountInfoSubPage();
        }
    }
}
