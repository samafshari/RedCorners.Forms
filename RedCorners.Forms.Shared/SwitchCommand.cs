using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

using Xamarin.Forms;

namespace RedCorners.Forms
{
    public class SwitchCommand : BindableObject, ICommand
    {
        public static SwitchCommand Instance { get; } = new SwitchCommand();

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is Switch sw)
                sw.IsToggled = !sw.IsToggled;
        }
    }
}
