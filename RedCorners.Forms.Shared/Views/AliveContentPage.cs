using RedCorners.Forms.Systems;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RedCorners.Forms
{
    public class AliveContentPage : ContentPage
    {
        bool isStarted = false;
        WeakReference<BindableModel> lastContext = null;

        public event EventHandler OnAppeared;
        public event EventHandler OnDisappeared;

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
                        lastVm.Stop();
                    }
                    isStarted = true;
                    vm.Start();
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
                vm.Stop();
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
            if (!isStarted && BindingContext is BindableModel vm)
            {
                if (lastContext != null && lastContext.TryGetTarget(out var lastVm))
                {
                    lastVm.Stop();
                }
                isStarted = true;
                vm.Start();
                lastContext = new WeakReference<BindableModel>(vm);
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
