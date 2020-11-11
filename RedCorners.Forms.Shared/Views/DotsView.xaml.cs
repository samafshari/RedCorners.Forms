using RedCorners;
using RedCorners.Forms;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace RedCorners.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DotsView
    {
        public int Count
        {
            get => (int)GetValue(CountProperty);
            set => SetValue(CountProperty, value);
        }

        public static readonly BindableProperty CountProperty = BindableProperty.Create(
            nameof(Count), typeof(int), typeof(DotsView), 1, propertyChanged: (bindable, oldVal, newVal) =>
            {
                (bindable as DotsView).UpdateLayout();
            });

        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(
            nameof(SelectedIndex), typeof(int), typeof(DotsView), 1, propertyChanged: (bindable, oldVal, newVal) =>
            {
                (bindable as DotsView).UpdateLayout();
            });

        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(
            nameof(SelectedColor), typeof(Color), typeof(DotsView), Color.White, propertyChanged: (bindable, oldVal, newVal) =>
            {
                (bindable as DotsView).UpdateLayout();
            });
        
        public Color NormalColor
        {
            get => (Color)GetValue(NormalColorProperty);
            set => SetValue(NormalColorProperty, value);
        }

        public static readonly BindableProperty NormalColorProperty = BindableProperty.Create(
            nameof(NormalColor), typeof(Color), typeof(DotsView), Color.FromHex("#8FFF"), propertyChanged: (bindable, oldVal, newVal) =>
            {
                (bindable as DotsView).UpdateLayout();
            });


        public DotsView()
        {
            InitializeComponent();
            UpdateLayout();
        }

        void UpdateLayout()
        {
            Children.Clear();

            var size = 10;
            for (int i = 0; i < Count; i++)
            {
                var ellipse = new Ellipse
                {
                    WidthRequest = size,
                    HeightRequest = size,
                    Fill = new SolidColorBrush
                    {
                        Color = i == SelectedIndex ? SelectedColor : NormalColor
                    }
                };
                Children.Add(ellipse);
            }
        }
    }
}