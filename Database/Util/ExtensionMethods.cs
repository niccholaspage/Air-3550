namespace Database.Util
{
    public static class ExtensionMethods
    {
        public static string FormatAsMoney(this decimal amount)
        {
            return string.Format("{0:C}", amount);
        }
    }
}
