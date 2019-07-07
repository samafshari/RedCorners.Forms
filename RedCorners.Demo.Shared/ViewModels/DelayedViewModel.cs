using System;
using System.Text;
using System.Linq;
using RedCorners.Forms;
using RedCorners.Models;
using System.Collections.Generic;
using Xamarin.Forms;

namespace RedCorners.Demo.ViewModels
{
    public class DelayedViewModel : BindableModel
    {
        public DelayedViewModel()
        {
            Status = TaskStatuses.Success;
        }

        public Command DelayCommand => new Command(() =>
        {
            System.Threading.Thread.Sleep(10000);
        });
    }
}
