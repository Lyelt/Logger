using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LyeltLogger.Enums;

namespace LyeltLogger
{
    public class LogOptions
    {
        /// <summary>
        /// Name of the current logging application
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// Minimum level of logging
        /// </summary>
        public LogLevel Verbosity { get; set; }

        /// <summary>
        /// Filter messages that are constantly logged
        /// </summary>
        public bool DuplicationFilter { get; set; }

        /// <summary>
        /// Create a new Log Options object with the given app name, verbosity, and duplication filter option.
        /// </summary>
        /// <remarks>
        /// Anything not specified will get a default value.
        /// </remarks>
        /// <param name="appName">App name to be used in the logs. Default is the current running application's name</param>
        /// <param name="verbosity">Minimum level of messages to log. Default Information</param>
        /// <param name="dupFilter">Whether to filter for duplicate files. Default is false</param>
        public LogOptions(string appName = null, LogLevel verbosity = LogLevel.Information, bool dupFilter = false)
        {
            AppName = appName ?? AppDomain.CurrentDomain?.FriendlyName ?? string.Empty;
            Verbosity = verbosity;
            DuplicationFilter = dupFilter;
        }

        /// <summary>
        /// Default logger options 
        /// </summary>
        public static LogOptions Default { get { return new LogOptions(); } }
    }
}
