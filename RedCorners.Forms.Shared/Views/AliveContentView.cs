using RedCorners.Forms.Systems;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RedCorners.Forms
{
    public class AliveContentView : ContentView
    {
        bool parentWasNull = true;
        WeakReference<AliveContentPage> pagePointer = null;

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
            typeof(AliveContentView),
            false,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                (bindable as AliveContentView).AdjustPadding();
            });

        public static BindableProperty FixBottomPaddingProperty = BindableProperty.Create(
            nameof(FixBottomPadding),
            typeof(bool),
            typeof(AliveContentView),
            false,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                (bindable as AliveContentView).AdjustPadding();
            });

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

        void TryHook(AliveContentPage page)
        {
            if (parentWasNull && Parent != null)
            {
                page = page ?? GetPage();
                if (page != null)
                {
                    parentWasNull = false;
                    pagePointer = new WeakReference<AliveContentPage>(page);
                    page.OnAppeared += Page_OnAppeared;
                    page.OnDisappeared += Page_OnDisappeared;
                    TriggerStart();
                }
            }
            else if (!parentWasNull && Parent == null)
            {
                if (pagePointer.TryGetTarget(out var lastPage))
                {
                    lastPage.OnDisappeared -= Page_OnDisappeared;
                    lastPage.OnAppeared -= Page_OnAppeared;
                }
                parentWasNull = true;
                TriggerStop();
            }

            AdjustPadding();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            TryHook(null);
        }

        public void HookToAlivePage(AliveContentPage page)
        {
            TryHook(page);
        }

        protected override void ChangeVisualState()
        {
            base.ChangeVisualState();
        }

        private void Page_OnDisappeared(object sender, EventArgs e)
        {
            TriggerStop();
        }

        private void Page_OnAppeared(object sender, EventArgs e)
        {
            TriggerStart();
        }

        public virtual void Start()
        {

        }

        public virtual void Stop()
        {

        }

        bool isStarted = false;
        public void TriggerStart()
        {
            if (!isStarted)
            {
                isStarted = true;
                Start();
            }
        }

        public void TriggerStop()
        {
            if (isStarted)
            {
                isStarted = false;
                Stop();
            }
        }

        public AliveContentPage GetPage()
        {
            if (Parent is AliveContentPage p) return p;

            var el = this as Element;
            while (true)
            {
                if (el == null || el.Parent == null) return null;
                if (el.Parent is AliveContentPage page) return page;
                el = el.Parent;
            }
        }

        public bool GetIsVisibleRecursive()
        {
            var el = (View)this;
            while (el != null)
            {
                if (!el.IsVisible) return false;
                el = el.Parent as View;
            }
            return true;
        }
    }
}
