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
    public enum SidebarSides
    {
        Left,
        Right,
        Top,
        Bottom
    }

    [ContentProperty("Body")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Sidebar 
    {
        public View Body
        {
            get => (View)GetValue(BodyProperty);
            set => SetValue(BodyProperty, value);
        }

        public SidebarSides Side
        {
            get => (SidebarSides)GetValue(SideProperty);
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

        public uint FadeDuration
        {
            get => (uint)GetValue(FadeDurationProperty);
            set => SetValue(FadeDurationProperty, value);
        }

        public bool DoesFadeIn
        {
            get => (bool)GetValue(DoesFadeInProperty);
            set => SetValue(DoesFadeInProperty, value);
        }

        public uint SlideDuration
        {
            get => (uint)GetValue(SlideDurationProperty);
            set => SetValue(SlideDurationProperty, value);
        }

        public bool DoesSlide
        {
            get => (bool)GetValue(DoesSlideProperty);
            set => SetValue(DoesSlideProperty, value);
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

        public static readonly BindableProperty BodyProperty = BindableProperty.Create(
            propertyName: nameof(Body),
            returnType: typeof(View),
            declaringType: typeof(Sidebar),
            defaultValue: new Grid(),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                (bindable as Sidebar)?.UpdateLayout();
            });

        public static readonly BindableProperty SideProperty = BindableProperty.Create(
            propertyName: nameof(Side),
            returnType: typeof(SidebarSides),
            declaringType: typeof(Sidebar),
            defaultValue: SidebarSides.Left,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                (bindable as Sidebar)?.UpdateLayout();
            });

        public static readonly BindableProperty ContentSizeProperty = BindableProperty.Create(
            propertyName: nameof(ContentSize),
            returnType: typeof(GridLength),
            declaringType: typeof(Sidebar),
            defaultValue: new GridLength(3, GridUnitType.Star),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                (bindable as Sidebar)?.UpdateLayout();
            });

        public static readonly BindableProperty FadeDurationProperty = BindableProperty.Create(
            propertyName: nameof(FadeDuration),
            returnType: typeof(uint),
            declaringType: typeof(Sidebar),
            defaultValue: (uint)100,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
            });

        public static readonly BindableProperty DoesFadeInProperty = BindableProperty.Create(
            propertyName: nameof(DoesFadeIn),
            returnType: typeof(bool),
            declaringType: typeof(Sidebar),
            defaultValue: true,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
            });

        public static readonly BindableProperty SlideDurationProperty = BindableProperty.Create(
            propertyName: nameof(SlideDuration),
            returnType: typeof(uint),
            declaringType: typeof(Sidebar),
            defaultValue: (uint)250,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
            });

        public static readonly BindableProperty DoesSlideProperty = BindableProperty.Create(
            propertyName: nameof(DoesSlide),
            returnType: typeof(bool),
            declaringType: typeof(Sidebar),
            defaultValue: true,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
            });

        public static readonly BindableProperty IsSwipeEnabledProperty = BindableProperty.Create(
            propertyName: nameof(IsSwipeEnabled),
            returnType: typeof(bool),
            declaringType: typeof(Sidebar),
            defaultValue: true,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
            });

        public static readonly BindableProperty IsFullSizeProperty = BindableProperty.Create(
            propertyName: nameof(IsFullSize),
            returnType: typeof(bool),
            declaringType: typeof(Sidebar),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                (bindable as Sidebar)?.UpdateLayout();
            });

        SwipeGestureRecognizer swipeRight = null, swipeLeft = null, swipeUp = null, swipeDown = null;
        SwipeGestureRecognizer swipeRightIn = null, swipeLeftIn = null, swipeUpIn = null, swipeDownIn = null;

        public Sidebar()
        {
            InitializeComponent();
            UpdateLayout();

            var tap = new TapGestureRecognizer();
            tap.Tapped += BtnDismiss_Clicked;
            btnDismiss.GestureRecognizers.Add(tap);
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            if (Parent is View view && swipeRight == null)
            {
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

            if ((Side == SidebarSides.Right && e.Direction == SwipeDirection.Left) ||
                (Side == SidebarSides.Left && e.Direction == SwipeDirection.Right) ||
                (Side == SidebarSides.Top && e.Direction == SwipeDirection.Down) ||
                (Side == SidebarSides.Bottom && e.Direction == SwipeDirection.Up))
                IsVisible = true;
        }

        private void SwipeIn_Swiped(object sender, SwipedEventArgs e)
        {
            if (!IsSwipeEnabled) return;
            if (!IsVisible) return;

            if ((Side == SidebarSides.Right && e.Direction == SwipeDirection.Right) ||
                (Side == SidebarSides.Left && e.Direction == SwipeDirection.Left) ||
                (Side == SidebarSides.Top && e.Direction == SwipeDirection.Up) ||
                (Side == SidebarSides.Bottom && e.Direction == SwipeDirection.Down))
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

            if (Side == SidebarSides.Right || Side == SidebarSides.Left)
            {
                Grid.SetRow(content, 0);
                Grid.SetRowSpan(content, 2);
                Grid.SetColumnSpan(content, 1);

                if (Side == SidebarSides.Left)
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

                if (Side == SidebarSides.Top)
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

        bool oldVisible = false;
        protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanging(propertyName);

            if (propertyName == nameof(IsVisible))
            {
                oldVisible = IsVisible;
                if (!IsVisible)
                {
                    Opacity = 0;
                    CloseSlide();
                }
            }
        }

        void CloseSlide()
        {
            if (!DoesSlide) return;
            var baseView = (Parent as View) ?? this;
            var h = (Side == SidebarSides.Left || Side == SidebarSides.Right);
            var baseSize =  h ? baseView.Width : baseView.Height;
            if (Width > 0) baseSize = h ? Width : Height;
            double translation;
            if (h)
            {
                if (IsFullSize) translation = baseSize;
                else if (ContentSize.IsAuto) translation = content.Width;
                else if (ContentSize.IsStar) translation = baseSize / ContentSize.Value;
                else translation = ContentSize.Value;
                if (Side == SidebarSides.Left) translation *= -1;
                content.TranslationX = translation;
            }
            else
            {
                if (IsFullSize) translation = baseSize;
                else if (ContentSize.IsAuto) translation = content.Height;
                else if (ContentSize.IsStar) translation = baseSize / ContentSize.Value;
                else translation = ContentSize.Value;
                if (Side == SidebarSides.Top) translation *= -1;
                content.TranslationY = translation;
            }

            Console.WriteLine($"BaseSize: {baseSize}, Translation: {translation}, Side: {Side}");
        }

        void OpenSlide()
        {
            if (!DoesSlide)
            {
                content.TranslationX = 0;
                content.TranslationY = 0;
                return;
            }

            content.TranslateTo(0, 0, SlideDuration);
        }

        void FadeIn()
        {
            if (!DoesFadeIn)
            {
                Opacity = 1;
                return;
            }

            Opacity = 0.1;
            this.FadeTo(1.0, FadeDuration);
        }

        void FadeOut()
        {
            Opacity = 0;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsVisible))
            {
                if (!oldVisible && IsVisible)
                {
                    FadeIn();
                    CloseSlide();
                    OpenSlide();
                }
                else if (!IsVisible)
                    FadeOut();
            }
        }
    }
}