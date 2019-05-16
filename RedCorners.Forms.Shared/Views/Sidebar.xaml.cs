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

        public Sidebar()
        {
            InitializeComponent();
            UpdateLayout();
            btnDismiss.Clicked += BtnDismiss_Clicked;
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

        bool oldVisible = false;
        protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanging(propertyName);

            if (propertyName == nameof(IsVisible))
            {
                oldVisible = IsVisible;
                if (!IsVisible) Opacity = 0;
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsVisible))
            {
                if (!oldVisible && IsVisible)
                {
                    Opacity = 0;
                    this.FadeTo(1.0, 100);
                }
                else if (!IsVisible)
                    Opacity = 0;
            }
        }
    }
}