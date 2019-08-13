using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace RedCorners.Forms
{

    public class Button2 : Button
    {
        public new event EventHandler Pressed;
        public new event EventHandler Released;

        public new ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static new readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command),
            typeof(ICommand),
            typeof(Button2),
            default(ICommand),
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is Button2 button)
                {
                    if (oldVal is IElement2 oldEl2)
                        oldEl2.Parent = null;
                    else if (oldVal is Element oldEl)
                        oldEl.Parent = null;
                    if (newVal is IElement2 newEl2)
                        newEl2.Parent = button;
                    else if (newVal is Element newEl)
                        newEl.Parent = button;
                }
            });


        public TextAlignment TextAlignment
        {
            get => (TextAlignment)GetValue(TextAlignmentProperty);
            set => SetValue(TextAlignmentProperty, value);
        }

        public static readonly BindableProperty TextAlignmentProperty =
            BindableProperty.Create(nameof(TextAlignment),
            typeof(TextAlignment),
            typeof(Button2),
            TextAlignment.Center,
            BindingMode.TwoWay);


        public virtual void OnPressed()
        {
            Pressed?.Invoke(this, EventArgs.Empty);
        }

        public virtual void OnReleased()
        {
            Released?.Invoke(this, EventArgs.Empty);
        }

        public new event EventHandler Clicked;
        public virtual void OnClicked()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
            if (Command?.CanExecute(CommandParameter) ?? false)
                Command.Execute(CommandParameter);
        }

    }
}
