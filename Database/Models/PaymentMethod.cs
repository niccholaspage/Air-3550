namespace Database.Models
{
    public enum PaymentMethod
    {
        CREDIT_CARD,
        ACCOUNT_BALANCE,
        POINTS
    }

    public static class Extensions
    {
        public static string FormattedString(this PaymentMethod paymentMethod)
        {
            return paymentMethod switch
            {
                PaymentMethod.CREDIT_CARD => "Credit Card",
                PaymentMethod.ACCOUNT_BALANCE => "Account Balance",
                PaymentMethod.POINTS => "Points",
                _ => "Invalid Payment Method"
            };
        }
    }
}
