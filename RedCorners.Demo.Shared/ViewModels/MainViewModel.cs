using RedCorners.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Reflection;

namespace RedCorners.Demo.ViewModels
{
    public class MainViewModel : BindableModel
    {
        bool _androidTranslucentStatus = true;
        public bool AndroidTranslucentStatus
        {
            get => _androidTranslucentStatus;
            set => SetProperty(ref _androidTranslucentStatus, value);
        }

        Color _androidStatusBarColor = Color.Red;
        public Color AndroidStatusBarColor
        {
            get => _androidStatusBarColor;
            set => SetProperty(ref _androidStatusBarColor, value);
        }

        public Command BlueStatusBarCommand => new Command(() => AndroidStatusBarColor = Color.FromHex("#770000FF"));
        public Command GreenStatusBarCommand => new Command(() => AndroidStatusBarColor = Color.FromHex("#7700FF00"));

        public ImageSource BackgroundImage => "https://images.pexels.com/photos/163822/color-umbrella-red-yellow-163822.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260";
    }
}
