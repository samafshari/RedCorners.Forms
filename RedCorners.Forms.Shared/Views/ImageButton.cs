using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RedCorners.Forms
{
    public class ImageButton : Grid
    {
        readonly Image image;
        public ImageButton()
        {
            image = new Image
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Aspect = Aspect.AspectFit
            };
            Children.Add(image);

            image.BindingContext = this;
            image.SetBinding(Image.SourceProperty, nameof(Source));
            image.InputTransparent = true;
            image.Margin = ImageMargin;

            var button = new Button2 {
                BindingContext = this,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };

            button.Pressed += Button_Pressed;
            button.Released += Button_Released;
            button.Clicked += Button_Clicked;

            button.BackgroundColor = Color.Transparent;
            button.Opacity = 0;
            Children.Add(button);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (Command?.CanExecute(CommandParameter) ?? false)
                Command.Execute(CommandParameter);
        }

        private void Button_Released(object sender, EventArgs e)
        {
            if (ReleasedCommand?.CanExecute(ReleasedCommandParameter) ?? false)
                ReleasedCommand.Execute(ReleasedCommandParameter);
        }

        private void Button_Pressed(object sender, EventArgs e)
        {
            if (PressedCommand?.CanExecute(PressedCommandParameter) ?? false)
                PressedCommand.Execute(PressedCommandParameter);
        }

        public ImageSource Source
        {
            get => (ImageSource)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
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

        public ICommand PressedCommand
        {
            get => (ICommand)GetValue(PressedCommandProperty);
            set => SetValue(PressedCommandProperty, value);
        }

        public object PressedCommandParameter
        {
            get => GetValue(PressedCommandParameterProperty);
            set => SetValue(PressedCommandParameterProperty, value);
        }

        public ICommand ReleasedCommand
        {
            get => (ICommand)GetValue(ReleasedCommandProperty);
            set => SetValue(ReleasedCommandProperty, value);
        }

        public object ReleasedCommandParameter
        {
            get => GetValue(ReleasedCommandParameterProperty);
            set => SetValue(ReleasedCommandParameterProperty, value);
        }

        public Thickness ImageMargin
        {
            get => (Thickness)GetValue(ImageMarginProperty);
            set => SetValue(ImageMarginProperty, value);
        }

        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            propertyName: nameof(Source),
            returnType: typeof(ImageSource),
            declaringType: typeof(ImageButton),
            defaultValue: null);

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(ImageButton),
            null,
            BindingMode.TwoWay);

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(ImageButton),
            null,
            BindingMode.TwoWay);

        public static readonly BindableProperty PressedCommandProperty = BindableProperty.Create(
            nameof(PressedCommand),
            typeof(ICommand),
            typeof(ImageButton),
            null,
            BindingMode.TwoWay);

        public static readonly BindableProperty PressedCommandParameterProperty = BindableProperty.Create(
            nameof(PressedCommandParameter),
            typeof(object),
            typeof(ImageButton),
            null,
            BindingMode.TwoWay);

        public static readonly BindableProperty ReleasedCommandProperty = BindableProperty.Create(
            nameof(ReleasedCommand),
            typeof(ICommand),
            typeof(ImageButton),
            null,
            BindingMode.TwoWay);

        public static readonly BindableProperty ReleasedCommandParameterProperty = BindableProperty.Create(
            nameof(ReleasedCommandParameter),
            typeof(object),
            typeof(ImageButton),
            null,
            BindingMode.TwoWay);

        public static readonly BindableProperty ImageMarginProperty = BindableProperty.Create(
            nameof(ImageMargin),
            typeof(Thickness),
            typeof(ImageButton),
            new Thickness(0,0),
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is ImageButton butt)
                {
                    butt.image.Margin = (Thickness)newVal;
                }
            });
    }
}
