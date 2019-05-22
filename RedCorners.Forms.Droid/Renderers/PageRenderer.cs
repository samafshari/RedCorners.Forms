using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using RedCorners.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AliveContentPage), typeof(RedCorners.Forms.Renderers.PageRenderer))]
namespace RedCorners.Forms.Renderers
{
    public class PageRenderer : Xamarin.Forms.Platform.Android.PageRenderer
    {
        public PageRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is AliveContentPage page)
            {
                UpdateAndroidStuff();
                page.PlatformUpdate = UpdateAndroidStuff;
            }
        }

        void UpdateAndroidStuff()
        {
            // This whole flow is buggy when done on the fly,
            // also, !T and !L doesn't go well together, so if it happens, we change it to !T and L

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                var activity = GetActivity(Context);
                var page = Element as AliveContentPage;

                var t = page.AndroidTranslucentStatus;
                var l = page.AndroidLayoutInScreen;

                if (!t && !l) l = true;

                if (activity != null)
                {
                    if (l)
                    {
                        activity.Window.DecorView.SetFitsSystemWindows(true);
                        activity.Window.AddFlags(WindowManagerFlags.LayoutInScreen);
                    }
                    else
                    {
                        activity.Window.DecorView.SetFitsSystemWindows(false);
                        activity.Window.ClearFlags(WindowManagerFlags.LayoutInScreen);
                    }

                    if (t)
                        activity.Window.AddFlags(WindowManagerFlags.TranslucentStatus);
                    else
                        activity.Window.ClearFlags(WindowManagerFlags.TranslucentStatus);

                    activity.Window.SetStatusBarColor(page.AndroidStatusBarColor.ToAndroid());

                    UpdateLayout();
                }

                DisplayMetrics metrics = new DisplayMetrics();
                activity.WindowManager.DefaultDisplay.GetMetrics(metrics);
                var usableHeight = metrics.HeightPixels;
                activity.WindowManager.DefaultDisplay.GetRealMetrics(metrics);
                var height = metrics.HeightPixels;
                var dpi = Resources.DisplayMetrics.ScaledDensity;

                var softHeight = Math.Max(0, (height - usableHeight) / dpi);

                var statusHeight = 25;

                // U11
                //T, !L: TOP OK BOTTOM OK
                //!T, !L: OK OK
                //!T, L: OK OK
                //T, L: OK OK

                //EMU
                //T, L: KK
                //!T, L: KK
                //!T, !L: NO
                //T, !L; KK

                if (!t && !l) statusHeight = 0;
                if (!t && l) statusHeight = 0;

                if (t && l) softHeight = 0;
                if (!t && l) softHeight = 0;
                if (t && !l) softHeight = 0;

                Signals.AndroidSafeInsetsUpdate.Signal<Thickness>(new Thickness(0, statusHeight, 0, softHeight));

                page.AdjustPadding();
                
            }
        }

        static Activity GetActivity(Context context)
        {
            if (context == null) return null;
            if (context is Activity activity) return activity;
            if (context is ContextWrapper wrapper) return GetActivity(wrapper.BaseContext);
            return null;
        }
    }
}