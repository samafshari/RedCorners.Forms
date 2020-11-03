using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xamarin.Forms;

namespace RedCorners.Forms.Converters
{
    public class UpperConverter : IValueConverter
    {
        public static UpperConverter Instance { get; } = new UpperConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString()?.ToUpper();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            Convert(value, targetType, parameter, culture);
    }
    
    public class LowerConverter : IValueConverter
    {
        public static LowerConverter Instance { get; } = new LowerConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString()?.ToLower();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            Convert(value, targetType, parameter, culture);
    }
    
    public class TitleCaseConverter : IValueConverter
    {
        public static TitleCaseConverter Instance { get; } = new TitleCaseConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = value?.ToString();
            if (s == null) return s;
            return culture.TextInfo.ToTitleCase(s);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            Convert(value, targetType, parameter, culture);
    }
}
