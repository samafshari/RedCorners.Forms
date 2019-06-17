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

        public View TitleBackground
        {
            get => (View)GetValue(TitleBackgroundProperty);
            set => SetValue(TitleBackgroundProperty, value);
        }

        public TitleBarPositions TitlePosition
        {
            get => (TitleBarPositions)GetValue(TitlePositionProperty);
            set => SetValue(TitlePositionProperty, value);
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

        public static readonly BindableProperty HasButtonProperty = BindableProperty.Create(
            propertyName: nameof(HasButton),
            returnType: typeof(bool),
            declaringType: typeof(TitledContentView),
            defaultValue: true,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TitledContentView page)
                {
                    page.titlebar.HasButton = (bool)newVal;
                }
            });

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
                    page.titlebar.ToolBar = (View)newVal;
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

        public static readonly BindableProperty TitleBackgroundProperty = BindableProperty.Create(
            propertyName: nameof(TitleBackground),
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
            UpdateTitlePosition();
        }

        void UpdateTitleBackgroundImage()
        {
            if (titlebar == null) return;
            titlebar.Background = TitleBackground;
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