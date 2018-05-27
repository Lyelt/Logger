using System;
using System.Collections.Concurrent;
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
        private static LogOptions _defaults;
        private static Logger _globalLogger;

        // Allow static logging to occur without ever providing options, by using the default options
        static LogManager()
        {
            _defaults = new LogOptions();
            _globalLogger = new Logger(typeof(LogManager), _defaults);
        }

        public static void SetDefaults(LogOptions options)
        {
            _defaults = options;
            _globalLogger.SetOptions(_defaults);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Logger GetLogger<T>()
        {
            return GetLogger<T>(_defaults);
        }

        /// <summary>
        /// Get a logger for the given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Logger GetLogger<T>(LogOptions options)
        {
            return new Logger(typeof(T), options);
        }

        internal static void LogDeferred(LogLevel level, Func<string> deferred)
        {
            _globalLogger.LogDeferred(level, deferred);
        }

        internal static void LogMessage(LogLevel level, string message)
        {
            _globalLogger.LogMessage(level, message);
        }
    }
}
