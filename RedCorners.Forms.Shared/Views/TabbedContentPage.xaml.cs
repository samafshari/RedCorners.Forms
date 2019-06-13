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
    public enum TabbedPageTransitions
    {
        None = 0,
        Crossfade,
        DipToBackground,
        //Slide,
        //SlideLeft,
        //SlideRight,
        //SlideUp,
        //SlideDown,
        //Insert,
        //InsertLeft,
        //InsertRight,
        //InsertUp,
        //InsertDown
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    [ContentProperty("Children")]
    public partial class TabbedContentPage
    {
        public TabbedContentPage()
        {
            Children = new ObservableCollection<ContentView2>();
            InitializeComponent();
            UpdateTabbarBackgroundView();
            UpdateBackgroundView();
            UpdateChildren();
            tabbar.SelectedIndex = SelectedTab;
            tabbarContainer.BackgroundColor = BackgroundColor;
            tabbar.PropertyChanged += Tabbar_PropertyChanged;
            tabbar.HeightRequest = TabbarHeightRequest;
            tabbar.TextColor = TextColor;
            tabbar.FontSize = FontSize;
            tabbar.FontAttributes = FontAttributes;
            tabbar.SelectedTextColor = SelectedTextColor;
            tabbar.SelectedFontSize = SelectedFontSize;
            tabbar.SelectedFontAttributes = SelectedFontAttributes;
            tabbar.FontFamily = FontFamily;
            tabbar.TextHeight = TextHeight;
            tabbar.Margin = TabBarPadding;
            tabbarContainer.IsVisible = IsTabbarVisible;
            overlay.Content = Overlay;
        }

        public Color TabbarBackgroundColor
        {
            get => (Color)GetValue(TabbarBackgroundColorProperty);
            set => SetValue(TabbarBackgroundColorProperty, value);
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

        public View TabbarBackground
        {
            get => (View)GetValue(TabbarBackgroundProperty);
            set => SetValue(TabbarBackgroundProperty, value);
        }

        public double TabbarHeightRequest
        {
            get => (double)GetValue(TabbarHeightRequestProperty);
            set => SetValue(TabbarHeightRequestProperty, value);
        }

        public IList<ContentView2> Children
        {
            get => (IList<ContentView2>)GetValue(ChildrenProperty);
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

        public Thickness TabBarPadding
        {
            get => (Thickness)GetValue(TabBarPaddingProperty);
            set => SetValue(TabBarPaddingProperty, value);
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

        public TabbedPageTransitions Transition
        {
            get => (TabbedPageTransitions)GetValue(TransitionProperty);
            set => SetValue(TransitionProperty, value);
        }

        public double TransitionDuration
        {
            get => (double)GetValue(TransitionDurationProperty);
            set => SetValue(TransitionDurationProperty, value);
        }

        public View Background
        {
            get => (View)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        public static readonly BindableProperty BackgroundProperty = BindableProperty.Create(
            propertyName: nameof(Background),
            returnType: typeof(View),
            declaringType: typeof(TabbedContentPage),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentPage page)
                {
                    page.UpdateBackgroundView();
                }
            });

        public static readonly BindableProperty TabbarBackgroundProperty =
            BindableProperty.Create(
            nameof(TabbarBackground),
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
            returnType: typeof(IList<ContentView2>),
            declaringType: typeof(TabbedContentPage),
            defaultValue: null,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentPage page)
                {
                    if (oldVal is IList<ContentView2> views)
                    {
                        foreach (var item in views)
                            item.PropertyChanged -= page.Child_PropertyChanged;
                    }

                    if (newVal is ObservableCollection<ContentView2>)
                    {
                        (newVal as ObservableCollection<ContentView2>).CollectionChanged += page.TabbedContentPage_CollectionChanged;
                    }
                    if (oldVal is ObservableCollection<ContentView2>)
                    {
                        (oldVal as ObservableCollection<ContentView2>).CollectionChanged -= page.TabbedContentPage_CollectionChanged;
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
                if (oldVal == newVal) return;
                if (bindable is TabbedContentPage page)
                    page.SelectTab();
                Console.WriteLine($"newVal: {newVal}");
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
                if (oldVal == newVal) return;
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

        public static readonly BindableProperty TabBarPaddingProperty =
            BindableProperty.Create(
            nameof(TabBarPadding),
            typeof(Thickness),
            typeof(TabbedContentPage),
            new Thickness(),
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentPage page)
                    page.tabbar.Margin = page.TabBarPadding;
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

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            propertyName: nameof(TextColor),
            returnType: typeof(Color),
            declaringType: typeof(TabbedContentPage),
            defaultValue: Color.Black,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentPage page)
                    page.tabbar.TextColor = page.TextColor;
            });

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
            propertyName: nameof(FontSize),
            returnType: typeof(double),
            declaringType: typeof(TabbedContentPage),
            defaultValue: 16.0,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentPage page)
                    page.tabbar.FontSize = page.FontSize;
            });

        public static readonly BindableProperty SelectedTextColorProperty = BindableProperty.Create(
            propertyName: nameof(SelectedTextColor),
            returnType: typeof(Color),
            declaringType: typeof(TabbedContentPage),
            defaultValue: Color.Default,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentPage page)
                    page.tabbar.SelectedTextColor = page.SelectedTextColor;
            });

        public static readonly BindableProperty SelectedFontSizeProperty = BindableProperty.Create(
            propertyName: nameof(SelectedFontSize),
            returnType: typeof(double?),
            declaringType: typeof(TabbedContentPage),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentPage page)
                    page.tabbar.SelectedFontSize = page.SelectedFontSize;
            });

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
            propertyName: nameof(FontFamily),
            returnType: typeof(string),
            declaringType: typeof(TabbedContentPage),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentPage page)
                    page.tabbar.FontFamily = page.FontFamily;
            });

        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(
            propertyName: nameof(FontAttributes),
            returnType: typeof(FontAttributes),
            declaringType: typeof(TabbedContentPage),
            defaultValue: FontAttributes.Bold,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentPage page)
                    page.tabbar.FontAttributes = page.FontAttributes;
            });

        public static readonly BindableProperty SelectedFontAttributesProperty = BindableProperty.Create(
            propertyName: nameof(SelectedFontAttributes),
            returnType: typeof(FontAttributes?),
            declaringType: typeof(TabbedContentPage),
            defaultValue: FontAttributes.Bold,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentPage page)
                    page.tabbar.SelectedFontAttributes = page.SelectedFontAttributes;
            });

        public static readonly BindableProperty TextHeightProperty = BindableProperty.Create(
            nameof(TextHeight),
            typeof(GridLength),
            typeof(TabbedContentPage),
            GridLength.Auto,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentPage page)
                    page.tabbar.TextHeight = page.TextHeight;
            });

        public static readonly BindableProperty TransitionProperty = BindableProperty.Create(
            nameof(Transition),
            typeof(TabbedPageTransitions),
            typeof(TabbedContentPage),
            TabbedPageTransitions.None,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentPage page) { }
            });

        public static readonly BindableProperty TransitionDurationProperty = BindableProperty.Create(
            nameof(TransitionDuration),
            typeof(double),
            typeof(TabbedContentPage),
            250.0,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentPage page) { }
            });

        void UpdateTabbarBackgroundView()
        {
            if (tabbarBackground.Content != TabbarBackground)
                tabbarBackground.Content = TabbarBackground;
        }

        void UpdateBackgroundView()
        {
            if (body.Content != Background)
                body.Content = Background;
        }

        private void Tabbar_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TabBar.SelectedIndex))
            {
                SelectedTab = tabbar.SelectedIndex;
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

            UpdateActivePage();
        }

        private void Child_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var monitoredNames = new[]
            {
                nameof(ContentView2.Title),
                nameof(ContentView2.Icon),
                nameof(ContentView2.ShowTabCommand),
                nameof(ContentView2.SelectedIcon),
                nameof(ContentView2.IsVisibleAsTab)
            };

            if (!monitoredNames.Contains(e.PropertyName))
                return;

            UpdateTabs();

            if (e.PropertyName == nameof(ContentView2.IsVisibleAsTab))
            {
                UpdateActivePage();
            }
        }

        void UpdateTabs()
        {
            if (tabbar == null) return;
            var tabbedPages = Children.Where(x => x.IsVisibleAsTab).ToList();
            var tabItems = tabbedPages.Select(x => new TabBarItem
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

            int tabId = 0;
            for (int i = 0; i < Children.Count; i++)
            {
                if (SelectedIndex == i)
                {
                    if (Children[i].IsVisibleAsTab)
                    {
                        if (SelectedTab != tabId)
                            tabbar.SelectedIndex = tabId;
                    }
                    else
                    {
                        if (SelectedTab != -1)
                            tabbar.SelectedIndex = -1;
                    }
                    break;
                }
                if (Children[i].IsVisibleAsTab)
                    tabId++;
            }

            // Prepare for transition
            if (Transition == TabbedPageTransitions.None)
            {
                UpdateActivePage_NoTransition();
                return;
            }

            var oldChild = Children.FirstOrDefault(x => x.IsVisible);
            var newChild = Children[SelectedIndex];

            if (oldChild == newChild || oldChild == null)
            {
                ShowActivePageOnly();
                return;
            }
            
            oldChild.InputTransparent = false;
            newChild.InputTransparent = true;
            oldChild.CascadeInputTransparent = true;
            newChild.CascadeInputTransparent = true;

            var transitions = new Dictionary<TabbedPageTransitions, Action<View, View>>
            {
                { TabbedPageTransitions.Crossfade, StartCrossFadeTransition },
                { TabbedPageTransitions.DipToBackground, StartDipTransition  }
            };

            transitions[Transition](oldChild, newChild);
        }

        void UpdateActivePage_NoTransition()
        {
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

        async void StartCrossFadeTransition(View oldChild, View newChild)
        {
            ViewExtensions.CancelAnimations(oldChild);
            ViewExtensions.CancelAnimations(newChild);
            newChild.Opacity = 0.01;
            newChild.IsVisible = true;
            
            await Task.WhenAll(
                newChild.FadeTo(1.0, (uint)TransitionDuration),
                oldChild.FadeTo(0.0, (uint)TransitionDuration));

            ShowActivePageOnly();
        }

        async void StartDipTransition(View oldChild, View newChild)
        {
            ViewExtensions.CancelAnimations(oldChild);
            ViewExtensions.CancelAnimations(newChild);

            newChild.Opacity = 0.01;
            newChild.IsVisible = true;

            uint halfLife = (uint)(TransitionDuration * 0.5);
            await oldChild.FadeTo(0.0, halfLife);
            await newChild.FadeTo(1.0, halfLife);
            ShowActivePageOnly();
        }

        void ShowActivePageOnly()
        {
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].IsVisible = SelectedIndex == i;
                Children[i].InputTransparent = !Children[i].InputTransparent;
            }
        }

        private void View_SizeChanged(object sender, EventArgs e)
        {
            UpdateActivePage();
        }

        readonly Command<ContentView2> ShowTabCommand = new Command<ContentView2>(view =>
        {

        });
    }
}