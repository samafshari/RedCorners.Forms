using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xamarin.Forms;

namespace RedCorners.Forms.Converters
{
    public class EqualsConverter : IValueConverter
    {
        public static EqualsConverter Instance { get; } = new EqualsConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() == parameter?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            null;
    }

    public class NotEqualsConverter : IValueConverter
    {
        public static NotEqualsConverter Instance { get; } = new NotEqualsConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() != parameter?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            null;
    }
}
