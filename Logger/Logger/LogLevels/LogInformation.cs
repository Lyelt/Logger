using System;
using static Logger.Enums;

namespace Logger
{
    public static partial class Log
    {
        public static void Information(string message)
        {
            LogManager.LogMessage(LogLevel.Information, message);
        }

        public static void Information(string message, params object[] args)
        {
            LogManager.LogMessage(LogLevel.Information, string.Format(message, args));
        }

        public static void DefferedInformation(Func<string> msgFunc)
        {
            LogManager.LogDeferred(LogLevel.Information, msgFunc);
        }
    }
}
