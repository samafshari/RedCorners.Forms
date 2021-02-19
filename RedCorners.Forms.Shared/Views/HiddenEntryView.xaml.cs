using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RedCorners;
using RedCorners.Forms;
using System.Windows.Input;

namespace RedCorners.Forms
{
    [Obsolete("Use HiddenEntry instead")]
    public class HiddenEntryView : HiddenEntry { }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HiddenEntry
    {
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public Color EntryBackgroundColor
        {
            get => (Color)GetValue(EntryBackgroundColorProperty);
            set => SetValue(EntryBackgroundColorProperty, value);
        }

        public Color EntryTextColor
        {
            get => (Color)GetValue(EntryTextColorProperty);
            set => SetValue(EntryTextColorProperty, value);
        }

        public Color PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }

        public Color LabelTextColor
        {
            get => (Color)GetValue(LabelTextColorProperty);
            set => SetValue(LabelTextColorProperty, value);
        }

        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public FontAttributes FontAttributes
        {
            get => (FontAttributes)GetValue(FontAttributesProperty);
            set => SetValue(FontAttributesProperty, value);
        }

        public TextAlignment EntryTextAlignment
        {
            get => (TextAlignment)GetValue(EntryTextAlignmentProperty);
            set => SetValue(EntryTextAlignmentProperty, value);
        }

        public TextAlignment LabelTextAlignment
        {
            get => (TextAlignment)GetValue(LabelTextAlignmentProperty);
            set => SetValue(LabelTextAlignmentProperty, value);
        }

        public Thickness LabelMargin
        {
            get => (Thickness)GetValue(LabelMarginProperty);
            set => SetValue(LabelMarginProperty, value);
        }

        [TypeConverter(typeof(FontSizeConverter))]
        public double EntryFontSize
        {
            get => (double)GetValue(EntryFontSizeProperty);
            set => SetValue(EntryFontSizeProperty, value);
        }

        [TypeConverter(typeof(FontSizeConverter))]
        public double LabelFontSize
        {
            get => (double)GetValue(LabelFontSizeProperty);
            set => SetValue(LabelFontSizeProperty, value);
        }

        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        public ICommand SubmitCommand
        {
            get => (ICommand)GetValue(SubmitCommandProperty);
            set => SetValue(SubmitCommandProperty, value);
        }

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public Action<object> UnfocusAction
        {
            get => (Action<object>)GetValue(UnfocusActionProperty);
            set => SetValue(UnfocusActionProperty, value);
        }

        public bool IsDate
        {
            get => (bool)GetValue(IsDateProperty);
            set => SetValue(IsDateProperty, value);
        }

        public DateTime Date
        {
            get => (DateTime)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }
        
        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text), typeof(string), typeof(HiddenEntry), defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
            nameof(IsPassword), typeof(bool), typeof(HiddenEntry), false, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder), typeof(string), typeof(HiddenEntry), defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty LabelProperty = BindableProperty.Create(
            nameof(Label), typeof(string), typeof(HiddenEntry), defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty EntryBackgroundColorProperty = BindableProperty.Create(
            nameof(EntryBackgroundColor), typeof(Color), typeof(HiddenEntry), Color.White, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty EntryTextColorProperty = BindableProperty.Create(
            nameof(EntryTextColor), typeof(Color), typeof(HiddenEntry), Color.Black, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(
            nameof(PlaceholderColor), typeof(Color), typeof(HiddenEntry), Color.Gray, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty LabelTextColorProperty = BindableProperty.Create(
            nameof(LabelTextColor), typeof(Color), typeof(HiddenEntry), Color.Black, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
            nameof(FontFamily), typeof(string), typeof(HiddenEntry), defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(
            nameof(FontAttributes), typeof(FontAttributes), typeof(HiddenEntry), FontAttributes.None, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty EntryTextAlignmentProperty = BindableProperty.Create(
            nameof(EntryTextAlignment), typeof(TextAlignment), typeof(HiddenEntry), TextAlignment.End, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty LabelTextAlignmentProperty = BindableProperty.Create(
            nameof(LabelTextAlignment), typeof(TextAlignment), typeof(HiddenEntry), TextAlignment.End, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty LabelMarginProperty = BindableProperty.Create(
            nameof(LabelMargin), typeof(Thickness), typeof(HiddenEntry), new Thickness(), defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty EntryFontSizeProperty = BindableProperty.Create(
            nameof(EntryFontSize), typeof(double), typeof(HiddenEntry), 14.0, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty LabelFontSizeProperty = BindableProperty.Create(
            nameof(LabelFontSize), typeof(double), typeof(HiddenEntry), 14.0, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(
            nameof(Keyboard), typeof(Keyboard), typeof(HiddenEntry), Keyboard.Default, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty SubmitCommandProperty = BindableProperty.Create(
            nameof(SubmitCommand), typeof(ICommand), typeof(HiddenEntry), default(ICommand), defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty UnfocusActionProperty = BindableProperty.Create(
            nameof(UnfocusAction), typeof(Action<object>), typeof(HiddenEntry), default(Action<object>), defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty IsDateProperty = BindableProperty.Create(
            nameof(IsDate), typeof(bool), typeof(HiddenEntry), default(bool), defaultBindingMode: BindingMode.TwoWay);
        
        public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(
            nameof(IsReadOnly), typeof(bool), typeof(HiddenEntry), default(bool), defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty DateProperty = BindableProperty.Create(
            nameof(Date), typeof(DateTime), typeof(HiddenEntry), DateTime.Now.Date, defaultBindingMode: BindingMode.TwoWay);



        public HiddenEntry()
        {
            InitializeComponent();

            entry.IsVisible = false;
            datePicker.IsVisible = false;

            label.InputTransparent = true;
            entry.Unfocused += Entry_Unfocused;
            datePicker.Unfocused += DatePicker_Unfocused;

            var tap = new TapGestureRecognizer();
            tap.Tapped += Tap_Tapped;
            GestureRecognizers.Add(tap);

            entry.BindingContext = this;
            label.BindingContext = this;
            datePicker.BindingContext = this;
        }

        public new event EventHandler<FocusEventArgs> Focused;
        public new event EventHandler Unfocused;

        private void Tap_Tapped(object sender, EventArgs e)
        {
            Focus();
            Focused?.Invoke(sender, new FocusEventArgs(this, true));
        }

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            Unfocus();
            Unfocused?.Invoke(sender, e);
        }

        private void DatePicker_Unfocused(object sender, FocusEventArgs e)
        {
            Unfocus();
            Unfocused?.Invoke(sender, e);
        }

        public new void Focus()
        {
            if (IsReadOnly) return;

            label.IsVisible = false;
            if (IsDate)
            {
                datePicker.IsVisible = true;
                datePicker.Focus();
            }
            else
            {
                entry.IsVisible = true;
                entry.Focus();
            }
        }

        public new void Unfocus()
        {
            entry.IsVisible = false;
            datePicker.IsVisible = false;
            label.IsVisible = true;
            if (IsDate)
            {
                UnfocusAction?.Invoke(datePicker.Date);
            }
            else
            {
                UnfocusAction?.Invoke(entry.Text);
            }
        }
    }
}