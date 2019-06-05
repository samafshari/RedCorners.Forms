using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RedCorners.Forms
{
    public class ImageButton : Grid
    {
        public ImageButton()
        {
            var image = new Image
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Aspect = Aspect.AspectFit
            };
            Children.Add(image);

            image.BindingContext = this;
            image.SetBinding(Image.SourceProperty, nameof(Source));
            image.InputTransparent = true;

            var tap = new TapGestureRecognizer();
            tap.Tapped += Tap_Tapped;

            GestureRecognizers.Add(tap);
            image.GestureRecognizers.Add(tap);
        }

        private void Tap_Tapped(object sender, EventArgs e)
        {
            if (Command?.CanExecute(CommandParameter) ?? false)
                Command.Execute(CommandParameter);
        }

        public ImageSource Source
        {
            get => (ImageSource)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public ImageSource PressedSource
        {
            get => (ImageSource)GetValue(PressedSourceProperty);
            set => SetValue(PressedSourceProperty, value);
        }

        public float NormalOpacity
        {
            get => (float)GetValue(NormalOpacityProperty);
            set => SetValue(NormalOpacityProperty, value);
        }

        public float PressedOpacity
        {
            get => (float)GetValue(PressedOpacityProperty);
            set => SetValue(PressedOpacityProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            propertyName: nameof(Source),
            returnType: typeof(ImageSource),
            declaringType: typeof(ImageButton),
            defaultValue: null);

        public static readonly BindableProperty PressedSourceProperty = BindableProperty.Create(
            propertyName: nameof(PressedSource),
            returnType: typeof(ImageSource),
            declaringType: typeof(ImageButton),
            defaultValue: null);

        public static readonly BindableProperty NormalOpacityProperty = BindableProperty.Create(
            propertyName: nameof(NormalOpacity),
            returnType: typeof(float),
            declaringType: typeof(ImageButton),
            defaultValue: 1.0f);

        public static readonly BindableProperty PressedOpacityProperty = BindableProperty.Create(
            propertyName: nameof(PressedOpacity),
            returnType: typeof(float),
            declaringType: typeof(ImageButton),
            defaultValue: 1.0f);

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(ImageButton),
            null,
            BindingMode.TwoWay);

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(Command),
            typeof(object),
            typeof(ImageButton),
            null,
            BindingMode.TwoWay);
    }
}
