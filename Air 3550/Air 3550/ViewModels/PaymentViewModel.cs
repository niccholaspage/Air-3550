using Air_3550.Util;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air_3550.ViewModels
{
    class PaymentViewModel : ObservableObject
    {
        private FlightPath _departingFlightPath;
        private FlightPath _returnFlightPath;

        public FlightPath DepartingFlightPath
        {
            get => _departingFlightPath;
            set => SetProperty(ref _departingFlightPath, value);
        }
        public FlightPath ReturnFlightPath
        {
            get => _returnFlightPath;
            set => SetProperty(ref _returnFlightPath, value);
        }
    }
}
