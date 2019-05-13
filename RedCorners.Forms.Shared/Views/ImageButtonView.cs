using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RedCorners.Forms
{
    public class ImageButtonView : Grid
    {
        public ImageButtonView()
        {
            var image = new Image
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Aspect = Aspect.AspectFit
            };
            Children.Add(image);
            image.BindingContext = this;
            image.SetBinding(Image.SourceProperty, "Source");

            var tap = new TapGestureRecognizer();
            tap.BindingContext = this;
            tap.SetBinding(TapGestureRecognizer.CommandProperty, "Command");
            tap.SetBinding(TapGestureRecognizer.CommandParameterProperty, "CommandParameter");

            GestureRecognizers.Add(tap);
            image.GestureRecognizers.Add(tap);
        }

        public ImageSource Source
        {
            get => (ImageSource)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            propertyName: "Source",
            returnType: typeof(ImageSource),
            declaringType: typeof(ImageButtonView),
            defaultValue: null);

        public object Command { get; set; }
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            "Command",
            typeof(object),
            typeof(ImageButtonView),
            null,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var view = bindable as ImageButtonView;
                view.Command = newValue;
            });

        private string _commandParameter;
        public string CommandParameter
        {
            get { return _commandParameter; }
            set
            {
                _commandParameter = value;
                OnPropertyChanged(nameof(CommandParameter));
            }
        }
    }
}
