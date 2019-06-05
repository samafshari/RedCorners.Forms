using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedCorners.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [ContentProperty("Items")]
    public partial class Tabbar
    {
        public Tabbar()
        {
            InitializeComponent();
            UpdateItems();
        }

        public IList<TabbarItem> Items
        {
            get => (IList<TabbarItem>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public int SelectedItem
        {
            get => (int)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public ICommand SelectedItemChangeCommand
        {
            get => (ICommand)GetValue(SelectedItemChangeCommandProperty);
            set => SetValue(SelectedItemChangeCommandProperty, value);
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

        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
            propertyName: nameof(Items),
            returnType: typeof(IList<TabbarItem>),
            declaringType: typeof(Tabbar),
            defaultValue: new ObservableCollection<TabbarItem>(),
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

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
            propertyName: nameof(SelectedItem),
            returnType: typeof(int),
            declaringType: typeof(Tabbar),
            defaultValue: 0,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is Tabbar tabbar)
                {
                    tabbar.UpdateSelectedItem();
                }
            });

        public static readonly BindableProperty SelectedItemChangeCommandProperty = BindableProperty.Create(
            propertyName: nameof(SelectedItemChangeCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(Tabbar),
            defaultValue: null);

        public static readonly BindableProperty ImageMarginProperty = BindableProperty.Create(
            nameof(ImageMargin),
            typeof(Thickness),
            typeof(Tabbar),
            new Thickness(8, 8),
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is Tabbar tabbar)
                {
                    tabbar.UpdateSelectedItem();
                }
            });

        public static readonly BindableProperty OrientationProperty = BindableProperty.Create(
            nameof(Orientation),
            typeof(StackOrientation),
            typeof(Tabbar),
            StackOrientation.Horizontal,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is Tabbar tabbar)
                {
                    tabbar.UpdateItems();
                }
            });

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
                bool pressed = SelectedItem == i || pressedIndex == i;
                if (pressed && item.SelectedImage != null)
                    source = item.SelectedImage;
                child.Source = source;
                child.Opacity = pressed ? item.SelectedOpacity : item.Opacity;
                child.ImageMargin = ImageMargin;
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
                SelectedItem = i;
                if (SelectedItemChangeCommand?.CanExecute(i) ?? false)
                    SelectedItemChangeCommand.Execute(i);
                item.Command?.Execute(item.CommandParameter);
                UpdateSelectedItem();
            }
        });
    }
}