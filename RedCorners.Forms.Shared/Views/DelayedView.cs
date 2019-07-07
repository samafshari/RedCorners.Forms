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

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
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

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            propertyName: nameof(Command),
            returnType: typeof(ICommand),
            declaringType: typeof(DelayedView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
           propertyName: nameof(CommandParameter),
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
            if (isLoaded) return;
            if (Parent != null)
                Work();
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

        async void Work()
        {
            var delay = Task.Delay(Delay);
            var job = Task.Run(() =>
            {
                if (Command?.CanExecute(CommandParameter) ?? false)
                    Command?.Execute(CommandParameter);
            });
            await Task.WhenAll(delay, job);

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