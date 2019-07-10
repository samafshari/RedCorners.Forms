using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Foundation;
using RedCorners.Forms;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SearchBar2), typeof(RedCorners.Forms.Renderers.SearchBar2Renderer))]
namespace RedCorners.Forms.Renderers
{
    public class SearchBar2Renderer : Xamarin.Forms.Platform.iOS.SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                Control.CancelButtonClicked += Control_CancelButtonClicked;
                Control.ShowsCancelButton = (e.NewElement as SearchBar2).IsCancelVisible;
            }
        }

        private void Control_CancelButtonClicked(object sender, EventArgs e)
        {
            var el2 = Element as SearchBar2;
            if (el2?.CancelCommand?.CanExecute(el2?.CancelCommandParameter) ?? false)
                el2.CancelCommand.Execute(el2.CancelCommandParameter);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control != null && Element as SearchBar2 != null)
                Control.ShowsCancelButton = (Element as SearchBar2).IsCancelVisible;
        }
    }
}