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
    [ContentProperty("Body")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Sidebar 
    {
        public enum Sides
        {
            Left,
            Right
        }

        public View Body
        {
            get => (View)GetValue(BodyProperty);
            set => SetValue(BodyProperty, value);
        }

        public Sides Side
        {
            get => (Sides)GetValue(SideProperty);
            set => SetValue(SideProperty, value);
        }

        public GridLength ContentWidth
        {
            get => (GridLength)GetValue(ContentWidthProperty);
            set => SetValue(ContentWidthProperty, value);
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
            returnType: typeof(Sides),
            declaringType: typeof(Sidebar),
            defaultValue: Sides.Left,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                (bindable as Sidebar)?.UpdateLayout();
            });

        public static readonly BindableProperty ContentWidthProperty = BindableProperty.Create(
            propertyName: nameof(ContentWidth),
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

        SwipeGestureRecognizer swipeRight = null, swipeLeft = null;
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
            }

            UpdateLayout();
        }

        private void Swipe_Swiped(object sender, SwipedEventArgs e)
        {
            if (!IsSwipeEnabled) return;
            if (IsVisible) return;

            if ((Side == Sides.Right && e.Direction == SwipeDirection.Left) ||
                (Side == Sides.Left && e.Direction == SwipeDirection.Right))
                IsVisible = true;
        }

        private void BtnDismiss_Clicked(object sender, EventArgs e)
        {
            SetValue(IsVisibleProperty, false);
        }

        void UpdateLayout()
        {
            if (Side == Sides.Left)
            {
                colLeft.Width = ContentWidth;
                colRight.Width = GridLength.Star;

                Grid.SetColumn(content, 0);
            }
            else
            {
                colLeft.Width = GridLength.Star;
                colRight.Width = ContentWidth;

                Grid.SetColumn(content, 1);
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
            var baseWidth = baseView.Width;
            if (Width > 0) baseWidth = Width;
            var translation = ContentWidth.IsStar ?
               (baseWidth * (1.0f / ContentWidth.Value)) :
               ContentWidth.Value;
            if (Side == Sides.Left) translation *= -1;
            content.TranslationX = translation;

            Console.WriteLine($"BaseWidth: {baseWidth}, Translation: {translation}, Side: {Side}");
        }

        void OpenSlide()
        {
            if (!DoesSlide)
            {
                content.TranslationX = 0;
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