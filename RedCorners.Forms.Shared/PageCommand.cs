using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RedCorners.Forms
{
    [ContentProperty("Page")]
    public class PageCommand : BindableObject, ICommand
    {
        public event EventHandler CanExecuteChanged;

        public PageCommand()
        {
        }

        bool ICommand.CanExecute(object parameter) => Page != null || PageType != null;

        void ICommand.Execute(object parameter)
        {
            if (PageType != null)
            {
                Page = Activator.CreateInstance(PageType) as Page;
                if (Page == null)
                    throw new Exception("PageType did not construct a Page!");
            }
            if (IsModal) Signals.ShowModalPage.Send(Page);
            else Signals.ShowPage.Send(Page);
        }

        public static BindableProperty PageProperty = BindableProperty.Create(
            nameof(Page),
            typeof(Page),
            typeof(PageCommand),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is PageCommand command)
                {
                    command.CanExecuteChanged?.Invoke(command, null);
                }
            });

        public Page Page
        {
            get => (Page)GetValue(PageProperty);
            set => SetValue(PageProperty, value);
        }

        public static BindableProperty PageTypeProperty = BindableProperty.Create(
            nameof(PageType),
            typeof(Type),
            typeof(PageCommand),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is PageCommand command)
                {
                    command.CanExecuteChanged?.Invoke(command, null);
                }
            });

        public Type PageType
        {
            get => (Type)GetValue(PageTypeProperty);
            set => SetValue(PageTypeProperty, value);
        }

        public static BindableProperty IsModalProperty = BindableProperty.Create(
            nameof(IsModal),
            typeof(bool),
            typeof(PageCommand),
            defaultValue: true,
            defaultBindingMode: BindingMode.TwoWay);

        public bool IsModal
        {
            get => (bool)GetValue(IsModalProperty);
            set => SetValue(IsModalProperty, value);
        }
    }
}
