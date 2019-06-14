using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedCorners.Forms
{
    public enum SideBarSides
    {
        Left,
        Right,
        Top,
        Bottom
    }

    [ContentProperty("Body")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SideBar 
    {
        public View Body
        {
            get => (View)GetValue(BodyProperty);
            set => SetValue(BodyProperty, value);
        }

        public SideBarSides Side
        {
            get => (SideBarSides)GetValue(SideProperty);
            set => SetValue(SideProperty, value);
        }

        public GridLength ContentSize
        {
            get => (GridLength)GetValue(ContentSizeProperty);
            set => SetValue(ContentSizeProperty, value);
        }

        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public uint FadeInDuration
        {
            get => (uint)GetValue(FadeInDurationProperty);
            set => SetValue(FadeInDurationProperty, value);
        }

        public bool DoesFadeIn
        {
            get => (bool)GetValue(DoesFadeInProperty);
            set => SetValue(DoesFadeInProperty, value);
        }

        public uint SlideInDuration
        {
            get => (uint)GetValue(SlideInDurationProperty);
            set => SetValue(SlideInDurationProperty, value);
        }

        public bool DoesSlideIn
        {
            get => (bool)GetValue(DoesSlideInProperty);
            set => SetValue(DoesSlideInProperty, value);
        }

        public uint FadeOutDuration
        {
            get => (uint)GetValue(FadeOutDurationProperty);
            set => SetValue(FadeOutDurationProperty, value);
        }

        public bool DoesFadeOut
        {
            get => (bool)GetValue(DoesFadeOutProperty);
            set => SetValue(DoesFadeOutProperty, value);
        }

        public uint SlideOutDuration
        {
            get => (uint)GetValue(SlideOutDurationProperty);
            set => SetValue(SlideOutDurationProperty, value);
        }

        public bool DoesSlideOut
        {
            get => (bool)GetValue(DoesSlideOutProperty); 
            set => SetValue(DoesSlideOutProperty, value);
        }

        public bool IsSwipeEnabled
        {
            get => (bool)GetValue(IsSwipeEnabledProperty);
            set => SetValue(IsSwipeEnabledProperty, value);
        }

        public bool IsFullSize
        {
            get => (bool)GetValue(IsFullSizeProperty);
            set => SetValue(IsFullSizeProperty, value);
        }

        public new bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }

        public static readonly BindableProperty BodyProperty = BindableProperty.Create(
            propertyName: nameof(Body),
            returnType: typeof(View),
            declaringType: typeof(SideBar),
            defaultValue: new Grid(),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                (bindable as SideBar)?.UpdateLayout();
            });

        public static readonly BindableProperty SideProperty = BindableProperty.Create(
            propertyName: nameof(Side),
            returnType: typeof(SideBarSides),
            declaringType: typeof(SideBar),
            defaultValue: SideBarSides.Left,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                (bindable as SideBar)?.UpdateLayout();
            });

        public static readonly BindableProperty ContentSizeProperty = BindableProperty.Create(
            propertyName: nameof(ContentSize),
            returnType: typeof(GridLength),
            declaringType: typeof(SideBar),
            defaultValue: new GridLength(3, GridUnitType.Star),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                (bindable as SideBar)?.UpdateLayout();
            });

        public static readonly BindableProperty FadeInDurationProperty = BindableProperty.Create(
            propertyName: nameof(FadeInDuration),
            returnType: typeof(uint),
            declaringType: typeof(SideBar),
            defaultValue: (uint)150,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
            });

        public static readonly BindableProperty DoesFadeInProperty = BindableProperty.Create(
            propertyName: nameof(DoesFadeIn),
            returnType: typeof(bool),
            declaringType: typeof(SideBar),
            defaultValue: true,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
            });

        public static readonly BindableProperty SlideInDurationProperty = BindableProperty.Create(
            propertyName: nameof(SlideInDuration),
            returnType: typeof(uint),
            declaringType: typeof(SideBar),
            defaultValue: (uint)350,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
            });

        public static readonly BindableProperty DoesSlideInProperty = BindableProperty.Create(
            propertyName: nameof(DoesSlideIn),
            returnType: typeof(bool),
            declaringType: typeof(SideBar),
            defaultValue: true,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
            });

        public static readonly BindableProperty FadeOutDurationProperty = BindableProperty.Create(
            propertyName: nameof(FadeOutDuration),
            returnType: typeof(uint),
            declaringType: typeof(SideBar),
            defaultValue: (uint)150,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
            });

        public static readonly BindableProperty DoesFadeOutProperty = BindableProperty.Create(
            propertyName: nameof(DoesFadeOut),
            returnType: typeof(bool),
            declaringType: typeof(SideBar),
            defaultValue: true,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
            });

        public static readonly BindableProperty SlideOutDurationProperty = BindableProperty.Create(
            propertyName: nameof(SlideInDuration),
            returnType: typeof(uint),
            declaringType: typeof(SideBar),
            defaultValue: (uint)300,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
            });

        public static readonly BindableProperty DoesSlideOutProperty = BindableProperty.Create(
            propertyName: nameof(DoesSlideIn),
            returnType: typeof(bool),
            declaringType: typeof(SideBar),
            defaultValue: true,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
            });

        public static readonly BindableProperty IsSwipeEnabledProperty = BindableProperty.Create(
            propertyName: nameof(IsSwipeEnabled),
            returnType: typeof(bool),
            declaringType: typeof(SideBar),
            defaultValue: true,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
            });

        public static new readonly BindableProperty IsVisibleProperty = BindableProperty.Create(
            propertyName: nameof(IsVisible),
            returnType: typeof(bool),
            declaringType: typeof(SideBar),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                (bindable as SideBar)?.UpdateVisibility((bool)oldVal, (bool)newVal);
            });

        public static readonly BindableProperty IsFullSizeProperty = BindableProperty.Create(
            propertyName: nameof(IsFullSize),
            returnType: typeof(bool),
            declaringType: typeof(SideBar),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                (bindable as SideBar)?.UpdateLayout();
            });

        SwipeGestureRecognizer swipeRight = null, swipeLeft = null, swipeUp = null, swipeDown = null;
        SwipeGestureRecognizer swipeRightIn = null, swipeLeftIn = null, swipeUpIn = null, swipeDownIn = null;

        public SideBar()
        {
            CascadeInputTransparent = true;

            InitializeComponent();
            SetVisibility(false);
            UpdateLayout();

            var tap = new TapGestureRecognizer();
            tap.Tapped += BtnDismiss_Clicked;
            btnDismiss.GestureRecognizers.Add(tap);
        }

        void SetVisibility(bool value)
        {
            SetValue(VisualElement.IsVisibleProperty, value);
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            if (Parent is View view && swipeRight == null)
            {
                while (true)
                {
                    if (view.InputTransparent && view.Parent != null && view.Parent is View)
                        view = view.Parent as View;
                    else break;
                }

                swipeRight = new SwipeGestureRecognizer();
                swipeRight.Swiped += Swipe_Swiped;
                swipeRight.Direction = SwipeDirection.Right;
                view.GestureRecognizers.Add(swipeRight);

                swipeLeft = new SwipeGestureRecognizer();
                swipeLeft.Swiped += Swipe_Swiped;
                swipeLeft.Direction = SwipeDirection.Left;
                view.GestureRecognizers.Add(swipeLeft);

                swipeUp = new SwipeGestureRecognizer();
                swipeUp.Swiped += Swipe_Swiped;
                swipeUp.Direction = SwipeDirection.Up;
                view.GestureRecognizers.Add(swipeUp);

                swipeDown = new SwipeGestureRecognizer();
                swipeDown.Swiped += Swipe_Swiped;
                swipeDown.Direction = SwipeDirection.Down;
                view.GestureRecognizers.Add(swipeDown);

                // IN
                swipeRightIn = new SwipeGestureRecognizer();
                swipeRightIn.Swiped += SwipeIn_Swiped;
                swipeRightIn.Direction = SwipeDirection.Right;
                GestureRecognizers.Add(swipeRightIn);

                swipeLeftIn = new SwipeGestureRecognizer();
                swipeLeftIn.Swiped += SwipeIn_Swiped;
                swipeLeftIn.Direction = SwipeDirection.Left;
                GestureRecognizers.Add(swipeLeftIn);

                swipeUpIn = new SwipeGestureRecognizer();
                swipeUpIn.Swiped += SwipeIn_Swiped;
                swipeUpIn.Direction = SwipeDirection.Up;
                GestureRecognizers.Add(swipeUpIn);

                swipeDownIn = new SwipeGestureRecognizer();
                swipeDownIn.Swiped += SwipeIn_Swiped;
                swipeDownIn.Direction = SwipeDirection.Down;
                GestureRecognizers.Add(swipeDownIn);
            }

            UpdateLayout();
        }

        private void Swipe_Swiped(object sender, SwipedEventArgs e)
        {
            if (!IsSwipeEnabled) return;
            if (IsVisible) return;

            if ((Side == SideBarSides.Right && e.Direction == SwipeDirection.Left) ||
                (Side == SideBarSides.Left && e.Direction == SwipeDirection.Right) ||
                (Side == SideBarSides.Top && e.Direction == SwipeDirection.Down) ||
                (Side == SideBarSides.Bottom && e.Direction == SwipeDirection.Up))
                IsVisible = true;
        }

        private void SwipeIn_Swiped(object sender, SwipedEventArgs e)
        {
            if (!IsSwipeEnabled) return;
            if (!IsVisible) return;

            if ((Side == SideBarSides.Right && e.Direction == SwipeDirection.Right) ||
                (Side == SideBarSides.Left && e.Direction == SwipeDirection.Left) ||
                (Side == SideBarSides.Top && e.Direction == SwipeDirection.Up) ||
                (Side == SideBarSides.Bottom && e.Direction == SwipeDirection.Down))
                SetValue(IsVisibleProperty, false);
        }

        private void BtnDismiss_Clicked(object sender, EventArgs e)
        {
            SetValue(IsVisibleProperty, false);
        }

        void UpdateLayout()
        {
            var contentSide = IsFullSize ? GridLength.Star : ContentSize;
            var otherSide = IsFullSize ? new GridLength(0) : GridLength.Star;

            if (Side == SideBarSides.Right || Side == SideBarSides.Left)
            {
                Grid.SetRow(content, 0);
                Grid.SetRowSpan(content, 2);
                Grid.SetColumnSpan(content, 1);

                if (Side == SideBarSides.Left)
                {
                    colLeft.Width = contentSide;
                    colRight.Width = otherSide;

                    Grid.SetColumn(content, 0);
                }
                else
                {
                    colLeft.Width = otherSide;
                    colRight.Width = contentSide;

                    Grid.SetColumn(content, 1);
                }
            }
            else
            {
                Grid.SetColumn(content, 0);
                Grid.SetColumnSpan(content, 2);
                Grid.SetRowSpan(content, 1);

                if (Side == SideBarSides.Top)
                {
                    rowTop.Height = contentSide;
                    rowBottom.Height = otherSide;

                    Grid.SetRow(content, 0);
                }
                else
                {
                    rowTop.Height = otherSide;
                    rowBottom.Height = contentSide;

                    Grid.SetRow(content, 1);
                }
            }

            content.Content = Body;
        }

        protected override void InvalidateLayout()
        {
            mainGrid.Children.Remove(content);
            UpdateLayout();
            mainGrid.Children.Add(content);
            UpdateChildrenLayout();
        }

        volatile bool isOpening = false;
        volatile bool isClosing = false;


        async void UpdateVisibility(bool oldVal, bool newVal)
        {
            InputTransparent = !newVal;
            // If value is not updated, do nothing.
            if (oldVal == newVal)
            {
                return;
            }

            // If in the middle of another update, force roll back the value change.
            if (isOpening || isClosing)
            {
                //IsVisible = oldVal;
                //return;
            }

            ViewExtensions.CancelAnimations(content);

            // It's an open sequence
            if (newVal)
            {
                SetVisibility(true);
                var hasOpenAnimation = DoesSlideIn || DoesFadeIn;
                if (!hasOpenAnimation)
                {
                    Opacity = 1.0;
                    TranslationX = 0;
                    TranslationY = 0;
                    return;
                }

                isOpening = true;
                await Task.WhenAll(OpenSlideAsync(), FadeInAsync());
                isOpening = false;
            }
            else
            {
                // It's a close sequence
                var hasCloseAnimation = DoesSlideOut || DoesFadeOut;
                if (!hasCloseAnimation)
                {
                    SetVisibility(false);
                    return;
                }

                isClosing = true;
                await Task.WhenAll(CloseSlideAsync(), FadeOutAsync());
                isClosing = false;

                SetVisibility(false);
            }
        }

        (double x, double y) GetSlideTranslations()
        {
            var baseView = (Parent as View) ?? this;
            var h = (Side == SideBarSides.Left || Side == SideBarSides.Right);
            var baseSize = h ? baseView.Width : baseView.Height;
            if (Width > 0) baseSize = h ? Width : Height;
            double translation;
            if (h)
            {
                if (IsFullSize) translation = baseSize;
                else if (ContentSize.IsAuto) translation = content.Width;
                else if (ContentSize.IsStar) translation = baseSize / ContentSize.Value;
                else translation = ContentSize.Value;
                if (Side == SideBarSides.Left) translation *= -1;
                return (translation, 0);
            }
            else
            {
                if (IsFullSize) translation = baseSize;
                else if (ContentSize.IsAuto) translation = content.Height;
                else if (ContentSize.IsStar) translation = baseSize / ContentSize.Value;
                else translation = ContentSize.Value;
                if (Side == SideBarSides.Top) translation *= -1;
                return (0, translation);
            }
        }

        async Task OpenSlideAsync()
        {
            if (!DoesSlideIn)
            {
                content.TranslationX = 0;
                content.TranslationY = 0;
                return;
            }

            (content.TranslationX, content.TranslationY) = GetSlideTranslations();
            
            await content.TranslateTo(0, 0, SlideInDuration);
        }

        async Task CloseSlideAsync()
        {
            content.TranslationX = 0;
            content.TranslationY = 0;

            if (!DoesSlideOut)
                return;

            var (x, y) = GetSlideTranslations();
            await content.TranslateTo(x, y, SlideOutDuration);
        }


        async Task FadeInAsync()
        {
            if (!DoesFadeIn)
            {
                Opacity = 1;
                return;
            }

            Opacity = 0.01;
            await this.FadeTo(1.0, FadeInDuration);
        }

        async Task FadeOutAsync()
        {
            if (!DoesFadeOut)
            {
                Opacity = 0;
                return;
            }

            Opacity = 1.0;
            await this.FadeTo(0.0, FadeOutDuration);
        }
    }
}