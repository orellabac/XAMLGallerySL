using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

public class Utils
{
    public static Image GetIcon(string name)
    {
        Image img = new Image();
        name = name.Replace("ms-appx://", "/XamlControlsGallerySL;component");
        img.Source = new BitmapImage(new Uri(name, UriKind.Relative));
        return img;
    }


}

namespace AppUIBasics
{
    public class InvertBooleanConverter : IValueConverter
    {

        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (targetType == typeof(Boolean))
            {
                var val = (Boolean)value;
                return val == true ? false: true;
            }
            throw new InvalidOperationException("Converter can only convert to value of type Visibility.");
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new Exception("Invalid call - one way only");
        }
    }

    public class BooleanConverter : IValueConverter
    {

        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (targetType == typeof(Boolean))
            {
                if (value is bool?)
                {
                    var val = ((bool?)value);
                    return val.HasValue ? val.Value : false;
                }
            }
            throw new InvalidOperationException("Unsupported type");
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new Exception("Invalid call - one way only");
        }
    }
}