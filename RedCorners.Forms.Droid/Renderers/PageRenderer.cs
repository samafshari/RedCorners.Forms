using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using RedCorners.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ContentPage2), typeof(RedCorners.Forms.Renderers.PageRenderer))]
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

            if (e.NewElement is ContentPage2 page)
            {
                UpdateAndroidStuff();
                page.PlatformUpdate = UpdateAndroidStuff;
            }
        }

        void UpdateAndroidStuff()
        {
            // This whole flow is buggy when done on the fly,
            // also, !T and !L don't go well together, so if it happens, we change it to !T and L

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                var activity = GetActivity(Context);
                var page = Element as ContentPage2;

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
                    if (page.KeepScreenOn.HasValue)
                    {
                        if (page.KeepScreenOn.Value)
                            activity.Window.AddFlags(WindowManagerFlags.KeepScreenOn);
                        else
                            activity.Window.ClearFlags(WindowManagerFlags.KeepScreenOn);
                    }
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


                Thickness cutout = new Thickness(0, statusHeight, 0, softHeight);
                
                if (Build.VERSION.SdkInt >= BuildVersionCodes.P)
                {
                    //check for edge insets
                    //var displayCutout = activity.Window.DecorView.RootWindowInsets.DisplayCutout;
                    Rect rect = new Rect();
                    activity.Window.DecorView.GetWindowVisibleDisplayFrame(rect);
                    cutout.Top = rect.Top / dpi;
                    //if (displayCutout != null)
                    //{
                    //    //cutout = new Thickness(displayCutout.SafeInsetLeft, displayCutout.SafeInsetTop, displayCutout.SafeInsetRight, displayCutout.SafeInsetBottom);
                    //    cutout.Top = displayCutout.SafeInsetTop / dpi;
                    //}
                }

                Signals.AndroidSafeInsetsUpdate.Signal<Thickness>(cutout);

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