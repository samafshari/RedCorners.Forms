using RedCorners.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RedCorners.Demo.ViewModels
{
    public class CounterViewModel : BindableModel
    {
        public int Count { get; set; }

        bool _isModal = false;
        public override bool IsModal
        {
            get => _isModal;
            set => SetProperty(ref _isModal, value);
        }

        public Command CountCommand => new Command(() =>
        {
            Count++;
            UpdateProperties();
        });

        public override void OnBind(BindableObject bindable)
        {
            base.OnBind(bindable);
            Console.WriteLine($"OnBind: {bindable.GetType().FullName}");
        }

        public override void OnUnbind(BindableObject bindable)
        {
            Console.WriteLine($"OnUnbind: {bindable.GetType().FullName}");
            base.OnUnbind(bindable);
        }

        public override void OnStart()
        {
            base.OnStart();
            Console.WriteLine($"OnStart");

        }

        public override void OnStop()
        {
            Console.WriteLine($"OnStop");
            base.OnStop();
        }

        public override void OnAppeared(ContentPage page)
        {
            base.OnAppeared(page);
            Console.WriteLine($"OnAppeared: {page.GetType().FullName}");
        }

        public override Command GoBackCommand => new Command(() =>
        {
            if (IsModal) base.GoBackCommand.Execute(this);
            else IsSidebarOpen = true;
        });
    }
}
