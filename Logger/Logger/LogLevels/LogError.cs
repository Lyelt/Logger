using System;
using static Logger.Enums;

namespace Logger
{
    public static partial class Log
    {
        public static void Error(string message)
        {
            LogManager.LogMessage(LogLevel.Error, message);
        }

        public static void Error(string message, params object[] args)
        {
            LogManager.LogMessage(LogLevel.Error, string.Format(message, args));
        }

        public static void Error(Exception ex)
        {
            LogManager.LogMessage(LogLevel.Error, ex.ToString());
        }

        public static void Error(Exception ex, string message)
        {
            LogManager.LogMessage(LogLevel.Error, string.Concat(message, Environment.NewLine, ex.ToString()));
        }

        public static void Error(Exception ex, string message, params object[] args)
        {
            LogManager.LogMessage(LogLevel.Error, string.Concat(string.Format(message, args), Environment.NewLine, ex.ToString()));
        }

        public static void DefferedError(Func<string> msgFunc)
        {
            LogManager.LogDeferred(LogLevel.Error, msgFunc);
        }
    }
}
