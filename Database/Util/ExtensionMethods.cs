using System;

namespace Database.Util
{
    public static class ExtensionMethods
    {
        public static string FormatAsMoney(this decimal amount)
        {
            return string.Format("{0:C}", amount);
        }

        public static string FormatNicely(this DateTime dateTime) => dateTime.ToString("ddd, MM/dd");

        public static string FormatAsTimeNicely(this TimeSpan timeSpan)
        {
            return DateTime.Today.Add(timeSpan).ToString("h:mm tt");
        }

        public static string FormatAsDurationNicely(this TimeSpan timeSpan)
        {
            string result = "";

            if (timeSpan.Days > 0)
            {
                result += timeSpan.Days + "d ";
            }

            if (timeSpan.Hours > 0)
            {
                result += timeSpan.Hours + "h ";
            }

            if (timeSpan.Minutes > 0)
            {
                result += timeSpan.Minutes + "m";
            }

            return result.Trim();
        }
    }
}
