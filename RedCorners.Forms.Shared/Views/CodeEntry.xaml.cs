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
            Unfocused += CodeEntry_Unfocused;
            UpdateKeyboard();
            BuildUI();
        }

        private void CodeEntry_Unfocused(object sender, FocusEventArgs e)
        {
            textBox?.Unfocus();
        }

        public static readonly BindableProperty BoxTypeProperty = BindableProperty.Create(
            nameof(BoxType),
            typeof(Type),
            typeof(CodeEntry),
            typeof(CodeEntryBoxView),
            BindingMode.OneWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (oldVal != newVal && bindable is CodeEntry view)
                    view.BuildUI();
            });

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
            BindingMode.OneWay,
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
            BindingMode.OneWay,
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
            BindingMode.OneWay);


        public static readonly BindableProperty FinishCommandParameterProperty = BindableProperty.Create(
            nameof(FinishCommandParameter),
            typeof(object),
            typeof(CodeEntry),
            null,
            BindingMode.OneWay);

        public static readonly BindableProperty EntryBackgroundColorProperty = BindableProperty.Create(
            nameof(EntryBackgroundColor),
            typeof(Color),
            typeof(CodeEntry),
            Color.White,
            BindingMode.OneWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is CodeEntry view)
                    view.UpdateColors();
            });

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            nameof(TextColor),
            typeof(Color),
            typeof(CodeEntry),
            Color.Black,
            BindingMode.OneWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is CodeEntry view)
                    view.UpdateColors();
            });

        public static readonly BindableProperty SpacingProperty = BindableProperty.Create(
            nameof(Spacing),
            typeof(double),
            typeof(CodeEntry),
            8.0,
            BindingMode.OneWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is CodeEntry view)
                    view.UpdateUI();
            });

        public Type BoxType
        {
            get => (Type)GetValue(BoxTypeProperty);
            set => SetValue(BoxTypeProperty, value);
        }

        public double Spacing
        {
            get => (double)GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }
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

        public Color EntryBackgroundColor
        {
            get => (Color)GetValue(EntryBackgroundColorProperty);
            set => SetValue(EntryBackgroundColorProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
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
            stack.Spacing = Spacing;
            for (int i = 0; i < Length; i++)
            {
                var box = Activator.CreateInstance(BoxType) as CodeEntryBoxViewBase;
                box.BackgroundColor = EntryBackgroundColor;
                box.TextColor = TextColor;
                stack.Children.Add(box);
            }
        }

        void UpdateUI()
        {
            var entries = stack.Children.Select(x => x as CodeEntryBoxViewBase).ToArray();
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

            if (stack != null)
                stack.Spacing = Spacing;
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

        void UpdateColors()
        {
            var entries = stack.Children.Select(x => x as Entry).ToArray();
            foreach (var entry in entries)
            {
                entry.BackgroundColor = EntryBackgroundColor;
                entry.TextColor = TextColor;
            }
        }
    }
}