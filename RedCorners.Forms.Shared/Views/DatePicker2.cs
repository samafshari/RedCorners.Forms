using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RedCorners.Forms.Views
{
    public class DatePicker2 : DatePicker
    {
        public TextAlignment TextAlignment
        {
            get => (TextAlignment)GetValue(TextAlignmentProperty);
            set => SetValue(TextAlignmentProperty, value);
        }

        public static readonly BindableProperty TextAlignmentProperty = BindableProperty.Create(
            nameof(TextAlignment), typeof(TextAlignment), typeof(DatePicker2), TextAlignment.Start, defaultBindingMode: BindingMode.TwoWay);
    }
}
