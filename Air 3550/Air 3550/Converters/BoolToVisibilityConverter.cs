using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;

namespace Air_3550.Converters
{
    class BoolToVisibilityConverter : IValueConverter
    {
        public Visibility OnTrue { get; set; }
        public Visibility OnFalse { get; set; }

        public BoolToVisibilityConverter()
        {
            OnTrue = Visibility.Visible;
            OnFalse = Visibility.Collapsed;
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {
            var boolean = (bool)value;

            return boolean ? OnTrue : OnFalse;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Visibility vis)
            {
                return vis == OnTrue;
            }
            else
            {
                return DependencyProperty.UnsetValue;
            }
        }
    }
}
