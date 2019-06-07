using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedCorners.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedContentPage
    {
        public TabbedContentPage()
        {
            InitializeComponent();
            UpdateTabbarBackgroundView();
        }

        public Color TabbarBackgroundColor
        {
            get => (Color)GetValue(TabbarBackgroundColorProperty);
            set => SetValue(TabbarBackgroundColorProperty, value);
        }

        public View TabbarBackgroundView
        {
            get => (View)GetValue(TabbarBackgroundViewProperty);
            set => SetValue(TabbarBackgroundViewProperty, value);
        }

        public GridLength TabbarHeight
        {
            get => (GridLength)GetValue(TabbarHeightProperty);
            set => SetValue(TabbarHeightProperty, value);
        }

        public static readonly BindableProperty TabbarBackgroundViewProperty =
            BindableProperty.Create(
            nameof(TabbarBackgroundView),
            typeof(View),
            typeof(TabbedContentPage),
            default(View),
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TabbedContentPage page)
                {
                    page.UpdateTabbarBackgroundView();
                }
            });

        public static readonly BindableProperty TabbarHeightProperty = 
            BindableProperty.Create(
            nameof(TabbarHeight),
            typeof(GridLength),
            typeof(TabbedContentPage),
            new GridLength(70),
            BindingMode.TwoWay);

        public static readonly BindableProperty TabbarBackgroundColorProperty =
            BindableProperty.Create(
            nameof(TabbarBackgroundColor),
            typeof(Color),
            typeof(TabbedContentPage),
            Color.FromHex("#EEEEEE"),
            BindingMode.TwoWay);

        void UpdateTabbarBackgroundView()
        {
            if (tabbarBackground != null)
                tabbarBackground.Content = TabbarBackgroundView;
        }
    }
}