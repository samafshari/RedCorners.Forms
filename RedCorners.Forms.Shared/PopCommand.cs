using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RedCorners.Forms
{
    public class PopCommand : BindableObject, ICommand
    {
        public event EventHandler CanExecuteChanged;
        bool executed = false;
        DateTimeOffset lastFire = DateTimeOffset.MinValue;

        public static BindableProperty FireOnceProperty = BindableProperty.Create(
            nameof(FireOnce),
            typeof(bool),
            typeof(PopCommand),
            defaultValue: true,
            defaultBindingMode: BindingMode.TwoWay);

        public bool FireOnce
        {
            get => (bool)GetValue(FireOnceProperty);
            set => SetValue(FireOnceProperty, value);
        }

        public static BindableProperty FireDelayProperty = BindableProperty.Create(
            nameof(FireDelay),
            typeof(double),
            typeof(PopCommand),
            defaultValue: 1000.0,
            defaultBindingMode: BindingMode.TwoWay);

        public double FireDelay
        {
            get => (double)GetValue(FireDelayProperty);
            set => SetValue(FireDelayProperty, value);
        }

        bool CanExecute(object _)
        {
            if (executed && FireOnce) return false;
            if (!FireOnce && FireDelay > 0 && (DateTimeOffset.Now - lastFire).TotalMilliseconds < FireDelay) return false;
            return true;
        }

        void ICommand.Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;
            executed = true;
            CanExecuteChanged?.Invoke(this, null);
            lastFire = DateTimeOffset.Now;
            Signals.PopModal.Signal();
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(parameter);
        }
    }
}
