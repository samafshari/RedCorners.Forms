using RedCorners.Forms.Systems;
using System;

namespace RedCorners.Forms
{
    public static class MessagingExtensions
    {
        public static void Subscribe(this Enum message, object subscriber, Action callback) =>
            MessagingSystem.Instance.Subscribe(subscriber, message, callback);

        public static void Subscribe<T>(this Enum message, object subscriber, Action<T> callback) =>
            MessagingSystem.Instance.Subscribe(subscriber, message, callback);

        public static void Unsubscribe(this Enum message, object subscriber) =>
            MessagingSystem.Instance.Unsubscribe(subscriber, message);

        public static void Send(this Enum message) =>
            MessagingSystem.Instance.Send(message);

        public static void Send<T>(this Enum message, T t) =>
            MessagingSystem.Instance.Send<T>(message, t);
    }
}
