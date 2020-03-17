using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using RedCorners.Forms.iOS.Renderers;
using RedCorners.Forms.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(DatePicker2), typeof(DatePicker2Renderer))]
namespace RedCorners.Forms.iOS.Renderers
{
    public class DatePicker2Renderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                e.OldElement.PropertyChanged -= NewElement_PropertyChanged;
            }
            if (e.NewElement != null)
            {
                e.NewElement.PropertyChanged += NewElement_PropertyChanged;

                UpdateAlignment(e.NewElement as DatePicker2);
            }
        }

        private void NewElement_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == DatePicker2.TextAlignmentProperty.PropertyName)
                UpdateAlignment(Element as DatePicker2);
        }

        void UpdateAlignment(DatePicker2 p)
        {
            if (p.TextAlignment == TextAlignment.Center)
                Control.TextAlignment = UITextAlignment.Center;
            else if (p.TextAlignment == TextAlignment.Start)
                Control.TextAlignment = UITextAlignment.Left;
            else
                Control.TextAlignment = UITextAlignment.Right;
        }
    }
}