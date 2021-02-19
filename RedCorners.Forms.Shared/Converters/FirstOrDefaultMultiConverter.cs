using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;
using Xamarin.Forms;

namespace RedCorners.Forms.Converters
{
    public class FirstOrDefaultMultiConverter : IMultiValueConverter
    {
        public static FirstOrDefaultMultiConverter Instance { get; } = new FirstOrDefaultMultiConverter();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null)
            {
                foreach (var value in values)
                {
                    if (HasValueConverter.HasValue(value))
                        return value;
                }
            }

            return parameter;
        }

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
