using RedCorners.Forms.Systems;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RedCorners.Forms
{
    public enum UIStatusBarStyles
    {
        Default = 0,
        //BlackTranslucent = 1,
        LightContent = 1,
        BlackOpaque = 2
    }

    public class AliveContentPage : ContentPage
    {
        bool isStarted = false;
        WeakReference<BindableModel> lastContext = null;

        public event EventHandler OnAppeared;
        public event EventHandler OnDisappeared;

        public Action PlatformUpdate;

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

        public bool AndroidLayoutInScreen
        {
            get => (bool)GetValue(AndroidLayoutInScreenProperty);
            set => SetValue(AndroidLayoutInScreenProperty, value);
        }

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
            propertyChanged: UpdateSettings);

        public static BindableProperty AndroidStatusBarColorProperty = BindableProperty.Create(
            nameof(AndroidStatusBarColor),
            typeof(Color),
            typeof(AliveContentPage),
            Color.Black,
            BindingMode.TwoWay,
            propertyChanged: UpdateSettings);

        public static BindableProperty AndroidTranslucentStatusProperty = BindableProperty.Create(
            nameof(AndroidTranslucentStatus),
            typeof(bool),
            typeof(AliveContentPage),
            true,
            BindingMode.TwoWay,
            propertyChanged: UpdateSettings);

        public static BindableProperty AndroidLayoutInScreenProperty = BindableProperty.Create(
            nameof(AndroidLayoutInScreen),
            typeof(bool),
            typeof(AliveContentPage),
            true,
            BindingMode.TwoWay,
            propertyChanged: UpdateSettings);

        static void UpdateSettings(BindableObject bindable, object oldVal, object newVal)
        {
            (bindable as AliveContentPage)?.PlatformUpdate?.Invoke();
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
            PlatformUpdate?.Invoke();

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

        Thickness? originalPadding;

        public void AdjustPadding()
        {
            if (originalPadding == null)
            {
                originalPadding = Padding;
            }

            var pageMargin = NotchSystem.Instance.GetPageMargin();
            var top = FixTopPadding ? pageMargin.Top : originalPadding.Value.Top;
            var bottom = FixBottomPadding ? pageMargin.Bottom : originalPadding.Value.Bottom;
            Padding = new Thickness(
                originalPadding.Value.Left,
                top,
                originalPadding.Value.Right,
                bottom);

            foreach (var child in GetAliveChildren())
                child.AdjustPadding();
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
                foreach (var item in GetAliveChildren())
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
