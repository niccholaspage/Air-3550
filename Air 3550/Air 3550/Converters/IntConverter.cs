using Microsoft.UI.Xaml.Data;
using System;

namespace Air_3550.Converters
{
    class IntConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return null;
            }

            return value.ToString();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            bool parsed = int.TryParse(value.ToString(), out int number);
            if (parsed)
            {
                return number;
            }
            else
            {
                return null;
            }
        }
    }
}
