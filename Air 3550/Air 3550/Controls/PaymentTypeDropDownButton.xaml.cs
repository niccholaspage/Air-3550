using Database.Models;
using Microsoft.UI.Xaml.Controls;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Controls
{
    public sealed partial class PaymentTypeDropDownButton : UserControl
    {
        public PaymentMethod SelectedPaymentMethod;

        public PaymentTypeDropDownButton()
        {
            this.InitializeComponent();

            SelectedPaymentMethod = PaymentMethod.CREDIT_CARD;
            PaymentDropDownButton.Content = PaymentMethod.CREDIT_CARD.FormattedString();

            foreach (PaymentMethod paymentMethod in Enum.GetValues(typeof(PaymentMethod)))
            {
                var item = new MenuFlyoutItem
                {
                    Text = paymentMethod.FormattedString()
                };

                item.Click += (_, __) =>
                {
                    SelectedPaymentMethod = paymentMethod;

                    PaymentDropDownButton.Content = SelectedPaymentMethod.FormattedString();
                };

                PaymentFlyout.Items.Add(item);
            }
        }
    }
}
