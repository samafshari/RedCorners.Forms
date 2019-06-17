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

            if (ViewModelType != null)
                ViewModel = Activator.CreateInstance(ViewModelType);

            if (ViewModel != null && ViewModelConfiguration != null)
                InjectExtensions.Inject((IDictionary<string, object>)ViewModelConfiguration, ViewModel);

            if (ViewModel != null)
                Page.BindingContext = ViewModel;

            if (IsModal) Signals.ShowModalPage.Signal(Page);
            else Signals.ShowPage.Signal(Page);
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

        public object ViewModel
        {
            get => (object)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        public static BindableProperty ViewModelProperty = BindableProperty.Create(
            nameof(ViewModel),
            typeof(object),
            typeof(PageCommand),
            defaultBindingMode: BindingMode.TwoWay);

        public Type ViewModelType
        {
            get => (Type)GetValue(ViewModelTypeProperty);
            set => SetValue(ViewModelTypeProperty, value);
        }

        public static BindableProperty ViewModelTypeProperty = BindableProperty.Create(
            nameof(ViewModelType),
            typeof(Type),
            typeof(PageCommand),
            defaultBindingMode: BindingMode.TwoWay);

        public ResourceDictionary ViewModelConfiguration
        {
            get => (ResourceDictionary)GetValue(ViewModelConfigurationProperty);
            set => SetValue(ViewModelConfigurationProperty, value);
        }

        public static BindableProperty ViewModelConfigurationProperty = BindableProperty.Create(
            nameof(ViewModelConfiguration),
            typeof(ResourceDictionary),
            typeof(PageCommand),
            defaultBindingMode: BindingMode.TwoWay);
    }
}
