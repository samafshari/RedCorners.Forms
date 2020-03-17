using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RedCorners.Forms.Renderers;
using RedCorners.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using DatePicker = Xamarin.Forms.DatePicker;

[assembly: ExportRenderer(typeof(DatePicker2), typeof(DatePicker2Renderer))]
namespace RedCorners.Forms.Renderers
{
    public class DatePicker2Renderer : DatePickerRenderer
    {
        public DatePicker2Renderer(Context context) : base(context)
        {

        }

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
            if (p.TextAlignment == Xamarin.Forms.TextAlignment.Center)
                Control.Gravity = GravityFlags.CenterHorizontal;
            else if (p.TextAlignment == Xamarin.Forms.TextAlignment.Start)
                Control.Gravity = GravityFlags.Left;
            else
                Control.Gravity = GravityFlags.Right;
        }
    }
}