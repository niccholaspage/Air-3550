using Database.Models;
using Microsoft.UI.Xaml.Controls;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Controls
{
    public sealed partial class PaymentTypeDropDownButton : UserControl
    {
        public PaymentTypeDropDownButton()
        {
            this.InitializeComponent();

            foreach (PaymentMethod paymentMethod in Enum.GetValues(typeof(PaymentMethod)))
            {
                PaymentComboBox.Items.Add(paymentMethod.FormattedString());
            }

            PaymentComboBox.SelectedIndex = 0;
        }

        public PaymentMethod SelectedPaymentMethod
        {
            get
            {
                return (PaymentMethod)PaymentComboBox.SelectedIndex;
            }
        }
    }
}
