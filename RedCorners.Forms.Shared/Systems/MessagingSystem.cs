using System;
using Xamarin.Forms;

namespace RedCorners.Forms.Systems
{
    public class MessagingSystem
    {
        MessagingSystem() { }
        public static MessagingSystem Instance { get; private set; } = new MessagingSystem();

        public void Subscribe(object subscriber, Enum message, Action callback)
        {
            MessagingCenter.Subscribe<MessagingSystem>(subscriber, message.ToString(), (s) => callback?.Invoke());
        }

        public void Subscribe<T>(object subscriber, Enum message, Action<T> callback)
        {
            MessagingCenter.Subscribe<MessagingSystem, T>(subscriber, message.ToString(), (s, t) => callback?.Invoke(t));
        }

        public void Unsubscribe(object subscriber, Enum message)
        {
            MessagingCenter.Unsubscribe<MessagingSystem>(subscriber, message.ToString());
        }

        public void Send(Enum message)
        {
            MessagingCenter.Send(this, message.ToString());
        }

        public void Send<T>(Enum message, T t)
        {
            MessagingCenter.Send(this, message.ToString(), t);
        }
    }
}
