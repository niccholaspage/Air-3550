using Air_3550.Repository;
using Air_3550.Util;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Threading.Tasks;
using Air_3550.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PaymentPage : Page
    {
        

        public class Params
        {
            public FlightPath Purchasing;

            public Params(FlightPath purchasing)
            {
                this.Purchasing = purchasing;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var param = e.Parameter as Params;
        }

        public PaymentPage()
        {
            this.InitializeComponent();
        }

        readonly SummaryViewModel ViewModel = new();
    }
}
