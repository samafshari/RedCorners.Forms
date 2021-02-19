using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;
using Xamarin.Forms;

namespace RedCorners.Forms.Converters
{
    public class OrMultiConverter : IMultiValueConverter
    {
        public static OrMultiConverter Instance { get; } = new OrMultiConverter();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || !targetType.IsAssignableFrom(typeof(bool)))
            {
                return false;
            }

            foreach (var value in values)
            {
                if (value is bool b && b)
                    return true;
                else if (HasValueConverter.HasValue(value))
                    return true;
            }
            return false;
        }

        // Ignore this
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (!(value is bool b) || targetTypes.Any(t => !t.IsAssignableFrom(typeof(bool))))
            {
                return null;
            }

            if (b)
            {
                return targetTypes.Select(t => (object)true).ToArray();
            }
            else
            {
                return null;
            }
        }
    }
}
