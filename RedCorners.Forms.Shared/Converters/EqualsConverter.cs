using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;
using Xamarin.Forms;

namespace RedCorners.Forms.Converters
{
    public class CountIsConverter : IValueConverter
    {
        public static CountIsConverter Instance { get; } = new CountIsConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int? p = null;
            if (int.TryParse(parameter?.ToString(), out var i))
                p = i;
            if (p == null) return value == null;
            if (value is IList e)
                return e?.Count == p;
            if (value is ICollection c)
                return c?.Count == p;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            null;
    }

    public class CountIsNotConverter : IValueConverter
    {
        public static CountIsNotConverter Instance { get; } = new CountIsNotConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int? p = null;
            if (int.TryParse(parameter?.ToString(), out var i))
                p = i;
            if (p == null) return value != null;
            if (value is IList e)
                return e?.Count != p;
            if (value is ICollection c)
                return c?.Count != p;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            null;
    }
}
