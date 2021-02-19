using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace RedCorners.Forms
{
    public abstract class CodeEntryBoxViewBase : ContentView2
    {
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(CodeEntryBoxViewBase),
            "",
            BindingMode.TwoWay);

        public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(
            nameof(BackgroundColor),
            typeof(Color),
            typeof(CodeEntryBoxViewBase),
            Color.Transparent);
        
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            nameof(TextColor),
            typeof(Color),
            typeof(CodeEntryBoxViewBase),
            Color.Black);


        public new virtual void Focus()
        {

        }

        public new virtual void Unfocus()
        {

        }
    }

    public class CodeEntryBoxView : CodeEntryBoxViewBase
    {
        public Entry Entry;

        public CodeEntryBoxView()
        {
            Content = Entry = new Entry
            {
                BackgroundColor = BackgroundColor,
                TextColor = TextColor,
                Text = Text
            };
            Entry.BindingContext = this;
            Entry.SetBinding(Entry.BackgroundColorProperty, new Binding(nameof(BackgroundColor)));
            Entry.SetBinding(Entry.TextColorProperty, new Binding(nameof(TextColor)));
            Entry.SetBinding(Entry.TextProperty, new Binding(nameof(Text)));
        }

        public override void Focus()
        {
            base.Focus();
            Entry.Focus();
        }

        public override void Unfocus()
        {
            base.Unfocus();
            Entry.Unfocus();
        }
    }
}
