using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using RedCorners.Forms;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AliveContentPage), typeof(RedCorners.Forms.Renderers.PageRenderer))]
namespace RedCorners.Forms.Renderers
{
    public class PageRenderer : Xamarin.Forms.Platform.iOS.PageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is AliveContentPage page)
            {
                page.PlatformUpdate = () =>
                {
                    SetNeedsStatusBarAppearanceUpdate();
                };
            }
        }
        
        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return (UIStatusBarStyle)(long)((AliveContentPage)Element).UIStatusBarStyle;
        }
    }
}