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
    public enum TabbedContentTransitions
    {
        None = 0,
        Crossfade,
        DipToBackground,
        Slide,
        SlideInverse,
        SlideLeft, //new from left
        SlideRight, //new from right
        SlideVertically,
        SlideInverseVertically,
        SlideUp, //new from up
        SlideDown, //new from down
        //Insert, // need to figure out how to do the z-order. maybe remove child and add again? or move through the list?
        //InsertLeft,
        //InsertRight,
        //InsertUp,
        //InsertDown
    }

    public enum TabBarPositions
    {
        Bottom = 0,
        Top,
        Left,
        Right
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    [ContentProperty("Children")]
    public partial class TabbedContentView
    {
        public TabbedContentView()
        {
            Children = new ObservableCollection<ContentView2>();
            InitializeComponent();
            UpdateTabBarBackgroundView();
            UpdateBackgroundView();
            UpdateTabBarPosition();
            UpdateChildren();
            UpdateTabBarSizeRequest();
            tabbar.SelectedIndex = SelectedTab;
            tabbarContainer.BackgroundColor = BackgroundColor;
            tabbar.PropertyChanged += TabBar_PropertyChanged;
            tabbar.TextColor = TextColor;
            tabbar.FontSize = FontSize;
            tabbar.FontAttributes = FontAttributes;
            tabbar.SelectedTextColor = SelectedTextColor;
            tabbar.SelectedFontSize = SelectedFontSize;
            tabbar.SelectedFontAttributes = SelectedFontAttributes;
            tabbar.FontFamily = FontFamily;
            tabbar.TextHeight = TextHeight;
            tabbar.Margin = TabBarPadding;
            tabbar.ItemOrientation = ItemOrientation;
            tabbarContainer.IsVisible = IsTabBarVisible;
            overlay.Content = Overlay;
        }

        public Color TabBarBackgroundColor
        {
            get => (Color)GetValue(TabBarBackgroundColorProperty);
            set => SetValue(TabBarBackgroundColorProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        [TypeConverter(typeof(FontSizeConverter))]
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

        [TypeConverter(typeof(FontSizeConverter))]
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

        public View TabBarBackground
        {
            get => (View)GetValue(TabBarBackgroundProperty);
            set => SetValue(TabBarBackgroundProperty, value);
        }

        public double TabBarSizeRequest
        {
            get => (double)GetValue(TabBarSizeRequestProperty);
            set => SetValue(TabBarSizeRequestProperty, value);
        }

        public new IList<ContentView2> Children
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

        public bool IsTabBarVisible
        {
            get => (bool)GetValue(IsTabBarVisibleProperty);
            set => SetValue(IsTabBarVisibleProperty, value);
        }

        public TabbedContentTransitions Transition
        {
            get => (TabbedContentTransitions)GetValue(TransitionProperty);
            set => SetValue(TransitionProperty, value);
        }

        public double TransitionDuration
        {
            get => (double)GetValue(TransitionDurationProperty);
            set => SetValue(TransitionDurationProperty, value);
        }

        public View BackgroundView
        {
            get => (View)GetValue(BackgroundViewProperty);
            set => SetValue(BackgroundViewProperty, value);
        }

        public TabBarPositions TabBarPosition
        {
            get => (TabBarPositions)GetValue(TabBarPositionProperty);
            set => SetValue(TabBarPositionProperty, value);
        }

        public bool FixTabBarPadding
        {
            get => (bool)GetValue(FixTabBarPaddingProperty);
            set => SetValue(FixTabBarPaddingProperty, value);
        }

        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }

        public double TabButtonsSpacing
        {
            get => (double)GetValue(TabButtonsSpacingProperty);
            set => SetValue(TabButtonsSpacingProperty, value);
        }

        public static readonly BindableProperty FixTabBarPaddingProperty = BindableProperty.Create(
            propertyName: nameof(FixTabBarPadding),
            returnType: typeof(bool),
            declaringType: typeof(TabbedContentView),
            defaultValue: true,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentView page)
                {
                    page.UpdateTabBarPosition();
                }
            });

        public static readonly BindableProperty TabBarPositionProperty = BindableProperty.Create(
            propertyName: nameof(TabBarPosition),
            returnType: typeof(TabBarPositions),
            declaringType: typeof(TabbedContentView),
            defaultValue: TabBarPositions.Bottom,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentView page)
                {
                    page.UpdateTabBarPosition();
                }
            });

        public static readonly BindableProperty BackgroundViewProperty = BindableProperty.Create(
            propertyName: nameof(BackgroundView),
            returnType: typeof(View),
            declaringType: typeof(TabbedContentView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentView page)
                {
                    page.UpdateBackgroundView();
                }
            });

        public static readonly BindableProperty TabBarBackgroundProperty =
            BindableProperty.Create(
            nameof(TabBarBackground),
            typeof(View),
            typeof(TabbedContentView),
            default(View),
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentView page)
                {
                    page.UpdateTabBarBackgroundView();
                }
            });

        public static readonly BindableProperty ChildrenProperty = BindableProperty.Create(
            propertyName: nameof(Children),
            returnType: typeof(IList<ContentView2>),
            declaringType: typeof(TabbedContentView),
            defaultValue: null,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentView page)
                {
                    if (oldVal is IList<ContentView2> views)
                    {
                        foreach (var item in views)
                            item.PropertyChanged -= page.Child_PropertyChanged;
                    }

                    if (newVal is ObservableCollection<ContentView2>)
                    {
                        (newVal as ObservableCollection<ContentView2>).CollectionChanged += page.TabbedContentView_CollectionChanged;
                    }
                    if (oldVal is ObservableCollection<ContentView2>)
                    {
                        (oldVal as ObservableCollection<ContentView2>).CollectionChanged -= page.TabbedContentView_CollectionChanged;
                    }
                    page.UpdateChildren();
                }
            });

        public static readonly BindableProperty SelectedTabProperty =
            BindableProperty.Create(
            nameof(SelectedTab),
            typeof(int),
            typeof(TabbedContentView),
            0,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (oldVal == newVal) return;
                if (bindable is TabbedContentView page)
                    page.SelectTab();
            });

        public static readonly BindableProperty SelectedIndexProperty =
            BindableProperty.Create(
            nameof(SelectedIndex),
            typeof(int),
            typeof(TabbedContentView),
            0,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (oldVal == newVal) return;
                if (bindable is TabbedContentView page)
                    page.UpdateActivePage();
            });

        private void TabbedContentView_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateChildren();
        }

        public ImageButtonOrientations ItemOrientation
        {
            get => (ImageButtonOrientations)GetValue(ItemOrientationProperty);
            set => SetValue(ItemOrientationProperty, value);
        }

        public static readonly BindableProperty ItemOrientationProperty = BindableProperty.Create(
            nameof(ItemOrientation),
            typeof(ImageButtonOrientations),
            typeof(TabbedContentView),
            ImageButtonOrientations.Up,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentView page)
                    page.tabbar.ItemOrientation = (ImageButtonOrientations)newVal;
            });

        public static readonly BindableProperty TabBarSizeRequestProperty =
            BindableProperty.Create(
            nameof(TabBarSizeRequest),
            typeof(double),
            typeof(TabbedContentView),
            70.0,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentView page)
                    page.UpdateTabBarSizeRequest();
            });
        
        public static readonly BindableProperty TabButtonsSpacingProperty =
            BindableProperty.Create(
            nameof(TabButtonsSpacing),
            typeof(double),
            typeof(TabbedContentView),
            0.0,
            BindingMode.OneWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentView page)
                    page.UpdateTabBarSizeRequest();
            });

        public static readonly BindableProperty TabBarPaddingProperty =
            BindableProperty.Create(
            nameof(TabBarPadding),
            typeof(Thickness),
            typeof(TabbedContentView),
            new Thickness(),
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentView page)
                    page.tabbar.Margin = page.TabBarPadding;
            });

        public static readonly BindableProperty TabBarBackgroundColorProperty =
            BindableProperty.Create(
            nameof(TabBarBackgroundColor),
            typeof(Color),
            typeof(TabbedContentView),
            Color.FromHex("#EEEEEE"),
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentView page)
                    page.tabbarContainer.BackgroundColor = (Color)newVal;
            });

        public static readonly BindableProperty IsTabBarVisibleProperty =
            BindableProperty.Create(
            nameof(IsTabBarVisible),
            typeof(bool),
            typeof(TabbedContentView),
            true,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentView page)
                    page.tabbarContainer.IsVisible = (bool)newVal;
            });

        public static readonly BindableProperty HasShadowProperty =
            BindableProperty.Create(
            nameof(HasShadow),
            typeof(bool),
            typeof(TabbedContentView),
            true,
            BindingMode.OneWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentView page)
                    page.UpdateShadow();
            });

        public static readonly BindableProperty TabStyleProperty =
            BindableProperty.Create(
                nameof(TabStyle),
                typeof(ImageButtonStyles),
                typeof(TabbedContentView),
                ImageButtonStyles.Image,
                BindingMode.TwoWay,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    if (bindable is TabbedContentView page)
                        page.UpdateTabs();
                });

        public static readonly BindableProperty ImageMarginProperty =
            BindableProperty.Create(
                nameof(ImageMargin),
                typeof(Thickness),
                typeof(TabbedContentView),
                new Thickness(8),
                BindingMode.TwoWay,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    if (bindable is TabbedContentView page)
                        if (page.tabbar != null)
                            page.tabbar.ImageMargin = (Thickness)newVal;
                });

        public static readonly BindableProperty OverlayProperty = BindableProperty.Create(
            propertyName: nameof(Overlay),
            returnType: typeof(View),
            declaringType: typeof(TabbedContentView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentView page)
                {
                    page.overlay.Content = (View)newVal;
                }
            });

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            propertyName: nameof(TextColor),
            returnType: typeof(Color),
            declaringType: typeof(TabbedContentView),
            defaultValue: Color.Black,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentView page)
                    page.tabbar.TextColor = page.TextColor;
            });

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
            propertyName: nameof(FontSize),
            returnType: typeof(double),
            declaringType: typeof(TabbedContentView),
            defaultValue: 16.0,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentView page)
                    page.tabbar.FontSize = page.FontSize;
            });

        public static readonly BindableProperty SelectedTextColorProperty = BindableProperty.Create(
            propertyName: nameof(SelectedTextColor),
            returnType: typeof(Color),
            declaringType: typeof(TabbedContentView),
            defaultValue: Color.Default,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentView page)
                    page.tabbar.SelectedTextColor = page.SelectedTextColor;
            });

        public static readonly BindableProperty SelectedFontSizeProperty = BindableProperty.Create(
            propertyName: nameof(SelectedFontSize),
            returnType: typeof(double?),
            declaringType: typeof(TabbedContentView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentView page)
                    page.tabbar.SelectedFontSize = page.SelectedFontSize;
            });

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
            propertyName: nameof(FontFamily),
            returnType: typeof(string),
            declaringType: typeof(TabbedContentView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentView page)
                    page.tabbar.FontFamily = page.FontFamily;
            });

        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(
            propertyName: nameof(FontAttributes),
            returnType: typeof(FontAttributes),
            declaringType: typeof(TabbedContentView),
            defaultValue: FontAttributes.Bold,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentView page)
                    page.tabbar.FontAttributes = page.FontAttributes;
            });

        public static readonly BindableProperty SelectedFontAttributesProperty = BindableProperty.Create(
            propertyName: nameof(SelectedFontAttributes),
            returnType: typeof(FontAttributes?),
            declaringType: typeof(TabbedContentView),
            defaultValue: FontAttributes.Bold,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentView page)
                    page.tabbar.SelectedFontAttributes = page.SelectedFontAttributes;
            });

        public static readonly BindableProperty TextHeightProperty = BindableProperty.Create(
            nameof(TextHeight),
            typeof(GridLength),
            typeof(TabbedContentView),
            GridLength.Auto,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentView page)
                    page.tabbar.TextHeight = page.TextHeight;
            });

        public static readonly BindableProperty TransitionProperty = BindableProperty.Create(
            nameof(Transition),
            typeof(TabbedContentTransitions),
            typeof(TabbedContentView),
            TabbedContentTransitions.None,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentView page) { }
            });

        public static readonly BindableProperty TransitionDurationProperty = BindableProperty.Create(
            nameof(TransitionDuration),
            typeof(double),
            typeof(TabbedContentView),
            250.0,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentView page) { }
            });

        void UpdateTabBarBackgroundView()
        {
            if (tabbarBackground.Content != TabBarBackground)
                tabbarBackground.Content = TabBarBackground;
        }

        void UpdateBackgroundView()
        {
            if (body.Content != BackgroundView)
                body.Content = BackgroundView;
        }

        void UpdateTabBarSizeRequest()
        {
            if (tabbar == null) return;
            tabbar.HorizontalSpacing = TabButtonsSpacing;

            if (TabBarPosition == TabBarPositions.Left || TabBarPosition == TabBarPositions.Right)
            {
                tabbar.HeightRequest = -1;
                tabbar.WidthRequest = TabBarSizeRequest;
            }
            else
            {
                tabbar.HeightRequest = TabBarSizeRequest;
                tabbar.WidthRequest = -1;
            }
        }

        void UpdateShadow()
        {
            if (shadow == null) return;

            shadowv.IsVisible = HasShadow && TabBarPosition == TabBarPositions.Right;
            shadowv2.IsVisible = HasShadow && TabBarPosition == TabBarPositions.Left;
            shadow.IsVisible = HasShadow && TabBarPosition == TabBarPositions.Bottom;
            shadow2.IsVisible = HasShadow && TabBarPosition == TabBarPositions.Top;
        }

        void UpdateTabBarPosition()
        {
            if (tabbar == null) return;

            UpdateTabBarSizeRequest();
            if (TabBarPosition == TabBarPositions.Left || TabBarPosition == TabBarPositions.Right)
            {
                // Horizontal
                tabbar.FixTopPadding = false;
                tabbar.FixBottomPadding = false;
                var col = TabBarPosition == TabBarPositions.Left ? 0 : 2;
                tabbarContainer.SetValue(Grid.RowProperty, 1);
                tabbarContainer.SetValue(Grid.ColumnProperty, col);
                tabbarContainer.HorizontalOptions = LayoutOptions.FillAndExpand;
                tabbarContainer.VerticalOptions = LayoutOptions.Fill;
                tabbar.Orientation = StackOrientation.Vertical;
            }
            else
            {
                // Vertical
                var row = TabBarPosition == TabBarPositions.Bottom ? 2 : 0;
                tabbarContainer.SetValue(Grid.RowProperty, row);
                tabbarContainer.SetValue(Grid.ColumnProperty, 1);
                if (FixTabBarPadding)
                {
                    if (TabBarPosition == TabBarPositions.Bottom)
                    {
                        tabbar.FixTopPadding = false;
                        tabbar.FixBottomPadding = true;
                    }
                    else
                    {
                        tabbar.FixTopPadding = true;
                        tabbar.FixBottomPadding = false;
                    }
                }
                else
                {
                    tabbar.FixTopPadding = false;
                    tabbar.FixBottomPadding = false;
                }

                tabbarContainer.HorizontalOptions = LayoutOptions.Fill;
                tabbarContainer.VerticalOptions = LayoutOptions.FillAndExpand;
                tabbar.Orientation = StackOrientation.Horizontal;
            }

            UpdateShadow();
        }

        private void TabBar_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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

        volatile bool isAnimating = false;
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
            if (Transition == TabbedContentTransitions.None || isAnimating)
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

            var transitions = new Dictionary<TabbedContentTransitions, Action<View, View>>
            {
                { TabbedContentTransitions.Crossfade, StartCrossFadeTransition },
                { TabbedContentTransitions.DipToBackground, StartDipTransition  },
                { TabbedContentTransitions.Slide, StartSlideTransition },
                { TabbedContentTransitions.SlideInverse, StartSlideInverseTransition },
                { TabbedContentTransitions.SlideLeft, StartSlideLeftTransition },
                { TabbedContentTransitions.SlideRight, StartSlideRightTransition },
                { TabbedContentTransitions.SlideVertically, StartSlideVerticallyTransition },
                { TabbedContentTransitions.SlideInverseVertically, StartSlideInverseVerticallyTransition },
                { TabbedContentTransitions.SlideUp, StartSlideUpTransition },
                { TabbedContentTransitions.SlideDown, StartSlideDownTransition },
            };

            transitions[Transition](oldChild, newChild);
        }

        void UpdateActivePage_NoTransition()
        {
            //Show
            ViewExtensions.CancelAnimations(Children[SelectedIndex]);
            Children[SelectedIndex].IsVisible = true;
            Children[SelectedIndex].InputTransparent = false;
            Children[SelectedIndex].Opacity = 1;
            Children[SelectedIndex].TranslationX = 0;
            Children[SelectedIndex].TranslationY = 0;

            //Hide (we do it after "show" to avoid flickering)
            for (int i = 0; i < Children.Count(); i++)
            {
                if (SelectedIndex != i)
                {
                    ViewExtensions.CancelAnimations(Children[i]);
                    var view = Children[i];
                    view.InputTransparent = true;
                    view.CascadeInputTransparent = true;
                    view.Opacity = 0;
                    if (view.Width <= 0)
                    {
                        view.SizeChanged -= View_SizeChanged;
                        view.SizeChanged += View_SizeChanged;
                    }
                    else
                    {
                        view.IsVisible = false;
                    }
                }
            }

            isAnimating = false;
        }

        async void StartCrossFadeTransition(View oldChild, View newChild)
        {
            ViewExtensions.CancelAnimations(oldChild);
            ViewExtensions.CancelAnimations(newChild);

            isAnimating = true;
            oldChild.TranslationX = oldChild.TranslationY = 0;
            newChild.TranslationX = newChild.TranslationY = 0;

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

            isAnimating = true;
            oldChild.TranslationX = oldChild.TranslationY = 0;
            newChild.TranslationX = newChild.TranslationY = 0;

            newChild.Opacity = 0.01;
            newChild.IsVisible = true;

            uint halfLife = (uint)(TransitionDuration * 0.5);
            await oldChild.FadeTo(0.0, halfLife);
            await newChild.FadeTo(1.0, halfLife);
            ShowActivePageOnly();
        }

        bool GetDirection(View oldChild, View newChild)
        {
            var oI = Children.IndexOf((ContentView2)oldChild);
            return oI < SelectedIndex;
        }

        void StartSlideTransition(View oldChild, View newChild)
        {
            StartSlideTransition(oldChild, newChild, !GetDirection(oldChild, newChild));
        }

        void StartSlideInverseTransition(View oldChild, View newChild)
        {
            StartSlideTransition(oldChild, newChild, GetDirection(oldChild, newChild));
        }

        void StartSlideLeftTransition(View oldChild, View newChild)
        {
            StartSlideTransition(oldChild, newChild, true);
        }

        void StartSlideRightTransition(View oldChild, View newChild)
        {
            StartSlideTransition(oldChild, newChild, false);
        }
        void StartSlideVerticallyTransition(View oldChild, View newChild)
        {
            StartSlideVerticallyTransition(oldChild, newChild, GetDirection(oldChild, newChild));
        }

        void StartSlideInverseVerticallyTransition(View oldChild, View newChild)
        {
            StartSlideVerticallyTransition(oldChild, newChild, !GetDirection(oldChild, newChild));
        }

        void StartSlideUpTransition(View oldChild, View newChild)
        {
            StartSlideVerticallyTransition(oldChild, newChild, false);
        }

        void StartSlideDownTransition(View oldChild, View newChild)
        {
            StartSlideVerticallyTransition(oldChild, newChild, true);
        }

        async void StartSlideTransition(View oldChild, View newChild, bool left)
        {
            ViewExtensions.CancelAnimations(oldChild);
            ViewExtensions.CancelAnimations(newChild);

            isAnimating = true;
            var oT = -oldChild.Width;
            if (left)
            {
                newChild.TranslationX = -oldChild.Width;
                oT *= -1;
            }
            else newChild.TranslationX = oldChild.Width;

            newChild.TranslationY = 0;
            oldChild.TranslationX = 0;

            newChild.Opacity = 1.0;
            newChild.IsVisible = true;

            await Task.WhenAll(
                newChild.TranslateTo(0, 0, (uint)TransitionDuration),
                oldChild.TranslateTo(oT, 0, (uint)TransitionDuration));

            ShowActivePageOnly();
        }

        async void StartSlideVerticallyTransition(View oldChild, View newChild, bool down)
        {
            ViewExtensions.CancelAnimations(oldChild);
            ViewExtensions.CancelAnimations(newChild);

            isAnimating = true;
            var oT = oldChild.Height;
            if (down)
            {
                newChild.TranslationY = oldChild.Height;
                oT *= -1;
            }
            else newChild.TranslationY = -oldChild.Height;

            newChild.TranslationX = 0;
            oldChild.TranslationY = 0;

            newChild.Opacity = 1.0;
            newChild.IsVisible = true;

            await Task.WhenAll(
                newChild.TranslateTo(0, 0, (uint)TransitionDuration),
                oldChild.TranslateTo(0, oT, (uint)TransitionDuration));

            ShowActivePageOnly();
        }

        void ShowActivePageOnly()
        {
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].IsVisible = SelectedIndex == i;
                Children[i].InputTransparent = !Children[i].IsVisible;
            }
            isAnimating = false;
        }

        private void View_SizeChanged(object sender, EventArgs e)
        {
            UpdateActivePage();
        }

        new readonly Command<ContentView2> ShowTabCommand = new Command<ContentView2>(view =>
        {

        });
    }
}