using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Logger.Enums;

namespace Logger
{
    public static class LogManager
    {
        public static LogLevel MinimumLogLevel { get; set; }

        public static string LoggingApplicationName { get; set; }

        public static string LogDirectory { get; set; }

        public static void Initialize(LogLevel minimumLogLevel = LogLevel.Information, string appName = "DefaultAppName", string logDir = "")
        {
            MinimumLogLevel = minimumLogLevel;
            LoggingApplicationName = appName;
            LogDirectory = string.IsNullOrWhiteSpace(logDir) ? Directory.GetCurrentDirectory() : logDir;
        }

        internal static void LogDeferred(LogLevel level, Func<string> deferred)
        {
            if (level >= MinimumLogLevel)
                LogMessage(level, deferred());
        }

        internal static void LogMessage(LogLevel level, string message)
        {
            if (level < MinimumLogLevel)
                return;
        }
    }
}
