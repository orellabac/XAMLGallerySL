using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AppUIBasics
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public Visibility NullValue { get; set; } = Visibility.Collapsed;
        public Visibility NonNullValue { get; set; } = Visibility.Visible;


        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            return (value == null) ? NullValue : NonNullValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            throw new NotImplementedException();
        }

    }
}
