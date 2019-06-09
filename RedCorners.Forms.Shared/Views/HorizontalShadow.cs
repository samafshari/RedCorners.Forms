using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Reflection;

namespace RedCorners.Forms
{
    public class HorizontalShadow : Image
    {
        public HorizontalShadow()
        {
            Source = ImageSource.FromResource("RedCorners.Forms.gradienth.png", typeof(HorizontalShadow).GetTypeInfo().Assembly);
            Aspect = Aspect.Fill;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            HeightRequest = 10;
            Opacity = 0.2f;
            Margin = new Thickness(0);
            VerticalOptions = LayoutOptions.Start;
            InputTransparent = true;
        }
    }

    public class HorizontalShadow2 : HorizontalShadow
    {
        public HorizontalShadow2()
        {
            VerticalOptions = LayoutOptions.End;
            Source = ImageSource.FromResource("RedCorners.Forms.gradienth2.png", typeof(HorizontalShadow).GetTypeInfo().Assembly);
        }
    }

    public class HorizontalLine : BoxView
    {
        public HorizontalLine()
        {
            HorizontalOptions = LayoutOptions.Fill;
            HeightRequest = 1;
            BackgroundColor = Color.White;
        }
    }
}
