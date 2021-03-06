﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using RedCorners.Forms;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage2), typeof(RedCorners.Forms.Renderers.PageRenderer))]
namespace RedCorners.Forms.Renderers
{
    public class PageRenderer : Xamarin.Forms.Platform.iOS.PageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is ContentPage2 page)
            {
                page.PlatformUpdate = () =>
                {
                    UIApplication.SharedApplication.SetStatusBarHidden(page.UIStatusBarHidden, page.UIStatusBarAnimated);
                    UIApplication.SharedApplication.SetStatusBarStyle((UIStatusBarStyle)(long)page.UIStatusBarStyle, page.UIStatusBarAnimated);
                    if (page.KeepScreenOn.HasValue)
                        UIApplication.SharedApplication.IdleTimerDisabled = page.KeepScreenOn.Value;
                    SetNeedsStatusBarAppearanceUpdate();
                };
            }
        }

        static UIStatusBarStyles lastStyle = UIStatusBarStyles.Default;
        static bool lastHidden = false;

        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            var style = (long)((Element as ContentPage2)?.UIStatusBarStyle ?? lastStyle);
            lastStyle = (UIStatusBarStyles)style;
            return (UIStatusBarStyle)style;
        }

        public override bool PrefersStatusBarHidden()
        {
            lastHidden = (Element as ContentPage2)?.UIStatusBarHidden ?? lastHidden;
            return lastHidden;
        }
    }
}