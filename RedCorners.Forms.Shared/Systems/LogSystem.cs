using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace RedCorners.Forms.Systems
{
    public class LogSystem
    {
        public static LogSystem Instance { get; private set; } = new LogSystem();
        LogSystem() { }


        public Action<string, string> LogDelegate =
                    (message, method) =>
                    Console.WriteLine($"[{method}] {message}");

        public void Log(string message, [CallerMemberName] string method = null)
        {
            LogDelegate?.Invoke(message, method);
        }
    }
}
