using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RedCorners.Demo.ViewModels
{
    public class CounterViewModel : BindableModel
    {
        public int Count { get; set; }

        public Command CountCommand => new Command(() =>
        {
            Count++;
            UpdateProperties();
        });
    }
}
