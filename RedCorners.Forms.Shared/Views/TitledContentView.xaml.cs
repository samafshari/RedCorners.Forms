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
    public enum TitleBarPositions
    {
        Top = 0,
        Bottom
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    [ContentProperty("Body")]
    public partial class TitledContentView
    {
        public View Body
        {
            get => (View)GetValue(BodyProperty);
            set => SetValue(BodyProperty, value);
        }

        public View ToolBar
        {
            get => (View)GetValue(ToolBarProperty);
            set => SetValue(ToolBarProperty, value);
        }

        public View LeftToolBar
        {
            get => (View)GetValue(LeftToolBarProperty);
            set => SetValue(LeftToolBarProperty, value);
        }

        public double TitleHeightRequest
        {
            get => (double)GetValue(TitleHeightRequestProperty);
            set => SetValue(TitleHeightRequestProperty, value);
        }

        public Thickness TitlePadding
        {
            get => (Thickness)GetValue(TitlePaddingProperty);
            set => SetValue(TitlePaddingProperty, value);
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

        public bool HasButton
        {
            get => (bool)GetValue(HasButtonProperty);
            set => SetValue(HasButtonProperty, value);
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

        public View TitleBackgroundView
        {
            get => (View)GetValue(TitleBackgroundViewProperty);
            set => SetValue(TitleBackgroundViewProperty, value);
        }

        public TitleBarPositions TitlePosition
        {
            get => (TitleBarPositions)GetValue(TitlePositionProperty);
            set => SetValue(TitlePositionProperty, value);
        }

        public ImageSource TitleBackgroundImage
        {
            get => (ImageSource)GetValue(TitleBackgroundImageProperty);
            set => SetValue(TitleBackgroundImageProperty, value);
        }

        public bool FixTitlePadding
        {
            get => (bool)GetValue(FixTitlePaddingProperty);
            set => SetValue(FixTitlePaddingProperty, value);
        }

        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }

        [TypeConverter(typeof(FontSizeConverter))]
        public double TitleFontSize
        {
            get => (double)GetValue(TitleFontSizeProperty);
            set => SetValue(TitleFontSizeProperty, value);
        }

        public string TitleFontFamily
        {
            get => (string)GetValue(TitleFontFamilyProperty);
            set => SetValue(TitleFontFamilyProperty, value);
        }

        public FontAttributes TitleFontAttributes
        {
            get => (FontAttributes)GetValue(TitleFontAttributesProperty);
            set => SetValue(TitleFontAttributesProperty, value);
        }

        public TitleAlignments TitleAlignment
        {
            get => (TitleAlignments)GetValue(TitleAlignmentProperty);
            set => SetValue(TitleAlignmentProperty, value);
        }

        public View TitleView
        {
            get => (View)GetValue(TitleViewProperty);
            set => SetValue(TitleViewProperty, value);
        }

        public ImageSource CustomBackImage
        {
            get => (ImageSource)GetValue(CustomBackImageProperty);
            set => SetValue(CustomBackImageProperty, value);
        }

        public bool IsDark
        {
            get => (bool)GetValue(IsDarkProperty);
            set => SetValue(IsDarkProperty, value);
        }

        public double TitleBackgroundImageOpacity
        {
            get => (double)GetValue(TitleBackgroundImageOpacityProperty);
            set => SetValue(TitleBackgroundImageOpacityProperty, value);
        }

        public static readonly BindableProperty IsDarkProperty = BindableProperty.Create(
            propertyName: nameof(IsDark),
            returnType: typeof(bool),
            declaringType: typeof(TitledContentView),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay);


        public static readonly BindableProperty CustomBackImageProperty = BindableProperty.Create(
            propertyName: nameof(CustomBackImage),
            returnType: typeof(ImageSource),
            declaringType: typeof(TitledContentView),
            defaultValue: default(ImageSource),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
            });

        public static readonly BindableProperty TitleAlignmentProperty = BindableProperty.Create(
            propertyName: nameof(TitleAlignment),
            returnType: typeof(TitleAlignments),
            declaringType: typeof(TitledContentView),
            defaultValue: TitleAlignments.Start,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
            });

        public static readonly BindableProperty TitleHeightRequestProperty = BindableProperty.Create(
            propertyName: nameof(TitleHeightRequest),
            returnType: typeof(double),
            declaringType: typeof(TitledContentView),
            defaultValue: 60.0,
            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty TitlePaddingProperty = BindableProperty.Create(
            propertyName: nameof(TitlePadding),
            returnType: typeof(Thickness),
            declaringType: typeof(TitledContentView),
            defaultValue: default(Thickness),
            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty TitleFontSizeProperty = BindableProperty.Create(
            propertyName: nameof(TitleFontSize),
            returnType: typeof(double),
            declaringType: typeof(TitledContentView),
            defaultValue: 16.0,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {

            });

        public static readonly BindableProperty TitleFontFamilyProperty = BindableProperty.Create(
            propertyName: nameof(TitleFontFamily),
            returnType: typeof(string),
            declaringType: typeof(TitledContentView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {

            });

        public static readonly BindableProperty TitleFontAttributesProperty = BindableProperty.Create(
            propertyName: nameof(TitleFontAttributes),
            returnType: typeof(FontAttributes),
            declaringType: typeof(TitledContentView),
            defaultValue: FontAttributes.Bold,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {

            });

        public static readonly BindableProperty HasButtonProperty = BindableProperty.Create(
            propertyName: nameof(HasButton),
            returnType: typeof(bool),
            declaringType: typeof(TitledContentView),
            defaultValue: true,
            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty HasShadowProperty =
            BindableProperty.Create(
            nameof(HasShadow),
            typeof(bool),
            typeof(TitledContentView),
            true,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TitledContentView page)
                {
                    page.UpdateTitlePosition();
                }
            });

        public static readonly BindableProperty FixTitlePaddingProperty = BindableProperty.Create(
            propertyName: nameof(FixTitlePadding),
            returnType: typeof(bool),
            declaringType: typeof(TitledContentView),
            defaultValue: true,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TitledContentView page)
                {
                    page.UpdateTitlePosition();
                }
            });

        public static readonly BindableProperty TitlePositionProperty = BindableProperty.Create(
            propertyName: nameof(TitlePosition),
            returnType: typeof(TitleBarPositions),
            declaringType: typeof(TitledContentView),
            defaultValue: TitleBarPositions.Top,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TitledContentView page)
                {
                    page.UpdateTitlePosition();
                }
            });

        public static readonly BindableProperty BodyProperty = BindableProperty.Create(
            propertyName: nameof(Body),
            returnType: typeof(View),
            declaringType: typeof(TitledContentView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TitledContentView page)
                {
                    page.content.Content = newVal as View;
                }
            });

        public static readonly BindableProperty ToolBarProperty = BindableProperty.Create(
            propertyName: nameof(ToolBar),
            returnType: typeof(View),
            declaringType: typeof(TitledContentView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
            });

        public static readonly BindableProperty LeftToolBarProperty = BindableProperty.Create(
            propertyName: nameof(LeftToolBar),
            returnType: typeof(View),
            declaringType: typeof(TitledContentView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
            });

        public static readonly BindableProperty TitleViewProperty = BindableProperty.Create(
            propertyName: nameof(TitleView),
            returnType: typeof(View),
            declaringType: typeof(TitledContentView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
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
            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty BackCommandProperty = BindableProperty.Create(
            propertyName: nameof(BackCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(TitledContentView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty BackCommandParameterProperty = BindableProperty.Create(
            propertyName: nameof(BackCommandParameter),
            returnType: typeof(object),
            declaringType: typeof(TitledContentView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty TitleBackgroundViewProperty = BindableProperty.Create(
            propertyName: nameof(TitleBackgroundView),
            returnType: typeof(View),
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

        public static readonly BindableProperty TitleColorProperty = BindableProperty.Create(
            propertyName: nameof(TitleColor),
            returnType: typeof(Color),
            declaringType: typeof(TitledContentView),
            defaultValue: Color.FromHex("#000000"),
            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty TitleTextColorProperty = BindableProperty.Create(
            propertyName: nameof(TitleTextColor),
            returnType: typeof(Color),
            declaringType: typeof(TitledContentView),
            defaultValue: Color.White,
            defaultBindingMode: BindingMode.TwoWay);


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

        public TitledContentView()
        {
            InitializeComponent();
            UpdateTitleBackgroundImage();
            UpdateTitlePosition();
        }

        void UpdateTitleBackgroundImage()
        {
            if (titlebar == null) return;
            titlebar.BackgroundImage = TitleBackgroundImage;
            titlebar.BackgroundImageOpacity = TitleBackgroundImageOpacity;
            titlebar.BackgroundView = TitleBackgroundView;
        }

        void UpdateTitlePosition()
        {
            if (titlebar != null)
            {
                if (FixTitlePadding)
                {
                    if (TitlePosition == TitleBarPositions.Top)
                    {
                        titlebar.FixTopPadding = true;
                        titlebar.FixBottomPadding = false;
                    }
                    else
                    {
                        titlebar.FixTopPadding = false;
                        titlebar.FixBottomPadding = true;
                    }
                }
                else
                {
                    titlebar.FixTopPadding = false;
                    titlebar.FixBottomPadding = false;
                }

                var row = TitlePosition == TitleBarPositions.Top ? 0 : 2;
                titlebar.SetValue(Grid.RowProperty, row);
            }

            if (shadow != null)
            {
                shadow.IsVisible = TitlePosition == TitleBarPositions.Top && HasShadow;
            }

            if (shadow2 != null)
            {
                shadow2.IsVisible = TitlePosition == TitleBarPositions.Bottom && HasShadow;
            }
        }
    }
}