using System;
using System.Text;
using System.Linq;
using RedCorners.Forms;
using RedCorners.Models;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Demos.ViewModels
{
    public class MainViewModel : BindableModel
    {
        public override bool IsModal => false;

        public MainViewModel()
        {
            Status = TaskStatuses.Success;
            Count = Settings.Instance.Count;
        }

        public int Count { get; set; }

        public Command CountCommand => new Command(() =>
        {
            // Increase the count
            Count++;
            UpdateProperties();

            // Store it in settings and save
            Settings.Instance.Count = Count;
            Signals.SaveSettings.Signal();
        });
    }
}