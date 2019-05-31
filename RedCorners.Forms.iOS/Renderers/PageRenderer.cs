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
                    UIApplication.SharedApplication.SetStatusBarHidden(page.UIStatusBarHidden, page.UIStatusBarAnimated);
                    UIApplication.SharedApplication.SetStatusBarStyle((UIStatusBarStyle)(long)page.UIStatusBarStyle, page.UIStatusBarAnimated);
                    SetNeedsStatusBarAppearanceUpdate();
                };
            }
        }
        
        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            var style = (long)((AliveContentPage)Element).UIStatusBarStyle;
            return (UIStatusBarStyle)style;
        }

        public override bool PrefersStatusBarHidden()
        {
            return ((AliveContentPage)Element).UIStatusBarHidden;
        }
    }
}