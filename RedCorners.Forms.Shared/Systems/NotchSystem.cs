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

        NotchSystem() { }

        public Thickness? OverridePadding { get; set; } = null;
        public Thickness ExtraPadding { get; private set; } = new Thickness();

        Thickness GetOverridePadding() =>
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

        public GridLength BottomHeight => new GridLength((bottom ?? 0) + ExtraPadding.Bottom);

        public bool HasWindowInformation => top.HasValue || OverridePadding.HasValue;
#else
        public bool HasWindowInformation => true;
        public GridLength BottomHeight => new GridLength(ExtraPadding.Bottom);

        public Thickness GetPageMargin() => GetOverridePadding();
#endif

        public Thickness TopMargin => new Thickness(0, GetPageMargin().Top, 0, 0);

        public Thickness BottomMargin => new Thickness(0, 0, 0, GetPageMargin().Bottom);

        public bool HasNotch
        {
            get
            {
#if __IOS__
                var window = UIKit.UIApplication.SharedApplication.Windows.FirstOrDefault();
                if ((window?.SafeAreaInsets.Top ?? 20) > 20)
                {
                    return true;
                }
#endif
                return false;
            }
        }
    }
}
