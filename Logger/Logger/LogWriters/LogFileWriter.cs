using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Logger
{
    public class LogFileWriter : LogWriter
    {
        private static string LOG_EXT = ".log";
        private string _logFileName;

        /// <summary>
        /// Create a new log file writer with the specified name and the default LogOptions
        /// </summary>
        /// <param name="name">Internal, unique name of the LogWriter</param>
        public LogFileWriter(string name) : this(name, LogOptions.Default) { }

        /// <summary>
        /// Create a new log file writer with the specified name and log directory
        /// </summary>
        /// <remarks>
        /// This constructor is used to initialize the log directory, as the log file to use is set at construction time
        /// </remarks>
        /// <param name="name">Internal, unique name of the LogWriter</param>
        /// <param name="logDirectory">Directory to create log files in</param>
        public LogFileWriter(string name, string logDirectory) : this(name, LogOptions.Default)
        {
            LogDirectory = logDirectory;
        }

        /// <summary>
        /// Create a new log file writer with the specified name, log directory, and log options.
        /// </summary>
        /// <param name="name">Internal, unique name of the LogWriter</param>
        /// <param name="logDirectory">Directory to create log files in</param>
        /// <param name="common">Log options commmon to all writers</param>
        public LogFileWriter(string name, string logDirectory, LogOptions common) : this(name, common)
        {
            LogDirectory = logDirectory;
        }

        /// <summary>
        /// Create a new log file writer with the specified name and log options
        /// </summary>
        /// <param name="name">Internal, unique name of the LogWriter</param>
        /// <param name="common">Log options commmon to all writers</param>
        public LogFileWriter(string name, LogOptions common) : base(name, common)
        {
            try
            {
                var dirInfo = Directory.CreateDirectory(LogDirectory);
                _logFileName = dirInfo.FullName + _commonOptions.AppName + LOG_EXT;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                _logFileName = _commonOptions.AppName + LOG_EXT;
            }
        }

        public override void LogMessage(LogMessage message)
        {
            try
            {
                File.AppendAllText(_logFileName, message.ToString());
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error writing log message to file. Message: {message}");
                Console.Error.WriteLine(ex);
            }
            finally
            {
                if (RotateLogs)
                    RotateLogFiles();
            }
        }

        private void RotateLogFiles()
        {

        }

        #region Properties
        /// <summary>
        /// Directory to log to. Default current directory
        /// </summary>
        public string LogDirectory { get; } = Directory.GetCurrentDirectory();

        /// <summary>
        /// Whether to rotate logs when the max file size is reached. Default true
        /// </summary>
        public bool RotateLogs { get; set; } = true;

        /// <summary>
        /// Maximum log file size in KB. Default 1024
        /// </summary>
        public float MaxLogFileSize { get; set; } = 1024;

        /// <summary>
        /// Maximum number of log files to keep. Default 100
        /// </summary>
        public int MaxNumberLogFiles { get; set; } = 100;

        /// <summary>
        /// Whether to compress log files past the number to keep. Default false
        /// </summary>
        public bool CompressArchivedFiles { get; set; } = false;
        #endregion
    }
}
