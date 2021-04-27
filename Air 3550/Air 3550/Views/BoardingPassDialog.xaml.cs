using Air_3550.Models;
using Microsoft.UI.Xaml.Controls;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BoardingPassDialog : ContentDialog
    {
        public Ticket Ticket;

        public string CustomerName;

        public BoardingPassDialog(Ticket ticket, string customerName)
        {
            this.InitializeComponent();
            Ticket = ticket;
            CustomerName = customerName;
        }
    }
}
