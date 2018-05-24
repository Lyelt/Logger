using System;
using static Logger.Enums;

namespace Logger
{
    public static partial class Log
    {
        public static void Debug(string message)
        {
            LogManager.LogMessage(LogLevel.Debug, message);
        }

        public static void Debug(string message, params object[] args)
        {
            LogManager.LogMessage(LogLevel.Debug, string.Format(message, args));
        }

        public static void DefferedDebug(Func<string> msgFunc)
        {
            LogManager.LogDeferred(LogLevel.Debug, msgFunc);
        }
    }
}
