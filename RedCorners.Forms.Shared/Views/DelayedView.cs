using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public uint Delay
        {
            get => (uint)GetValue(DelayProperty);
            set => SetValue(DelayProperty, value);
        }

        public static readonly BindableProperty LoadingViewProperty = BindableProperty.Create(
            propertyName: nameof(LoadingView),
            returnType: typeof(View),
            declaringType: typeof(TitledContentView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty DelayProperty = BindableProperty.Create(
            propertyName: nameof(LoadingView),
            returnType: typeof(uint),
            declaringType: typeof(DelayedView),
            defaultValue: 50,
            defaultBindingMode: BindingMode.TwoWay);

        public DelayedView()
        {
        }

        public override void OnStart()
        {
            base.OnStart();
        }
    }
}