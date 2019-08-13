using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RedCorners.Forms
{
    public class ProxyCommand : Element, ICommand
    {
        public event EventHandler CanExecuteChanged;

        public ProxyCommand()
        {

        }

        bool ICommand.CanExecute(object parameter)
        {
            if (!IsEnabled) return false;
            Console.WriteLine($"BindingContext: {BindingContext?.GetType().ToString() ?? "null"}");
            Console.WriteLine($"Parent: {Parent?.GetType().ToString() ?? "null"}");
            if (!UseParentParameter) parameter = CommandParameter;
            else parameter = CommandParameter ?? parameter;
            return Command?.CanExecute(parameter) ?? true;
        }

        void ICommand.Execute(object parameter)
        {
            if (!IsEnabled) return;
            if (!UseParentParameter) parameter = CommandParameter;
            else parameter = CommandParameter ?? parameter;
            Command?.Execute(parameter);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public bool UseParentParameter
        {
            get => (bool)GetValue(UseParentParameterProperty);
            set => SetValue(UseParentParameterProperty, value);
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
                    {
                        cmd.CanExecuteChanged -= proxy.CanExecuteChanged;
                        if (cmd is IElement2 el2) el2.Parent = null;
                        else if (cmd is Element el) el.Parent = null;
                    }
                    if (newVal is ICommand cmd2 && cmd2 != null)
                    {
                        cmd2.CanExecuteChanged += proxy.CanExecuteChanged;
                        if (cmd2 is IElement2 el2) el2.Parent = proxy;
                        else if (cmd2 is Element el) el.Parent = proxy;
                    }

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

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            propertyName: nameof(CommandParameter),
            returnType: typeof(object),
            declaringType: typeof(ProxyCommand),
            defaultValue: null);

        public static readonly BindableProperty UseParentParameterProperty = BindableProperty.Create(
            propertyName: nameof(UseParentParameter),
            returnType: typeof(bool),
            declaringType: typeof(ProxyCommand),
            defaultValue: true);
    }
}
