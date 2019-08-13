using RedCorners.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RedCorners.Demo.ViewModels
{
    public class CommandsViewModel : BindableModel
    {
        public Command<object> MessageCommand => new Command<object>(s =>
            App.Instance.DisplayAlert("Message", s?.ToString(), "OK"));
    }
}
