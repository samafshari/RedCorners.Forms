using System;
using System.Text;
using System.Linq;
using RedCorners.Forms;
using RedCorners.Models;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace RedCorners.Demo.ViewModels
{
    public class DelayedViewModel : BindableModel
    {
        public DelayedViewModel()
        {
            Status = TaskStatuses.Success;
        }

        public Func<object, Task> DelayCommand =>
            (o) => Task.Delay(10000);
    }
}
