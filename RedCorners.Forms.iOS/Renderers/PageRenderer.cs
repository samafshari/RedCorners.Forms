using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using RedCorners.Forms;
using UIKit;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(AliveContentPage), typeof(RedCorners.Forms.Renderers.PageRenderer))]
namespace RedCorners.Forms.Renderers
{
    public class PageRenderer : Xamarin.Forms.Platform.iOS.PageRenderer
    {
        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return (UIStatusBarStyle)(long)((AliveContentPage)Element).UIStatusBarStyle;
        }
    }
}