using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RedCorners.Forms
{
    public class ListView2 : ListView
    {
        public ListView2() : base(ListViewCachingStrategy.RecycleElement)
        {
        }
    }
}
