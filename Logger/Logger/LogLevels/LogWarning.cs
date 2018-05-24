using System;
using static Logger.Enums;

namespace Logger
{
    public static partial class Log
    {
        public static void Warning(string message)
        {
            LogManager.LogMessage(LogLevel.Warning, message);
        }

        public static void Warning(string message, params object[] args)
        {
            LogManager.LogMessage(LogLevel.Warning, string.Format(message, args));
        }

        public static void DefferedWarning(Func<string> msgFunc)
        {
            LogManager.LogDeferred(LogLevel.Warning, msgFunc);
        }
    }
}
