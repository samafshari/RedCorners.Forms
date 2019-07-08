using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RedCorners.Forms
{
    public class SearchBar2 : SearchBar
    {
        public static readonly BindableProperty CancelCommandProperty = BindableProperty.Create(
            nameof(CancelCommand),
            typeof(ICommand),
            typeof(SearchBar2),
            null);

        public static readonly BindableProperty CancelCommandParameterProperty = BindableProperty.Create(
            nameof(CancelCommandParameter),
            typeof(object),
            typeof(SearchBar2),
            null);

        public static readonly BindableProperty IsCancelVisibleProperty = BindableProperty.Create(
            nameof(IsCancelVisible),
            typeof(bool),
            typeof(SearchBar2),
            true);

        public static readonly BindableProperty TextChangeActionProperty = BindableProperty.Create(
            nameof(TextChangeAction),
            typeof(Action<string>),
            typeof(SearchBar2),
            null);

        public ICommand CancelCommand
        {
            get => (ICommand)GetValue(CancelCommandProperty);
            set => SetValue(CancelCommandProperty, value);
        }

        public object CancelCommandParameter
        {
            get => GetValue(CancelCommandParameterProperty);
            set => SetValue(CancelCommandParameterProperty, value);
        }

        public bool IsCancelVisible
        {
            get => (bool)GetValue(IsCancelVisibleProperty);
            set => SetValue(IsCancelVisibleProperty, value);
        }

        public Action<string> TextChangeAction
        {
            get => (Action<string>)GetValue(TextChangeActionProperty);
            set => SetValue(TextChangeActionProperty, value);
        }

        public SearchBar2()
        {
            TextChanged += SearchBar2_TextChanged;
        }

        private void SearchBar2_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextChangeAction?.Invoke(e.NewTextValue);
        }
    }
}
