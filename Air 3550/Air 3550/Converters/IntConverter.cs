using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int number;
            bool parsed = int.TryParse(value.ToString(), out number);
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
