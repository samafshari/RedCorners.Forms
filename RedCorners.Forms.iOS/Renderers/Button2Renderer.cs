using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using RedCorners.Forms;
using RedCorners.Forms.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Button2), typeof(Button2Renderer))]
namespace RedCorners.Forms.Renderers
{
    public class Button2Renderer : ButtonRenderer
    {
        bool firstTime = true;
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            var customButton = e.NewElement as Button2;
            var thisButton = Control as UIButton;

            if (firstTime)
            {
                firstTime = false;
                thisButton.TouchDown += delegate
                {
                    customButton.OnPressed();
                };
                thisButton.TouchUpOutside += delegate
                {
                    customButton.OnReleased();
                };
                thisButton.TouchUpInside += delegate
                {
                    customButton.OnReleased();
                    customButton.OnClicked();
                };
            }

            try
            {
                if (customButton?.TextAlignment == TextAlignment.Center)
                {
                    thisButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
                }
                else if (customButton?.TextAlignment == TextAlignment.Start)
                {
                    thisButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
                }
                else if (customButton?.TextAlignment == TextAlignment.End)
                {
                    thisButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Right;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Custom button null err: {ex}");
            }
        }
    }
}