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

#if __IOS__
        float? top = null;
        float? bottom = null;

        public Thickness GetPageMargin()
        {
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
            return new Thickness(0, HasNotch ? 40 : 20, 0, bottom.GetValueOrDefault());
        }

        public Thickness TopMargin => new Thickness(0, HasNotch ? 40 : 20, 0, 0);

        public GridLength BottomHeight => new GridLength(bottom ?? 0);

        public bool HasWindowInformation => top.HasValue;
#else
        public Thickness TopMargin => new Thickness(0, 0, 0, 0);
        public bool HasWindowInformation => true;
        public GridLength BottomHeight => new GridLength(0);

        public Thickness GetPageMargin() => new Thickness(0, 0, 0, 0);
#endif

        public Thickness BottomMargin => new Thickness(0, 0, 0, BottomHeight.Value);

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
