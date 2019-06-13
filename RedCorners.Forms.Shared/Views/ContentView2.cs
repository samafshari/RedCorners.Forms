using RedCorners.Forms.Systems;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RedCorners.Forms
{
    [Obsolete("AliveContentView is renamed to ContentView2. Use ContentView2 instead.")]
    public sealed class AliveContentView : ContentView2 { }

    public class ContentView2 : ContentView
    {
        Thickness? originalPadding;
        bool parentWasNull = true;
        WeakReference<ContentPage2> pagePointer = null;

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public ImageSource Icon
        {
            get => (ImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public ImageSource SelectedIcon
        {
            get => (ImageSource)GetValue(SelectedIconProperty);
            set => SetValue(SelectedIconProperty, value);
        }

        public bool IsVisibleAsTab
        {
            get => (bool)GetValue(IsVisibleAsTabProperty);
            set => SetValue(IsVisibleAsTabProperty, value);
        }

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

        public ICommand ShowTabCommand
        {
            get => (ICommand)GetValue(ShowTabCommandProperty);
            set => SetValue(ShowTabCommandProperty, value);
        }

        public static readonly BindableProperty FixTopPaddingProperty = BindableProperty.Create(
            nameof(FixTopPadding),
            typeof(bool),
            typeof(ContentView2),
            false,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                (bindable as ContentView2).AdjustPadding();
            });

        public static readonly BindableProperty IsVisibleAsTabProperty = BindableProperty.Create(
            nameof(IsVisibleAsTab),
            typeof(bool),
            typeof(ContentView2),
            true,
            BindingMode.TwoWay);

        public static readonly BindableProperty FixBottomPaddingProperty = BindableProperty.Create(
            nameof(FixBottomPadding),
            typeof(bool),
            typeof(ContentView2),
            false,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                (bindable as ContentView2).AdjustPadding();
            });

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(ContentView2),
            string.Empty,
            BindingMode.TwoWay);

        public static readonly BindableProperty IconProperty = BindableProperty.Create(
            nameof(Icon),
            typeof(ImageSource),
            typeof(ContentView2),
            null,
            BindingMode.TwoWay);

        public static readonly BindableProperty SelectedIconProperty = BindableProperty.Create(
            nameof(SelectedIcon),
            typeof(ImageSource),
            typeof(ContentView2),
            null,
            BindingMode.TwoWay);

        public static readonly BindableProperty ShowTabCommandProperty = BindableProperty.Create(
            nameof(ShowTabCommand),
            typeof(ICommand),
            typeof(ContentView2),
            null,
            BindingMode.TwoWay);

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
        }

        void TryHook(ContentPage2 page)
        {
            if (parentWasNull && Parent != null)
            {
                page = page ?? GetPage();
                if (page != null)
                {
                    parentWasNull = false;
                    pagePointer = new WeakReference<ContentPage2>(page);
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

        public void HookToAlivePage(ContentPage2 page)
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

        public virtual void OnStart()
        {

        }

        public virtual void OnStop()
        {

        }

        bool isStarted = false;
        public void TriggerStart()
        {
            if (!isStarted)
            {
                isStarted = true;
                OnStart();
            }
        }

        public void TriggerStop()
        {
            if (isStarted)
            {
                isStarted = false;
                OnStop();
            }
        }

        public ContentPage2 GetPage()
        {
            if (Parent is ContentPage2 p) return p;

            var el = this as Element;
            while (true)
            {
                if (el == null || el.Parent == null) return null;
                if (el.Parent is ContentPage2 page) return page;
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

        WeakReference<BindableModel> lastContext = null;
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (lastContext != null && lastContext.TryGetTarget(out var lastVm))
                lastVm?.OnUnbind(this);
            if (BindingContext is BindableModel vm)
            {
                vm.OnBind(this);
                lastContext = new WeakReference<BindableModel>(vm);
            }
        }
    }
}
