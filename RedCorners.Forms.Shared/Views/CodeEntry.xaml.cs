using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedCorners.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CodeEntry
    {
        public CodeEntry()
        {
            InitializeComponent();
            tap.Tapped += Tap_Tapped;
            textBox.Focused += TextBox_Focused;
            UpdateKeyboard();
            BuildUI();
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(CodeEntry),
            "",
            BindingMode.TwoWay);

        public static readonly BindableProperty LengthProperty = BindableProperty.Create(
            nameof(Length),
            typeof(int),
            typeof(CodeEntry),
            4,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (oldVal != newVal && bindable is CodeEntry view)
                    view.BuildUI();
            });

        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(
            nameof(Keyboard),
            typeof(Keyboard),
            typeof(CodeEntry),
            Keyboard.Numeric,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is CodeEntry view)
                    view.UpdateKeyboard();
            });

        public static readonly BindableProperty FinishCommandProperty = BindableProperty.Create(
            nameof(FinishCommand),
            typeof(ICommand),
            typeof(CodeEntry),
            null,
            BindingMode.TwoWay);


        public static readonly BindableProperty FinishCommandParameterProperty = BindableProperty.Create(
            nameof(FinishCommandParameter),
            typeof(object),
            typeof(CodeEntry),
            null,
            BindingMode.TwoWay);


        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public int Length
        {
            get => (int)GetValue(LengthProperty);
            set => SetValue(LengthProperty, value);
        }

        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        public ICommand FinishCommand
        {
            get => (ICommand)GetValue(FinishCommandProperty);
            set => SetValue(FinishCommandProperty, value);
        }

        public object FinishCommandParameter
        {
            get => GetValue(FinishCommandParameterProperty);
            set => SetValue(FinishCommandParameterProperty, value);
        }

        private void TextBox_Focused(object sender, FocusEventArgs e)
        {
            textBox.Text = "";
        }

        private void Tap_Tapped(object sender, EventArgs e)
        {
            textBox.Focus();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = textBox.Text;
            UpdateUI();
        }

        void BuildUI()
        {
            stack.Children.Clear();
            for (int i = 0; i < Length; i++)
            {
                stack.Children.Add(new Entry());
            }
        }

        void UpdateUI()
        {
            var entries = stack.Children.Select(x => x as Entry).ToArray();
            if (textBox.Text == null)
                foreach (var entry in entries)
                    entry.Text = "";
            else
            {
                if (textBox.Text.Length >= entries.Length)
                {
                    textBox.Text = textBox.Text.Substring(0, entries.Length);
                    if (textBox.IsFocused)
                    {
                        textBox.Unfocus();
                        if (FinishCommand?.CanExecute(FinishCommandParameter) ?? false)
                            FinishCommand?.Execute(FinishCommandParameter);
                    }
                }

                for (int i = 0; i < entries.Length; i++)
                {
                    entries[i].Text = textBox.Text.Length > i ? textBox.Text[i].ToString() : "";
                }
            }
        }

        public new void Focus()
        {
            textBox.Focus();
        }

        void UpdateKeyboard()
        {
            if (textBox == null) return;
            textBox.Keyboard = Keyboard;
        }
    }
}