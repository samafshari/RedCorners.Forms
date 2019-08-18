using System;
using Xamarin.Forms;

namespace RedCorners.Forms
{

    public class Button2 : Button
    {
        public new event EventHandler Pressed;
        public new event EventHandler Released;

        public TextAlignment TextAlignment
        {
            get => (TextAlignment)GetValue(TextAlignmentProperty);
            set => SetValue(TextAlignmentProperty, value);
        }

        public static readonly BindableProperty TextAlignmentProperty =
            BindableProperty.Create(nameof(TextAlignment),
            typeof(TextAlignment),
            typeof(Button2),
            TextAlignment.Center,
            BindingMode.TwoWay);

        public virtual void OnPressed()
        {
            Pressed?.Invoke(this, EventArgs.Empty);
        }

        public virtual void OnReleased()
        {
            Released?.Invoke(this, EventArgs.Empty);
        }

#if __IOS__
        public new event EventHandler Clicked;
        public virtual void OnClicked()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }
#endif
    }
}
