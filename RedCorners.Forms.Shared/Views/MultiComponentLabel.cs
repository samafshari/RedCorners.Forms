using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using Xamarin.Forms;

namespace RedCorners.Forms
{
    public class MultiComponentLabel : Label
    {
        public IList<TextComponent> Components { get; set; }

        public MultiComponentLabel()
        {
            var components = new ObservableCollection<TextComponent>();
            components.CollectionChanged += OnComponentsChanged;
            Components = components;
        }

        private void OnComponentsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            BuildText();
        }

        private void OnComponentPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            BuildText();
        }

        private void BuildText()
        {
            var formattedString = new FormattedString();
            foreach (var component in Components)
            {
                formattedString.Spans.Add(new Span
                {
                    Text = component.Text,
                    FontAttributes = component.FontAttributes,
                    TextColor = component.TextColor

                });
                component.PropertyChanged -= OnComponentPropertyChanged;
                component.PropertyChanged += OnComponentPropertyChanged;
                component.BindingContext = BindingContext;
            }

            FormattedText = formattedString;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (Components == null) return;
            foreach (var component in Components)
            {
                component.BindingContext = BindingContext;
            }
        }
    }

    public class TextComponent : BindableObject
    {
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text),
                                    typeof(string),
                                    typeof(TextComponent),
                                    default(string));

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor),
                                    typeof(Color),
                                    typeof(TextComponent),
                                    Color.Default);

        public static readonly BindableProperty FontAttributesProperty =
            BindableProperty.Create(nameof(FontAttributes),
                                    typeof(FontAttributes),
                                    typeof(TextComponent),
                                    default(FontAttributes));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public FontAttributes FontAttributes
        {
            get => (FontAttributes)GetValue(FontAttributesProperty);
            set => SetValue(FontAttributesProperty, value);
        }
    }
}
