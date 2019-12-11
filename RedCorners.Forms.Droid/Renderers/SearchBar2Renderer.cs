using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using RedCorners.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(SearchBar2), typeof(RedCorners.Forms.Renderers.SearchBar2Renderer))]
namespace RedCorners.Forms.Renderers
{
    public class SearchBar2Renderer : Xamarin.Forms.Platform.Android.SearchBarRenderer
    {
        [Obsolete]
        public SearchBar2Renderer() { }

        public SearchBar2Renderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }
    }
}