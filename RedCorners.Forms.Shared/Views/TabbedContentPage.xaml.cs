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
    [ContentProperty("Children")]
    public partial class TabbedContentPage
    {
        public TabbedContentPage()
        {
            Children = new ObservableCollection<AliveContentView>();
            InitializeComponent();
            UpdateTabbarBackgroundView();
            UpdateChildren();
            tabbar.SelectedItem = SelectedTab;
            tabbarContainer.BackgroundColor = BackgroundColor;
            tabbar.PropertyChanged += Tabbar_PropertyChanged;
            tabbar.HeightRequest = TabbarHeightRequest;
            tabbarContainer.IsVisible = IsTabbarVisible;
            overlay.Content = Overlay;
        }

        public Color TabbarBackgroundColor
        {
            get => (Color)GetValue(TabbarBackgroundColorProperty);
            set => SetValue(TabbarBackgroundColorProperty, value);
        }

        public View TabbarBackgroundView
        {
            get => (View)GetValue(TabbarBackgroundViewProperty);
            set => SetValue(TabbarBackgroundViewProperty, value);
        }

        public double TabbarHeightRequest
        {
            get => (double)GetValue(TabbarHeightRequestProperty);
            set => SetValue(TabbarHeightRequestProperty, value);
        }

        public IList<AliveContentView> Children
        {
            get => (IList<AliveContentView>)GetValue(ChildrenProperty);
            set => SetValue(ChildrenProperty, value);
        }

        public int SelectedTab
        {
            get => (int)GetValue(SelectedTabProperty);
            protected set => SetValue(SelectedTabProperty, value);
        }

        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        public ImageButtonStyles TabStyle
        {
            get => (ImageButtonStyles)GetValue(TabStyleProperty);
            set => SetValue(TabStyleProperty, value);
        }

        public Thickness ImageMargin
        {
            get => (Thickness)GetValue(ImageMarginProperty);
            set => SetValue(ImageMarginProperty, value);
        }

        public View Overlay
        {
            get => (View)GetValue(OverlayProperty);
            set => SetValue(OverlayProperty, value);
        }

        public bool IsTabbarVisible
        {
            get => (bool)GetValue(IsTabbarVisibleProperty);
            set => SetValue(IsTabbarVisibleProperty, value);
        }

        public static readonly BindableProperty TabbarBackgroundViewProperty =
            BindableProperty.Create(
            nameof(TabbarBackgroundView),
            typeof(View),
            typeof(TabbedContentPage),
            default(View),
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentPage page)
                {
                    page.UpdateTabbarBackgroundView();
                }
            });

        public static readonly BindableProperty ChildrenProperty = BindableProperty.Create(
            propertyName: nameof(Children),
            returnType: typeof(IList<AliveContentView>),
            declaringType: typeof(TabbedContentPage),
            defaultValue: null,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentPage page)
                {
                    if (oldVal is IList<AliveContentView> views)
                    {
                        foreach (var item in views)
                            item.PropertyChanged -= page.Child_PropertyChanged;
                    }

                    if (newVal is ObservableCollection<AliveContentView>)
                    {
                        (newVal as ObservableCollection<AliveContentView>).CollectionChanged += page.TabbedContentPage_CollectionChanged;
                    }
                    if (oldVal is ObservableCollection<AliveContentView>)
                    {
                        (oldVal as ObservableCollection<AliveContentView>).CollectionChanged -= page.TabbedContentPage_CollectionChanged;
                    }
                    page.UpdateChildren();
                }
            });

        public static readonly BindableProperty SelectedTabProperty =
            BindableProperty.Create(
            nameof(SelectedTab),
            typeof(int),
            typeof(TabbedContentPage),
            0,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentPage page)
                    page.SelectTab();
            });

        public static readonly BindableProperty SelectedIndexProperty =
            BindableProperty.Create(
            nameof(SelectedIndex),
            typeof(int),
            typeof(TabbedContentPage),
            0,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentPage page)
                    page.UpdateActivePage();
            });

        private void TabbedContentPage_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateChildren();
        }

        public static readonly BindableProperty TabbarHeightRequestProperty = 
            BindableProperty.Create(
            nameof(TabbarHeightRequest),
            typeof(double),
            typeof(TabbedContentPage),
            70.0,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentPage page)
                    page.tabbar.HeightRequest = (double)page.TabbarHeightRequest;
            });

        public static readonly BindableProperty TabbarBackgroundColorProperty =
            BindableProperty.Create(
            nameof(TabbarBackgroundColor),
            typeof(Color),
            typeof(TabbedContentPage),
            Color.FromHex("#EEEEEE"),
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentPage page)
                    page.tabbarContainer.BackgroundColor = (Color)newVal;
            });

        public static readonly BindableProperty IsTabbarVisibleProperty =
            BindableProperty.Create(
            nameof(IsTabbarVisible),
            typeof(bool),
            typeof(TabbedContentPage),
            true,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentPage page)
                    page.tabbarContainer.IsVisible = (bool)newVal;
            });

        public static readonly BindableProperty TabStyleProperty =
            BindableProperty.Create(
                nameof(TabStyle),
                typeof(ImageButtonStyles),
                typeof(TabbedContentPage),
                ImageButtonStyles.Image,
                BindingMode.TwoWay,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    if (bindable is TabbedContentPage page)
                        page.UpdateTabs();
                });

        public static readonly BindableProperty ImageMarginProperty =
            BindableProperty.Create(
                nameof(ImageMargin),
                typeof(Thickness),
                typeof(TabbedContentPage),
                new Thickness(8),
                BindingMode.TwoWay,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    if (bindable is TabbedContentPage page)
                        if (page.tabbar != null)
                            page.tabbar.ImageMargin = (Thickness)newVal;
                });

        public static readonly BindableProperty OverlayProperty = BindableProperty.Create(
            propertyName: nameof(Overlay),
            returnType: typeof(View),
            declaringType: typeof(TabbedContentPage),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentPage page)
                {
                    page.overlay.Content = (View)newVal;
                }
            });

        void UpdateTabbarBackgroundView()
        {
            if (tabbarBackground != null)
                tabbarBackground.Content = TabbarBackgroundView;
        }

        private void Tabbar_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Tabbar.SelectedItem))
            {
                SelectedTab = tabbar.SelectedItem;
            }
        }

        void SelectTab()
        {
            if (SelectedTab < 0) return;
            if (Children == null) return;
            var tabbedPages = Children.Where(x => x.IsVisibleAsTab).ToList();
            if (SelectedTab >= tabbedPages.Count) return;
            SelectedIndex = Children.IndexOf(tabbedPages[SelectedTab]);
        }

        void UpdateChildren()
        {
            if (content == null) return;
            if (SelectedIndex >= Children.Count())
                SelectedIndex = 0;

            if (Children == null) return;

            foreach (var child in Children)
            {
                child.PropertyChanged -= Child_PropertyChanged;
                child.PropertyChanged += Child_PropertyChanged;
            }

            UpdateTabs();

            foreach (var view in Children)
            {
                view.HorizontalOptions = LayoutOptions.Fill;
                view.VerticalOptions = LayoutOptions.Fill;

                if (!content.Children.Contains(view))
                    content.Children.Add(view);
            }
            for (int i = 0; i < content.Children.Count; i++)
            {
                if (!Children.Contains(content.Children[i]))
                {
                    content.Children.RemoveAt(i);
                    i--;
                }
            }

            Console.WriteLine($"Children: {Children.Count} - Grid: {content.Children.Count}");
            UpdateActivePage();
        }

        private void Child_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var monitoredNames = new[]
            {
                nameof(AliveContentView.Title),
                nameof(AliveContentView.Icon),
                nameof(AliveContentView.ShowTabCommand),
                nameof(AliveContentView.SelectedIcon),
                nameof(AliveContentView.IsVisibleAsTab)
            };

            if (!monitoredNames.Contains(e.PropertyName))
                return;

            UpdateTabs();

            if (e.PropertyName == nameof(AliveContentView.IsVisibleAsTab))
            {
                SelectTab();
                UpdateActivePage();
            }
        }

        void UpdateTabs()
        {
            if (tabbar == null) return;
            var tabbedPages = Children.Where(x => x.IsVisibleAsTab).ToList();
            var tabItems = tabbedPages.Select(x => new TabbarItem
            {
                Command = x.ShowTabCommand ?? ShowTabCommand,
                CommandParameter = x,
                Image = x.Icon,
                SelectedImage = x.SelectedIcon,
                Opacity = x.SelectedIcon == null ? 0.5 : 1.0,
                Text = x.Title
            }).ToList();
            tabbar.Items = tabItems;
            tabbar.ImageButtonStyle = TabStyle;
            tabbar.ImageMargin = ImageMargin;
        }

        void UpdateActivePage()
        {
            if (SelectedIndex < 0) return;
            if (SelectedIndex >= Children.Count()) return;

            //Show
            Children[SelectedIndex].IsVisible = true;
            Children[SelectedIndex].InputTransparent = false;
            Children[SelectedIndex].Opacity = 1;

            //Hide (we do it after "show" to avoid flickering)
            for (int i = 0; i < Children.Count(); i++)
            {
                if (SelectedIndex != i)
                {
                    var view = Children[i];
                    if (view.Width <= 0)
                    {
                        view.InputTransparent = true;
                        view.Opacity = 0;
                        view.SizeChanged -= View_SizeChanged;
                        view.SizeChanged += View_SizeChanged;
                    }
                    else
                        view.IsVisible = false;
                }
            }
        }

        private void View_SizeChanged(object sender, EventArgs e)
        {
            UpdateActivePage();
        }

        readonly Command<AliveContentView> ShowTabCommand = new Command<AliveContentView>(view =>
        {

        });
    }
}