using RedCorners.Forms.Systems;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace RedCorners.Forms
{
    public static class LogExtensions
    {
        public static void Log<T>(
            this T sender,
            string message = "",
            [CallerMemberName] string method = "",
            [CallerLineNumber] int lineNo = 0,
            [CallerFilePath] string path = "")
        {
            LogSystem.Instance.Log(message ?? "null", $"[{DateTime.Now}] [...@{lineNo}] {typeof(T).Name}\\{method}");
        }

        public static void Log<T>(
            this string message,
            [CallerMemberName] string method = "",
            [CallerLineNumber] int lineNo = 0,
            [CallerFilePath] string path = "")
        {
            LogSystem.Instance.Log(message ?? "null", $"[{DateTime.Now}] [...@{lineNo}] {typeof(T).Name}\\{method}");
        }
    }
}
