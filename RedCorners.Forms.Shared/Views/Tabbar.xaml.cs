using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RedCorners.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [ContentProperty("Items")]
    public partial class Tabbar
    {
        public Tabbar()
        {
            Items = new ObservableCollection<TabbarItem>();
            InitializeComponent();
            UpdateItems();
        }

        public IList<TabbarItem> Items
        {
            get => (IList<TabbarItem>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        public ICommand SelectedIndexChangeCommand
        {
            get => (ICommand)GetValue(SelectedIndexChangeCommandProperty);
            set => SetValue(SelectedIndexChangeCommandProperty, value);
        }

        public Thickness ImageMargin
        {
            get => (Thickness)GetValue(ImageMarginProperty);
            set => SetValue(ImageMarginProperty, value);
        }

        public StackOrientation Orientation
        {
            get => (StackOrientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public FontAttributes FontAttributes
        {
            get => (FontAttributes)GetValue(FontAttributesProperty);
            set => SetValue(FontAttributesProperty, value);
        }

        public Color SelectedTextColor
        {
            get => (Color)GetValue(SelectedTextColorProperty);
            set => SetValue(SelectedTextColorProperty, value);
        }

        public double? SelectedFontSize
        {
            get => (double?)GetValue(SelectedFontSizeProperty);
            set => SetValue(SelectedFontSizeProperty, value);
        }

        public FontAttributes? SelectedFontAttributes
        {
            get => (FontAttributes?)GetValue(SelectedFontAttributesProperty);
            set => SetValue(SelectedFontAttributesProperty, value);
        }

        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public GridLength TextHeight
        {
            get => (GridLength)GetValue(TextHeightProperty);
            set => SetValue(TextHeightProperty, value);
        }

        public double ImageWidthRequest
        {
            get => (double)GetValue(ImageWidthRequestProperty);
            set => SetValue(ImageWidthRequestProperty, value);
        }

        public double ImageHeightRequest
        {
            get => (double)GetValue(ImageHeightRequestProperty);
            set => SetValue(ImageHeightRequestProperty, value);
        }

        public ImageButtonStyles ImageButtonStyle
        {
            get => (ImageButtonStyles)GetValue(ImageButtonStyleProperty);
            set => SetValue(ImageButtonStyleProperty, value);
        }

        public double Spacing
        {
            get => (double)GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }

        public static readonly BindableProperty TextHeightProperty = BindableProperty.Create(
            nameof(TextHeight),
            typeof(GridLength),
            typeof(Tabbar),
            GridLength.Auto,
            BindingMode.TwoWay,
            propertyChanged: UpdateItemsOnPropertyChanged);

        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
            propertyName: nameof(Items),
            returnType: typeof(IList<TabbarItem>),
            declaringType: typeof(Tabbar),
            defaultValue: null,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is Tabbar tabbar)
                {
                    if (newVal is ObservableCollection<TabbarItem>)
                    {
                        (newVal as ObservableCollection<TabbarItem>).CollectionChanged += tabbar.Tabbar_CollectionChanged;
                    }
                    if (oldVal is ObservableCollection<TabbarItem>)
                    {
                        (oldVal as ObservableCollection<TabbarItem>).CollectionChanged -= tabbar.Tabbar_CollectionChanged;
                    }
                    tabbar.UpdateItems();
                }
            });

        public static readonly BindableProperty ImageButtonStyleProperty = BindableProperty.Create(
            nameof(ImageButtonStyle),
            typeof(ImageButtonStyles),
            typeof(Tabbar),
            ImageButtonStyles.Image,
            BindingMode.TwoWay,
            propertyChanged: UpdateItemsOnPropertyChanged);

        public static readonly BindableProperty ImageHeightRequestProperty = BindableProperty.Create(
            nameof(ImageHeightRequest),
            typeof(double),
            typeof(Tabbar),
            -1.0,
            BindingMode.TwoWay,
            propertyChanged: UpdateItemsOnPropertyChanged);

        public static readonly BindableProperty ImageWidthRequestProperty = BindableProperty.Create(
            nameof(ImageWidthRequest),
            typeof(double),
            typeof(Tabbar),
            -1.0,
            BindingMode.TwoWay,
            propertyChanged: UpdateItemsOnPropertyChanged);

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            propertyName: nameof(TextColor),
            returnType: typeof(Color),
            declaringType: typeof(Tabbar),
            defaultValue: Color.Black,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: UpdateSelectedItemOnPropertyChanged);

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
            propertyName: nameof(FontSize),
            returnType: typeof(double),
            declaringType: typeof(Tabbar),
            defaultValue: 16.0,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: UpdateSelectedItemOnPropertyChanged);

        public static readonly BindableProperty SelectedTextColorProperty = BindableProperty.Create(
            propertyName: nameof(SelectedTextColor),
            returnType: typeof(Color),
            declaringType: typeof(Tabbar),
            defaultValue: Color.Default,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: UpdateSelectedItemOnPropertyChanged);

        public static readonly BindableProperty SelectedFontSizeProperty = BindableProperty.Create(
            propertyName: nameof(SelectedFontSize),
            returnType: typeof(double?),
            declaringType: typeof(Tabbar),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: UpdateSelectedItemOnPropertyChanged);

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
            propertyName: nameof(FontFamily),
            returnType: typeof(string),
            declaringType: typeof(Tabbar),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: UpdateSelectedItemOnPropertyChanged);

        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(
            propertyName: nameof(FontAttributes),
            returnType: typeof(FontAttributes),
            declaringType: typeof(Tabbar),
            defaultValue: FontAttributes.Bold,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: UpdateSelectedItemOnPropertyChanged);

        public static readonly BindableProperty SelectedFontAttributesProperty = BindableProperty.Create(
            propertyName: nameof(SelectedFontAttributes),
            returnType: typeof(FontAttributes?),
            declaringType: typeof(Tabbar),
            defaultValue: FontAttributes.Bold,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: UpdateSelectedItemOnPropertyChanged);


        public TextAlignment VerticalTextAlignment
        {
            get => (TextAlignment)GetValue(VerticalTextAlignmentProperty);
            set => SetValue(VerticalTextAlignmentProperty, value);
        }

        public TextAlignment HorizontalTextAlignment
        {
            get => (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
            set => SetValue(HorizontalTextAlignmentProperty, value);
        }

        public static readonly BindableProperty VerticalTextAlignmentProperty = BindableProperty.Create(
            propertyName: nameof(VerticalTextAlignment),
            returnType: typeof(TextAlignment),
            declaringType: typeof(Tabbar),
            defaultValue: TextAlignment.Start,
            propertyChanged: UpdateSelectedItemOnPropertyChanged);

        public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(
            propertyName: nameof(HorizontalTextAlignment),
            returnType: typeof(TextAlignment),
            declaringType: typeof(Tabbar),
            defaultValue: TextAlignment.Center,
            propertyChanged: UpdateSelectedItemOnPropertyChanged);

        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(
            propertyName: nameof(SelectedIndex),
            returnType: typeof(int),
            declaringType: typeof(Tabbar),
            defaultValue: 0,
            propertyChanged: UpdateSelectedItemOnPropertyChanged);

        public static readonly BindableProperty SelectedIndexChangeCommandProperty = BindableProperty.Create(
            propertyName: nameof(SelectedIndexChangeCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(Tabbar),
            defaultValue: null);

        public static readonly BindableProperty ImageMarginProperty = BindableProperty.Create(
            nameof(ImageMargin),
            typeof(Thickness),
            typeof(Tabbar),
            new Thickness(0),
            BindingMode.TwoWay,
            propertyChanged: UpdateSelectedItemOnPropertyChanged);

        public static readonly BindableProperty OrientationProperty = BindableProperty.Create(
            nameof(Orientation),
            typeof(StackOrientation),
            typeof(Tabbar),
            StackOrientation.Horizontal,
            BindingMode.TwoWay,
            propertyChanged: UpdateItemsOnPropertyChanged);

        public static readonly BindableProperty SpacingProperty = BindableProperty.Create(
            nameof(Spacing),
            typeof(double),
            typeof(Tabbar),
            (double)0,
            BindingMode.TwoWay,
            propertyChanged: UpdateItemsOnPropertyChanged);

        static void UpdateItemsOnPropertyChanged(BindableObject bindable, object oldVal, object newVal)
        {
            if (bindable is Tabbar tabbar)
            {
                tabbar.UpdateItems();
            }
        }

        static void UpdateSelectedItemOnPropertyChanged(BindableObject bindable, object oldVal, object newVal)
        {
            if (bindable is Tabbar tabbar)
            {
                tabbar.UpdateSelectedItem();
            }
        }

        private void Tabbar_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateItems();
        }

        void UpdateItems()
        {
            if (Items == null) return;
            if (content == null) return;
            content.Children.Clear();
            content.ColumnDefinitions.Clear();
            content.RowDefinitions.Clear();

            int c = 0;
            foreach (var item in Items as IList<TabbarItem>)
            {
                item.PropertyChanged -= Item_PropertyChanged;
                item.PropertyChanged += Item_PropertyChanged;

                var img = new ImageButton
                {
                    BindingContext = item,
                    CommandParameter = c,
                    PressedCommandParameter = c,
                    ReleasedCommandParameter = c,
                    Command = Command,
                    PressedCommand = PressedCommand,
                    ReleasedCommand = ReleasedCommand,
                    Text = item.Text,
                    TextColor = TextColor,
                    FontAttributes = FontAttributes,
                    ImageButtonStyle = ImageButtonStyle,
                    FontFamily = FontFamily,
                    FontSize = FontSize,
                    TextHeight = TextHeight,
                    ImageMargin = ImageMargin,
                    RowSpacing = Spacing,
                    ImageHeightRequest = ImageHeightRequest,
                    ImageWidthRequest = ImageWidthRequest
                };

                content.Children.Add(img);
                if (Orientation == StackOrientation.Horizontal)
                {
                    content.ColumnDefinitions.Add(new ColumnDefinition
                    {
                        Width = GridLength.Star
                    });
                    img.SetValue(Grid.ColumnProperty, c++);
                    img.SetValue(Grid.RowProperty, 0);
                }
                else
                {
                    content.RowDefinitions.Add(new RowDefinition
                    {
                        Height = GridLength.Star
                    });
                    img.SetValue(Grid.RowProperty, c++);
                    img.SetValue(Grid.ColumnProperty, 0);
                }
            }
            UpdateSelectedItem();
        }

        private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UpdateSelectedItem();
        }

        void UpdateSelectedItem()
        {
            for (int i = 0; i < content.Children.Count; i++)
            {
                var child = content.Children[i] as ImageButton;
                var item = child.BindingContext as TabbarItem;
                var source = item.Image;
                bool pressed = SelectedIndex == i || pressedIndex == i;
                if (pressed && item.SelectedImage != null)
                    source = item.SelectedImage;
                child.Source = source;
                ViewExtensions.CancelAnimations(child);
                child.FadeTo(pressed ? item.SelectedOpacity : item.Opacity, 100);
                child.ImageMargin = ImageMargin;
                child.TextColor = (pressed && SelectedTextColor != Color.Default) ? SelectedTextColor : TextColor;
                child.FontSize = (pressed && SelectedFontSize.HasValue) ? SelectedFontSize.Value : FontSize;
                child.FontAttributes = (pressed && SelectedFontAttributes.HasValue ? SelectedFontAttributes.Value : FontAttributes);
                child.FontFamily = FontFamily;
                child.Text = item.Text;
                child.RowSpacing = Spacing;
                child.VerticalTextAlignment = VerticalTextAlignment;
                child.HorizontalTextAlignment = HorizontalTextAlignment;
            }
        }

        int pressedIndex = -1;
        public Command<int> PressedCommand => new Command<int>(i =>
        {
            pressedIndex = i;
            UpdateSelectedItem();
        });

        public Command<int> ReleasedCommand => new Command<int>(i =>
        {
            pressedIndex = -1;
            UpdateSelectedItem();
        });

        public Command<int> Command => new Command<int>(i =>
        {
            var button = content.Children[i] as ImageButton;
            var item = button.BindingContext as TabbarItem;
            var can = item.Command?.CanExecute(item.CommandParameter) ?? true;
            if (can)
            {
                SelectedIndex = i;
                if (SelectedIndexChangeCommand?.CanExecute(i) ?? false)
                    SelectedIndexChangeCommand.Execute(i);
                item.Command?.Execute(item.CommandParameter);
                UpdateSelectedItem();
            }
        });
    }
}