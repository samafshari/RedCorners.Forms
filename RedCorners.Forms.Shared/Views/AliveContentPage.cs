using RedCorners.Forms.Systems;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using System.Runtime.CompilerServices;

#if __ANDROID__
using Android.OS;
using Android.Views;
#endif

namespace RedCorners.Forms
{
    public class AliveContentPage : ContentPage
    {
        public enum UIStatusBarStyles
        {
            Default = 0,
            BlackTranslucent = 1,
            LightContent = 1,
            BlackOpaque = 2
        }

        bool isStarted = false;
        WeakReference<BindableModel> lastContext = null;

        public event EventHandler OnAppeared;
        public event EventHandler OnDisappeared;
        public Action UpdateAndroidStuff;

        public bool FixTopPadding
        {
            get => (bool)GetValue(FixTopPaddingProperty);
            set => SetValue(FixTopPaddingProperty, value);
        }

        public bool FixBottomPadding
        {
            get => (bool)GetValue(FixBottomPaddingProperty);
            set => SetValue(FixBottomPaddingProperty, value);
        }

        public UIStatusBarStyles UIStatusBarStyle
        {
            get => (UIStatusBarStyles)GetValue(UIStatusBarStyleProperty);
            set => SetValue(UIStatusBarStyleProperty, value);
        }

        public Color AndroidStatusBarColor
        {
            get => (Color)GetValue(AndroidStatusBarColorProperty);
            set => SetValue(AndroidStatusBarColorProperty, value);
        }

        public bool AndroidTranslucentStatus
        {
            get => (bool)GetValue(AndroidTranslucentStatusProperty);
            set => SetValue(AndroidTranslucentStatusProperty, value);
        }

        //public bool AndroidLayoutInScreen
        //{
        //    get => (bool)GetValue(AndroidLayoutInScreenProperty);
        //    set => SetValue(AndroidLayoutInScreenProperty, value);
        //}

        bool isPaddingFixed;
        Thickness originalPadding;

        public static BindableProperty FixTopPaddingProperty = BindableProperty.Create(
            nameof(FixTopPadding),
            typeof(bool),
            typeof(AliveContentPage),
            true,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                (bindable as AliveContentPage).AdjustPadding();
            });

        public static BindableProperty FixBottomPaddingProperty = BindableProperty.Create(
            nameof(FixBottomPadding),
            typeof(bool),
            typeof(AliveContentPage),
            true,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                (bindable as AliveContentPage).AdjustPadding();
            });

        public static BindableProperty UIStatusBarStyleProperty = BindableProperty.Create(
            nameof(UIStatusBarStyle),
            typeof(UIStatusBarStyles),
            typeof(AliveContentPage),
            UIStatusBarStyles.LightContent,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
            });

        public static BindableProperty AndroidStatusBarColorProperty = BindableProperty.Create(
            nameof(AndroidStatusBarColor),
            typeof(Color),
            typeof(AliveContentPage),
            Color.Black,
            BindingMode.TwoWay,
            propertyChanged: UpdateAndroidSettings);

        public static BindableProperty AndroidTranslucentStatusProperty = BindableProperty.Create(
            nameof(AndroidTranslucentStatus),
            typeof(bool),
            typeof(AliveContentPage),
            true,
            BindingMode.TwoWay,
            propertyChanged: UpdateAndroidSettings);

        //public static BindableProperty AndroidLayoutInScreenProperty = BindableProperty.Create(
        //    nameof(AndroidLayoutInScreen),
        //    typeof(bool),
        //    typeof(AliveContentPage),
        //    true,
        //    BindingMode.TwoWay,
        //    propertyChanged: UpdateAndroidSettings);

        static void UpdateAndroidSettings(BindableObject bindable, object oldVal, object newVal)
        {
            (bindable as AliveContentPage)?.UpdateAndroidStuff?.Invoke();
        }

        public AliveContentPage()
        {
            AdjustPadding();
        }

        IEnumerable<AliveContentView> GetAliveChildren()
        {
            if (Content is VisualElement ve)
            {
                return ve
                    .GetAllChildren()
                    .Where(x => x is AliveContentView)
                    .Select(x => (AliveContentView)x);
            }

            return new AliveContentView[] { };
        }

        protected override void OnAppearing()
        {
            OnAppeared?.Invoke(this, null);

            base.OnAppearing();

            AdjustPadding();

            if (BindingContext is BindableModel vm)
            {
                if (!isStarted)
                {
                    if (lastContext != null && lastContext.TryGetTarget(out var lastVm))
                    {
                        lastVm.OnStop();
                    }
                    isStarted = true;
                    vm.OnStart();
                    lastContext = new WeakReference<BindableModel>(vm);
                }

                vm.OnAppeared(this);
            }
        }

        void AdjustPadding()
        {
            if (FixTopPadding)
            {
                if (!isPaddingFixed)
                {
                    originalPadding = Padding;
                    isPaddingFixed = true;
                }

                Padding = NotchSystem.Instance.TopMargin;
            }
            else if (isPaddingFixed)
            {
                Padding = originalPadding;
                isPaddingFixed = false;
            }

            if (FixBottomPadding && NotchSystem.Instance.HasNotch)
            {
                var padding = Padding;
                padding.Bottom = NotchSystem.Instance.BottomMargin.Bottom + 10;
                Padding = padding;
            }
        }

        protected override void OnDisappearing()
        {
            OnDisappeared?.Invoke(this, null);

            if (isStarted && BindingContext is BindableModel vm)
            {
                isStarted = false;
                vm.OnStop();
                lastContext = null;
            }

            base.OnDisappearing();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(Content))
            {
                var children = GetAliveChildren().ToList();
                foreach (var item in children)
                {
                    item.HookToAlivePage(this);
                }
            }
        }

        protected override void OnBindingContextChanged()
        {
            BindableModel lastVm = null;
            if (lastContext != null && lastContext.TryGetTarget(out lastVm))
            {
                lastVm.OnUnbind(this);
            }

            if (BindingContext is BindableModel vm)
            {
                vm.OnBind(this);
                if (!isStarted)
                {
                    lastVm?.OnStop();
                    isStarted = true;
                    vm.OnStart();
                    lastContext = new WeakReference<BindableModel>(vm);
                }
            }

            base.OnBindingContextChanged();
        }

        protected override bool OnBackButtonPressed()
        {
            if (BindingContext is BindableModel vm)
            {
                if (vm.OnBack()) return true;
            }
            return base.OnBackButtonPressed();
        }
    }
}
