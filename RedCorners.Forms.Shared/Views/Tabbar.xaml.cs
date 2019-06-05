using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public ObservableCollection<TabbarItem> Items
        {
            get => (ObservableCollection<TabbarItem>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public int SelectedItem
        {
            get => (int)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
            propertyName: nameof(Items),
            returnType: typeof(ObservableCollection<TabbarItem>),
            declaringType: typeof(Tabbar),
            defaultValue: null,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is Tabbar tabbar)
                {
                    if (newVal != null)
                    {
                        (newVal as ObservableCollection<TabbarItem>).CollectionChanged += tabbar.Tabbar_CollectionChanged;
                    }
                    if (oldVal != null)
                    {
                        (newVal as ObservableCollection<TabbarItem>).CollectionChanged -= tabbar.Tabbar_CollectionChanged;
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

        private void Tabbar_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateItems();
        }

        void UpdateItems()
        {
            if (Items == null) return;
            content.Children.Clear();
            content.ColumnDefinitions.Clear();

            int c = 0;
            foreach (var item in Items)
            {
                item.PropertyChanged -= Item_PropertyChanged;
                item.PropertyChanged += Item_PropertyChanged;

                var img = new ImageButton
                {
                    BindingContext = item
                };

                content.Children.Add(img);
                content.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = GridLength.Star
                });
                img.SetValue(Grid.ColumnProperty, c++);
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
                if (SelectedItem == i && item.SelectedImage != null)
                {
                    source = item.SelectedImage;
                }
                child.Source = source;
                child.PressedSource = item.SelectedImage;

            }
        } 
    }
}