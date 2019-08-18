using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using RedCorners.Forms;
using RedCorners.Forms.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.FastRenderers;
using System.ComponentModel;
using ColorExtensions = Xamarin.Forms.Platform.Android.ColorExtensions;

[assembly: ExportRenderer(typeof(Frame2), typeof(Frame2Renderer))]
namespace RedCorners.Forms.Renderers
{
    public class Frame2Renderer : FrameRenderer
    {
        public Frame2Renderer(Context context) : base(context)
        {

        }

        [Obsolete]
        public Frame2Renderer():base()
        {

        }

        bool _disposed = false;
        protected override void Dispose(bool disposing)
        {
            _disposed = true;
            base.Dispose(disposing);
        }

        void UpdateShadow()
        {
            if ( _disposed)
                return;

            var f2 = Element as Frame2;
            float elevation = f2.ShadowRadius;
            
            if (elevation == -1f)
                elevation = CardElevation;

            if (Element.HasShadow)
                CardElevation = elevation;
            else
                CardElevation = 0f;

            
            this.SetOutlineAmbientShadowColor(ColorExtensions.ToAndroid(f2.ShadowColor));
            this.SetOutlineSpotShadowColor(ColorExtensions.ToAndroid(f2.ShadowColor));
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == Frame2.HasShadowProperty.PropertyName ||
                e.PropertyName == Frame2.ShadowRadiusProperty.PropertyName ||
                e.PropertyName == Frame2.ShadowColorProperty.PropertyName)
                UpdateShadow();
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                UpdateShadow();
            }
        }
    }
}