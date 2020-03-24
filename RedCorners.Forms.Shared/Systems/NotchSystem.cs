using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;

#if __IOS__
using UIKit;
using Foundation;
#endif


namespace RedCorners.Forms.Systems
{
    public class NotchSystem
    {
        public static NotchSystem Instance { get; private set; } = new NotchSystem();

        NotchSystem() {
#if __ANDROID__
            Signals.AndroidSafeInsetsUpdate.Subscribe<Thickness>(this, thickness =>
            {
                AndroidSafeInsets = thickness;
            });
#endif
        }

        public Thickness? OverridePadding { get; set; } = null;
        public Thickness ExtraPadding { get; set; } = new Thickness();

        public Thickness GetOverridePadding() =>
            SumPadding(OverridePadding ?? new Thickness(0, 0, 0, 0),
            ExtraPadding);

        Thickness SumPadding(Thickness t1, Thickness t2) =>
            new Thickness(
                left: t1.Left + t2.Left,
                top: t1.Top + t2.Top,
                right: t1.Right + t2.Right,
                bottom: t1.Bottom + t2.Bottom);

#if __IOS__
        float? top = null;
        float? bottom = null;

        public Thickness GetPageMargin()
        {
            if (OverridePadding.HasValue)
                return GetOverridePadding();

            if (top == null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Console.WriteLine("Calculating safe areas...");
                    var window = UIKit.UIApplication.SharedApplication.Windows.FirstOrDefault();
                    if (window == null) Console.WriteLine($"No window found.");
                    else
                    {
                        top = (float)window.SafeAreaInsets.Top;
                        bottom = HasNotch ? 25 : 0;
                    }
                });
            }
            return SumPadding(
                new Thickness(0, HasNotch ? 40 : 20, 0, bottom.GetValueOrDefault()),
                ExtraPadding);
        }

        public GridLength BottomHeight => new GridLength(GetPageMargin().Bottom);
        public bool HasWindowInformation => top.HasValue || OverridePadding.HasValue;
#elif __ANDROID__
        Thickness AndroidSafeInsets = new Thickness();
        public bool HasWindowInformation => true;
        public GridLength BottomHeight => new GridLength(GetPageMargin().Bottom);
        public Thickness GetPageMargin()
        {
            return SumPadding(OverridePadding ?? AndroidSafeInsets, ExtraPadding);
        }
#else
        public bool HasWindowInformation => true;
        public GridLength BottomHeight => new GridLength(GetPageMargin().Bottom);

        public Thickness GetPageMargin() => SumPadding(OverridePadding ?? new Thickness(0, 25, 0, 0), ExtraPadding);
#endif

        public Thickness TopMargin => new Thickness(0, GetPageMargin().Top, 0, 0);

        public Thickness BottomMargin => new Thickness(0, 0, 0, GetPageMargin().Bottom);

        public bool HasNotch
        {
            get
            {
#if __IOS__
                var window = UIKit.UIApplication.SharedApplication.Windows.FirstOrDefault();
                if (window != null)
                {
                    SafeAreaInsets = new Thickness
                    (
                        window.SafeAreaInsets.Left,
                        window.SafeAreaInsets.Top,
                        window.SafeAreaInsets.Right,
                        window.SafeAreaInsets.Bottom
                    );
                }
                if ((window?.SafeAreaInsets.Top ?? 20) > 20)
                {
                    return true;
                }
#endif
                return false;
            }
        }

#if __IOS__
        public Thickness SafeAreaInsets { get; set; }
#endif
    }
}
