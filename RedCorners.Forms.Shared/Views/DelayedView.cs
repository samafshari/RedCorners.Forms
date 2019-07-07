using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RedCorners.Forms
{
    [ContentProperty("Content")]
    public class DelayedView : ContentView2
    {
        public bool IsLoading { get; protected set; }

        public View LoadingView
        {
            get => (View)GetValue(LoadingViewProperty);
            set => SetValue(LoadingViewProperty, value);
        }

        public int Delay
        {
            get => (int)GetValue(DelayProperty);
            set => SetValue(DelayProperty, value);
        }

        public new View Content
        {
            get => (View)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public Func<object, Task> Job
        {
            get => (Func<object, Task>)GetValue(JobProperty);
            set => SetValue(JobProperty, value);
        }

        public object JobParameter
        {
            get => GetValue(JobParameterProperty);
            set => SetValue(JobParameterProperty, value);
        }

        public static readonly BindableProperty LoadingViewProperty = BindableProperty.Create(
            propertyName: nameof(LoadingView),
            returnType: typeof(View),
            declaringType: typeof(DelayedView),
            defaultValue: (View)new LoadingView(),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is DelayedView view)
                    view.UpdateLoadingView();
            });

        public static readonly BindableProperty DelayProperty = BindableProperty.Create(
            propertyName: nameof(Delay),
            returnType: typeof(int),
            declaringType: typeof(DelayedView),
            defaultValue: 50,
            defaultBindingMode: BindingMode.TwoWay);

        public new static readonly BindableProperty ContentProperty = BindableProperty.Create(
            propertyName: nameof(ContentProperty),
            returnType: typeof(View),
            declaringType: typeof(DelayedView),
            defaultValue: (View)new LoadingView(),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is DelayedView view)
                    view.UpdateContent();
            });

        public static readonly BindableProperty JobProperty = BindableProperty.Create(
            propertyName: nameof(Job),
            returnType: typeof(Func<object, Task>),
            declaringType: typeof(DelayedView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty JobParameterProperty = BindableProperty.Create(
           propertyName: nameof(JobParameter),
           returnType: typeof(object),
           declaringType: typeof(DelayedView),
           defaultValue: null,
           defaultBindingMode: BindingMode.TwoWay);

        bool isLoaded = false;

        public DelayedView()
        {
            UpdateLoadingView();
        }

        public override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            TryWork();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            TryWork();
        }

        void UpdateLoadingView()
        {
            if (isLoaded) return;
            base.Content = LoadingView;
        }

        void UpdateContent()
        {
            if (isLoaded)
            {
                base.Content = Content;
            }
        }

        void TryWork()
        {
            if (isLoaded) return;
            if (Parent != null && Width > 0)
                Work();
        }

        async void Work()
        {
            var delay = Task.Delay(Delay);
            if (Job != null)
            {
                var job = Job(JobParameter);
                await Task.WhenAll(delay, job);
            }
            else
            {
                await delay;
            }

            isLoaded = true;
            UpdateContent();
        }
    }

    internal class LoadingView : Grid
    {
        public LoadingView()
        {
            HorizontalOptions = LayoutOptions.Fill;
            VerticalOptions = LayoutOptions.Fill;

            Children.Add(new ActivityIndicator
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 32,
                WidthRequest = 32,
                IsRunning = true,
                IsEnabled = true
            });
        }
    }
}