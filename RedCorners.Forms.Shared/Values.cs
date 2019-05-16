using RedCorners.Forms.Systems;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RedCorners.Forms
{
    public static class Values
    {
        public static Thickness PageMargin => NotchSystem.Instance.GetPageMargin();
        public static Thickness TopMargin => NotchSystem.Instance.TopMargin;
        public static Thickness BottomMargin => NotchSystem.Instance.BottomMargin;
    }
}
