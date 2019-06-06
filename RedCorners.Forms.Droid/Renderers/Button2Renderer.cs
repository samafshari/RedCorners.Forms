using System;
using System.Collections.Generic;
using System.Text;
using Android.Content;
using Android.Views;
using RedCorners.Forms;
using RedCorners.Forms.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XTextAlignment = Xamarin.Forms.TextAlignment;

[assembly: ExportRenderer(typeof(Button2), typeof(Button2Renderer))]
namespace RedCorners.Forms.Renderers
{
    public class Button2Renderer : ButtonRenderer
    {
        public Button2Renderer(Context context) : base(context)
        {

        }

        public Button2Renderer() : base()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            var button2 = e.NewElement as Button2;

            var thisButton = Control as Android.Widget.Button;
            thisButton.Touch += (object sender, TouchEventArgs args) =>
            {
                if (args.Event.Action == MotionEventActions.Down)
                {
                    button2.OnPressed();
                }
                else if (args.Event.Action == MotionEventActions.Up)
                {
                    button2.OnReleased();
                }
                args.Handled = false;
            };

            if (button2.TextAlignment == XTextAlignment.Center)
                thisButton.Gravity = GravityFlags.CenterVertical | GravityFlags.CenterHorizontal;
            else if (button2.TextAlignment == XTextAlignment.Start)
                thisButton.Gravity = GravityFlags.CenterVertical | GravityFlags.Start;
            else if (button2.TextAlignment == XTextAlignment.End)
                thisButton.Gravity = GravityFlags.CenterVertical | GravityFlags.End;
        }
    }
}