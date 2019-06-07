using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RedCorners.Forms
{
    public class TabbarItem : BindableObject
    {
        public ImageSource Image
        {
            get => (ImageSource)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        public ImageSource SelectedImage
        {
            get => (ImageSource)GetValue(SelectedImageProperty);
            set => SetValue(SelectedImageProperty, value);
        }

        public float Opacity
        {
            get => (float)GetValue(OpacityProperty);
            set => SetValue(OpacityProperty, value);
        }

        public float SelectedOpacity
        {
            get => (float)GetValue(SelectedOpacityProperty);
            set => SetValue(SelectedOpacityProperty, value);
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

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            propertyName: nameof(Text),
            returnType: typeof(string),
            declaringType: typeof(TabbarItem),
            defaultValue: null);

        public static readonly BindableProperty ImageProperty = BindableProperty.Create(
            propertyName: nameof(Image),
            returnType: typeof(ImageSource),
            declaringType: typeof(TabbarItem),
            defaultValue: null);

        public static readonly BindableProperty SelectedImageProperty = BindableProperty.Create(
            propertyName: nameof(SelectedImage),
            returnType: typeof(ImageSource),
            declaringType: typeof(TabbarItem),
            defaultValue: null);

        public static readonly BindableProperty OpacityProperty = BindableProperty.Create(
            propertyName: nameof(Opacity),
            returnType: typeof(float),
            declaringType: typeof(TabbarItem),
            defaultValue: 0.5f);

        public static readonly BindableProperty SelectedOpacityProperty = BindableProperty.Create(
            propertyName: nameof(Opacity),
            returnType: typeof(float),
            declaringType: typeof(TabbarItem),
            defaultValue: 1.0f);

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            propertyName: nameof(Command),
            returnType: typeof(ICommand),
            declaringType: typeof(TabbarItem),
            defaultValue: null);

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            propertyName: nameof(CommandParameter),
            returnType: typeof(object),
            declaringType: typeof(TabbarItem),
            defaultValue: null);
    }
}
