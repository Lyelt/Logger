using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Logger.Enums;

namespace Logger
{
    public class LogOptions
    {
        /// <summary>
        /// Current app name
        /// </summary>
        public static string DefaultAppName { get; } = System.AppDomain.CurrentDomain.FriendlyName;

        /// <summary>
        /// Current directory
        /// </summary>
        public static string DefaultLogDirectory { get; } = Directory.GetCurrentDirectory();

        /// <summary>
        /// Information level
        /// </summary>
        public static LogLevel DefaultVerbosity { get; } = LogLevel.Information;

        /// <summary>
        /// Only log to file
        /// </summary>
        public static ReadOnlyCollection<LogOption> DefaultLogOptions { get; } = new ReadOnlyCollection<LogOption>( new List<LogOption> { LogOption.LogToFile } );

        public string AppName { get; set; }

        public string LogDirectory { get; set; }

        public LogLevel Verbosity { get; set; }

        public ReadOnlyCollection<LogOption> Options { get; set; }

        /// <summary>
        /// Create a new LogOptions, specifying some or all of the parameters. 
        /// Those not specified will get default values.
        /// </summary>
        /// <param name="appName">Default is the running application's name</param>
        /// <param name="logDir">Default is current directory</param>
        /// <param name="verbosity">Default is Information</param>
        /// <param name="options">If none are specified, default is only LogToFile</param>
        public LogOptions(string appName = null, string logDir = null, LogLevel verbosity = LogLevel.Information, params LogOption[] options)
        {
            AppName = appName ?? DefaultAppName;
            LogDirectory = logDir ?? DefaultLogDirectory;
            Verbosity = verbosity;
            Options = options.Length == 0 ? DefaultLogOptions : new ReadOnlyCollection<LogOption>(options);
        }

        /// <summary>
        /// Create a new LogOptions with all default values.
        /// </summary>
        public LogOptions()
        {
            AppName = DefaultAppName;
            LogDirectory = DefaultLogDirectory;
            Verbosity = DefaultVerbosity;
            Options = DefaultLogOptions;
        }
    }
}
