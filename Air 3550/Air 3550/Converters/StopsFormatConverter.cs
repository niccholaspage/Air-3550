using Microsoft.UI.Xaml.Data;
using System;

namespace Air_3550.Converters
{
    class StopsFormatConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return null;
            }

            int stops = (int)value;

            return stops switch
            {
                0 => "Nonstop",
                1 => stops + " stop",
                _ => stops + " stops"
            };
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new ArgumentException("We don't support converting back!");
        }
    }
}
