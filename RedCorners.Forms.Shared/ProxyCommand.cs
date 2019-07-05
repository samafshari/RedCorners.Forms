using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RedCorners.Forms
{
    public class ProxyCommand : BindableObject, ICommand
    {
        public event EventHandler CanExecuteChanged;

        public ProxyCommand()
        {

        }

        bool ICommand.CanExecute(object parameter)
        {
            if (!IsEnabled) return false;
            return Command?.CanExecute(parameter) ?? true;
        }

        void ICommand.Execute(object parameter)
        {
            if (!IsEnabled) return;
            Command?.Execute(parameter);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            propertyName: nameof(Command),
            returnType: typeof(ICommand),
            declaringType: typeof(ProxyCommand),
            defaultValue: null,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is ProxyCommand proxy)
                {
                    if (oldVal is ICommand cmd && cmd != null)
                        cmd.CanExecuteChanged -= proxy.CanExecuteChanged;
                    if (newVal is ICommand cmd2 && cmd2 != null)
                        cmd2.CanExecuteChanged += proxy.CanExecuteChanged;

                    proxy.CanExecuteChanged?.Invoke(bindable, new EventArgs());
                }
            });

        public static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(
            propertyName: nameof(IsEnabled),
            returnType: typeof(bool),
            declaringType: typeof(ProxyCommand),
            defaultValue: true,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is ProxyCommand proxy)
                    proxy.CanExecuteChanged?.Invoke(bindable, new EventArgs());
            });

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }

        void PropagateBindingContext()
        {
        }
        
    }
}
