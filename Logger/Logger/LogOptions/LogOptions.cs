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

        public static LogOptions Default
        {
            get
            {
                return new LogOptions
                {
                    AppName = AppDomain.CurrentDomain.FriendlyName,
                    Verbosity = LogLevel.Information,
                    DuplicationFilter = false
                };
            }
        }
    }
}
