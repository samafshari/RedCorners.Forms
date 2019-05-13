using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Reflection;

namespace RedCorners.Forms
{
    public class HorizontalShadowView : Image
    {
        public HorizontalShadowView()
        {
            Source = ImageSource.FromResource("RedCorners.Forms.gradienth.png", typeof(HorizontalShadowView).GetTypeInfo().Assembly);
            Aspect = Aspect.Fill;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            HeightRequest = 10;
            Opacity = 0.2f;
            Margin = new Thickness(0);
            VerticalOptions = LayoutOptions.Start;
        }
    }

    public class HorizontalShadowView2 : HorizontalShadowView
    {
        public HorizontalShadowView2()
        {
            VerticalOptions = LayoutOptions.End;
            Source = ImageSource.FromResource("RedCorners.Forms.gradienth2.png", typeof(HorizontalShadowView).GetTypeInfo().Assembly);
        }
    }
}
