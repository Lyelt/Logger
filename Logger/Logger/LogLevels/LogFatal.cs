using System;
using static Logger.Enums;

namespace Logger
{
    public static partial class Log
    {
        public static void Fatal(Exception ex)
        {
            LogManager.LogMessage(LogLevel.Fatal, ex.ToString());
        }

        public static void Fatal(Exception ex, string message)
        {
            LogManager.LogMessage(LogLevel.Fatal, string.Concat(message, Environment.NewLine, ex.ToString()));
        }

        public static void Fatal(Exception ex, string message, params object[] args)
        {
            LogManager.LogMessage(LogLevel.Fatal, string.Concat(string.Format(message, args), Environment.NewLine, ex.ToString()));
        }

        public static void DefferedFatal(Func<string> msgFunc)
        {
            LogManager.LogDeferred(LogLevel.Fatal, msgFunc);
        }
    }
}
