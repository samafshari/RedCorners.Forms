using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RedCorners.Forms
{
    public class WorkerPage : ContentPage
    {
        readonly Task task;

        public WorkerPage(Task task)
        {
            this.task = task;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(() => task);
        }
    }
}
