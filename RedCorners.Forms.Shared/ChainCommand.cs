using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace RedCorners.Forms
{
    public enum ExecuteCommandPolicies
    {
        AllNoTest,
        AllWithTest,
        StopIfFail,
        FirstSuccessOnly
    }

    public enum CanExecuteCommandPolicies
    {
        All,
        Any,
        Custom
    }

    [ContentProperty("Items")]
    public class ChainCommand : BindableObject, ICommand
    {
        public event EventHandler CanExecuteChanged;

        public ChainCommand()
        {
            Items = new ObservableCollection<ICommand>();
        }

        bool ICommand.CanExecute(object parameter)
        {
            if (Items == null || Items.Count == 0) return false;

            if (CanExecutePolicy == CanExecuteCommandPolicies.Custom)
                return CustomCanExecute?.Invoke(parameter) ?? false;
            else if (CanExecutePolicy == CanExecuteCommandPolicies.Any)
                return Items.Any(x => x.CanExecute(parameter));
            else if (CanExecutePolicy == CanExecuteCommandPolicies.All)
                return Items.All(x => x.CanExecute(parameter));

            return true;
        }

        void ICommand.Execute(object parameter)
        {
            if (Items == null || Items.Count == 0) return;

            foreach (var item in Items)
            {
                if (ExecutePolicy == ExecuteCommandPolicies.AllNoTest)
                    item?.Execute(parameter);
                else if (ExecutePolicy == ExecuteCommandPolicies.AllWithTest)
                {
                    if (item?.CanExecute(parameter) ?? true)
                        item.Execute(parameter);
                }
                else if (ExecutePolicy == ExecuteCommandPolicies.FirstSuccessOnly)
                {
                    if (item?.CanExecute(parameter) ?? true)
                    {
                        item.Execute(parameter);
                        return;
                    }
                }
                else if (ExecutePolicy == ExecuteCommandPolicies.StopIfFail)
                {
                    if (item?.CanExecute(parameter) ?? true)
                    {
                        item.Execute(parameter);
                    }
                    else return;
                }
            }
        }

        public IList<ICommand> Items
        {
            get => (IList<ICommand>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public CanExecuteCommandPolicies CanExecutePolicy
        {
            get => (CanExecuteCommandPolicies)GetValue(CanExecutePolicyProperty);
            set => SetValue(CanExecutePolicyProperty, value);
        }

        public ExecuteCommandPolicies ExecutePolicy
        {
            get => (ExecuteCommandPolicies)GetValue(ExecutePolicyProperty);
            set => SetValue(ExecutePolicyProperty, value);
        }

        public Func<object, bool> CustomCanExecute
        {
            get => (Func<object, bool>)GetValue(CustomCanExecuteProperty);
            set => SetValue(CustomCanExecuteProperty, value);
        }

        //public bool TestBeforeExecute
        //{
        //    get => (bool)GetValue(TestBeforeExecuteProperty);
        //    set => SetValue(TestBeforeExecuteProperty, value);
        //}

        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
            propertyName: nameof(Items),
            returnType: typeof(IList<ICommand>),
            declaringType: typeof(ChainCommand),
            defaultValue: null,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is ChainCommand chain)
                {
                    if (oldVal is IList<ICommand> olds && olds != null)
                        foreach (var item in olds)
                            item.CanExecuteChanged -= chain.CanExecuteChanged;
                    if (newVal is IList<ICommand> news && news != null)
                        foreach (var item in news)
                        {
                            item.CanExecuteChanged += chain.CanExecuteChanged;
                                
                        }

                    chain.PropagateBindingContext();
                    chain.CanExecuteChanged?.Invoke(bindable, new EventArgs());
                }
            });

        public static readonly BindableProperty CanExecutePolicyProperty = BindableProperty.Create(
            propertyName: nameof(CanExecutePolicy),
            returnType: typeof(CanExecuteCommandPolicies),
            declaringType: typeof(ChainCommand),
            defaultValue: default(CanExecuteCommandPolicies),
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is ChainCommand chain)
                    chain.CanExecuteChanged?.Invoke(bindable, new EventArgs());
            });

        public static readonly BindableProperty ExecutePolicyProperty = BindableProperty.Create(
            propertyName: nameof(ExecutePolicy),
            returnType: typeof(ExecuteCommandPolicies),
            declaringType: typeof(ChainCommand),
            defaultValue: default(ExecuteCommandPolicies),
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is ChainCommand chain)
                    chain.CanExecuteChanged?.Invoke(bindable, new EventArgs());
            });

        public static readonly BindableProperty CustomCanExecuteProperty = BindableProperty.Create(
            propertyName: nameof(CustomCanExecute),
            returnType: typeof(Func<object, bool>),
            declaringType: typeof(ChainCommand),
            defaultValue: null,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is ChainCommand chain)
                    chain.CanExecuteChanged?.Invoke(bindable, new EventArgs());
            });

        object oldContext = null;

        void PropagateBindingContext()
        {
            foreach (var item in Items)
            {
                if (item is BindableObject bo && (bo.BindingContext == null || bo.BindingContext == oldContext))
                    bo.BindingContext = item;
            }
            oldContext = BindingContext;
        }
        
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            PropagateBindingContext();
        }

        //public static readonly BindableProperty TestBeforeExecuteProperty = BindableProperty.Create(
        //    propertyName: nameof(TestBeforeExecute),
        //    returnType: typeof(bool),
        //    declaringType: typeof(ChainCommand),
        //    defaultValue: true,
        //    propertyChanged: (bindable, oldVal, newVal) =>
        //    {

        //    });
    }
}
