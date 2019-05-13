using RedCorners.Forms.Systems;
using System;

namespace RedCorners.Forms
{
    public static class MessagingExtensions
    {
        // Enum

        /// <summary>
        /// Subscribe to a MessagingCenter signal (RedCorners.Forms)
        /// </summary>
        /// <param name="message">Signal</param>
        /// <param name="subscriber">Object to subscribe. e.g. this</param>
        /// <param name="callback">Callback method</param>
        public static void Subscribe(this Enum message, object subscriber, Action callback) =>
            MessagingSystem.Instance.Subscribe(subscriber, message, callback);

        /// <summary>
        /// Subscribe to a MessagingCenter parametered (T) signal (RedCorners.Forms)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">Signal</param>
        /// <param name="subscriber">Object to subscribe. e.g. this</param>
        /// <param name="callback">Callback method taking a T parameter</param>
        public static void Subscribe<T>(this Enum message, object subscriber, Action<T> callback) =>
            MessagingSystem.Instance.Subscribe(subscriber, message, callback);

        /// <summary>
        /// Unsubscribe from a MessagingCenter signal (RedCorners.Forms)
        /// </summary>
        /// <param name="message">Signal</param>
        /// <param name="subscriber">Subscriber to unsubscribe from, e.g. this</param>
        public static void Unsubscribe(this Enum message, object subscriber) =>
            MessagingSystem.Instance.Unsubscribe(subscriber, message);

        /// <summary>
        /// Broadcasts the signal through MessagingCenter (RedCorners.Forms)
        /// </summary>
        /// <param name="message">Signal</param>
        public static void Signal(this Enum message) =>
            MessagingSystem.Instance.Send(message);

        /// <summary>
        /// Broadcasts the signal with a parameter through MessagingCenter (RedCorners.Forms)
        /// </summary>
        /// <typeparam name="T">Parameter Type</typeparam>
        /// <param name="message">Signal</param>
        /// <param name="t">Parameter</param>
        public static void Signal<T>(this Enum message, T t) =>
            MessagingSystem.Instance.Send<T>(message, t);

        // String

        /// <summary>
        /// Subscribe to a MessagingCenter signal (RedCorners.Forms)
        /// </summary>
        /// <param name="message">Signal</param>
        /// <param name="subscriber">Object to subscribe. e.g. this</param>
        /// <param name="callback">Callback method</param>
        public static void Subscribe(this string message, object subscriber, Action callback) =>
            MessagingSystem.Instance.Subscribe(subscriber, message, callback);

        /// <summary>
        /// Subscribe to a MessagingCenter parametered (T) signal (RedCorners.Forms)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">Signal</param>
        /// <param name="subscriber">Object to subscribe. e.g. this</param>
        /// <param name="callback">Callback method taking a T parameter</param>
        public static void Subscribe<T>(this string message, object subscriber, Action<T> callback) =>
            MessagingSystem.Instance.Subscribe(subscriber, message, callback);

        /// <summary>
        /// Unsubscribe from a MessagingCenter signal (RedCorners.Forms)
        /// </summary>
        /// <param name="message">Signal</param>
        /// <param name="subscriber">Subscriber to unsubscribe from, e.g. this</param>
        public static void Unsubscribe(this string message, object subscriber) =>
            MessagingSystem.Instance.Unsubscribe(subscriber, message);

        /// <summary>
        /// Broadcasts the signal through MessagingCenter (RedCorners.Forms)
        /// </summary>
        /// <param name="message">Signal</param>
        public static void Signal(this string message) =>
            MessagingSystem.Instance.Send(message);

        /// <summary>
        /// Broadcasts the signal with a parameter through MessagingCenter (RedCorners.Forms)
        /// </summary>
        public static void Signal<T>(this string message, T t) =>
            MessagingSystem.Instance.Send<T>(message, t);
    }
}
