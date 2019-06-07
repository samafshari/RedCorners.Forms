using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedCorners.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [ContentProperty("Body")]
    public partial class TitledContentView
    {
        public View Body
        {
            get => (View)GetValue(BodyProperty);
            set => SetValue(BodyProperty, value);
        }

        public View Buttons
        {
            get => (View)GetValue(ButtonsProperty);
            set => SetValue(ButtonsProperty, value);
        }

        public View Overlay
        {
            get => (View)GetValue(OverlayProperty);
            set => SetValue(OverlayProperty, value);
        }

        public bool? IsBackButtonVisible
        {
            get => (bool?)GetValue(IsBackButtonVisibleProperty);
            set => SetValue(IsBackButtonVisibleProperty, value);
        }

        public ICommand BackCommand
        {
            get => (ICommand)GetValue(BackCommandProperty);
            set => SetValue(BackCommandProperty, value);
        }

        public object BackCommandParameter
        {
            get => GetValue(BackCommandParameterProperty);
            set => SetValue(BackCommandParameterProperty, value);
        }

        public Color TitleColor
        {
            get => (Color)GetValue(TitleColorProperty);
            set => SetValue(TitleColorProperty, value);
        }

        public Color TitleTextColor
        {
            get => (Color)GetValue(TitleTextColorProperty);
            set => SetValue(TitleTextColorProperty, value);
        }

        public ImageSource TitleBackgroundImage
        {
            get => (ImageSource)GetValue(TitleBackgroundImageProperty);
            set => SetValue(TitleBackgroundImageProperty, value);
        }

        public double TitleBackgroundImageOpacity
        {
            get => (double)GetValue(TitleBackgroundImageOpacityProperty);
            set => SetValue(TitleBackgroundImageOpacityProperty, value);
        }

        public static readonly BindableProperty BodyProperty = BindableProperty.Create(
            propertyName: nameof(Body),
            returnType: typeof(View),
            declaringType: typeof(TitledContentView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                Console.WriteLine("Body is changing");
                if (bindable is TitledContentView page)
                {
                    page.content.Content = newVal as View;
                }
            });

        public static readonly BindableProperty ButtonsProperty = BindableProperty.Create(
            propertyName: nameof(Buttons),
            returnType: typeof(View),
            declaringType: typeof(TitledContentView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TitledContentView page)
                    page.titlebar.Buttons = (View)newVal;
            });

        public static readonly BindableProperty OverlayProperty = BindableProperty.Create(
            propertyName: nameof(Overlay),
            returnType: typeof(View),
            declaringType: typeof(TitledContentView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TitledContentView page)
                {
                    page.overlay.Content = (View)newVal;
                }
            });

        public static readonly BindableProperty IsBackButtonVisibleProperty = BindableProperty.Create(
            propertyName: nameof(IsBackButtonVisible),
            returnType: typeof(bool?),
            declaringType: typeof(TitledContentView),
            defaultValue: default(bool?),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TitledContentView page)
                    page.titlebar.IsBackButtonVisible = (bool?)newVal;
            });

        public static readonly BindableProperty BackCommandProperty = BindableProperty.Create(
            propertyName: nameof(BackCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(TitledContentView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TitledContentView page)
                    page.titlebar.BackCommand = (ICommand)newVal;
            });

        public static readonly BindableProperty BackCommandParameterProperty = BindableProperty.Create(
            propertyName: nameof(BackCommandParameter),
            returnType: typeof(object),
            declaringType: typeof(TitledContentView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TitledContentView page)
                    page.titlebar.BackCommandParameter = newVal;
            });

        public static readonly BindableProperty TitleBackgroundImageProperty = BindableProperty.Create(
            propertyName: nameof(TitleBackgroundImage),
            returnType: typeof(ImageSource),
            declaringType: typeof(TitledContentView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TitledContentView page)
                {
                    page.UpdateTitleBackgroundImage();
                }
            });

        public static readonly BindableProperty TitleBackgroundImageOpacityProperty = BindableProperty.Create(
            propertyName: nameof(TitleBackgroundImageOpacity),
            returnType: typeof(double),
            declaringType: typeof(TitledContentView),
            defaultValue: 1.0,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TitledContentView page)
                {
                    page.UpdateTitleBackgroundImage();
                }
            });

        public static readonly BindableProperty TitleColorProperty = BindableProperty.Create(
            propertyName: nameof(TitleColor),
            returnType: typeof(Color),
            declaringType: typeof(TitledContentView),
            defaultValue: Color.FromHex("#000000"),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TitledContentView page)
                    page.titlebar.BackgroundColor = (Color)newVal;
            });

        public static readonly BindableProperty TitleTextColorProperty = BindableProperty.Create(
            propertyName: nameof(TitleTextColor),
            returnType: typeof(Color),
            declaringType: typeof(TitledContentView),
            defaultValue: Color.White,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TitledContentView page)
                    page.titlebar.TextColor = (Color)newVal;
            });

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(Title))
                titlebar.Title = Title;
        }

        public TitledContentView()
        {
            InitializeComponent();
            UpdateTitleBackgroundImage();
        }

        void UpdateTitleBackgroundImage()
        {
            if (titlebar == null) return;
            if (TitleBackgroundImage == null)
            {
                titlebar.Body = null;
            }
            else
            {
                titlebar.Body = new Image
                {
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Fill,
                    Source = TitleBackgroundImage,
                    Aspect = Aspect.AspectFill,
                    Opacity = TitleBackgroundImageOpacity
                };
            }
        }
    }
}