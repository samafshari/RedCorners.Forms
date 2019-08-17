using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Foundation;
using UIKit;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Specifics = Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using SizeF = CoreGraphics.CGSize;
using Xamarin.Forms;
using RedCorners.Forms;
using RedCorners.Forms.Renderers;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Frame2), typeof(Frame2Renderer))]
namespace RedCorners.Forms.Renderers
{
    public class Frame2Renderer : VisualElementRenderer<Frame2>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame2> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
                SetupLayer();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Xamarin.Forms.VisualElement.BackgroundColorProperty.PropertyName ||
                e.PropertyName == Xamarin.Forms.Frame.BorderColorProperty.PropertyName ||
                e.PropertyName == Xamarin.Forms.Frame.HasShadowProperty.PropertyName ||
                e.PropertyName == Xamarin.Forms.Frame.CornerRadiusProperty.PropertyName ||
                e.PropertyName == Frame2.ShadowRadiusProperty.PropertyName ||
                e.PropertyName == Frame2.ShadowColorProperty.PropertyName)
                SetupLayer();
        }

        void SetupLayer()
        {
            float cornerRadius = Element.CornerRadius;

            if (cornerRadius == -1f)
                cornerRadius = 5f; // default corner radius

            var shadowRadius = Element.ShadowRadius;
            if (shadowRadius == -1f)
                shadowRadius = 5f;

            Layer.CornerRadius = cornerRadius;

            if (Element.BackgroundColor == Color.Default)
                Layer.BackgroundColor = UIColor.White.CGColor;
            else
                Layer.BackgroundColor = Element.BackgroundColor.ToCGColor();

            if (Element.HasShadow)
            {
                Layer.ShadowRadius = shadowRadius;
                Layer.ShadowColor = Element.ShadowColor.ToCGColor();
                Layer.ShadowOpacity = 1.0f;
                Layer.ShadowOffset = new SizeF();
            }
            else
                Layer.ShadowOpacity = 0;

            if (Element.BorderColor == Color.Default)
                Layer.BorderColor = UIColor.Clear.CGColor;
            else
            {
                Layer.BorderColor = Element.BorderColor.ToCGColor();
                Layer.BorderWidth = 1;
            }

            Layer.RasterizationScale = UIScreen.MainScreen.Scale;
            Layer.ShouldRasterize = true;
        }
    }
}