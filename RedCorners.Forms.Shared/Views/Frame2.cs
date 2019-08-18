using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RedCorners.Forms
{
    public class Frame2 : Frame
    {
        public float ShadowRadius
        {
            get => (float)GetValue(ShadowRadiusProperty);
            set => SetValue(ShadowRadiusProperty, value);
        }

        public new bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }

        public Color ShadowColor
        {
            get => (Color)GetValue(ShadowColorProperty);
            set => SetValue(ShadowColorProperty, value);
        }

        public static readonly BindableProperty ShadowRadiusProperty =
            BindableProperty.Create(
                nameof(ShadowRadius),
                typeof(float),
                typeof(Frame2),
                -1.0f,
                BindingMode.TwoWay);

        public static new readonly BindableProperty HasShadowProperty =
            BindableProperty.Create(
                nameof(HasShadow),
                typeof(bool),
                typeof(Frame2),
                false,
                BindingMode.TwoWay);

        public static readonly BindableProperty ShadowColorProperty =
            BindableProperty.Create(
                nameof(ShadowColor),
                typeof(Color),
                typeof(Frame2),
                Color.FromHex("#333333"),
                BindingMode.TwoWay);
    }
}
