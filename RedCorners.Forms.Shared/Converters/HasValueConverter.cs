using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RedCorners.Forms.Converters
{
    public class HasValueConverter : IValueConverter
    {
        public static HasValueConverter Instance => new HasValueConverter();

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (value is string text) return text?.HasValue() ?? false;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
