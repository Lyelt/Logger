using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Logger.Enums;

namespace Logger
{
    public class LogFileOptions : WriterOptions
    {
        /// <summary>
        /// Whether to rotate logs when the max file size is reached
        /// </summary>
        public bool RotateLogs { get; set; }

        /// <summary>
        /// Directory to log to
        /// </summary>
        public string LogDirectory { get; set; }

        /// <summary>
        /// Maximum log file size in KB
        /// </summary>
        public float MaxLogFileSize { get; set; }

        /// <summary>
        /// Maximum number of log files to keep
        /// </summary>
        public int MaxNumberLogFiles { get; set; }

        /// <summary>
        /// Whether to compress log files past the number to keep
        /// </summary>
        public bool CompressArchivedFiles { get; set; }

        public static LogFileOptions Default
        {
            get
            {
                return new LogFileOptions
                {
                    RotateLogs = true,
                    LogDirectory = Directory.GetCurrentDirectory(),
                    MaxLogFileSize = 1024,
                    MaxNumberLogFiles = 100,
                    CompressArchivedFiles = false
                };
            }
        }
    }
}
