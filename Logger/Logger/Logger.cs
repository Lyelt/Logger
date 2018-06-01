using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Logger.Enums;

namespace Logger
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Logger
    {
        /// <summary>
        /// 
        /// </summary>
        public static string DefaultLogWriter { get; } = "DefaultLogWriter";

        /// <summary>
        /// 
        /// </summary>
        public static string DefaultDatabaseWriter { get; } = "DefaultDatabaseWriter";

        /// <summary>
        /// 
        /// </summary>
        public static string DefaultEventViewerWriter { get; } = "DefaultEventViewerWriter";

        private LogOptions _commonOptions;
        private Type _type;
        private Dictionary<string, LogWriter> _logWriters = new Dictionary<string, LogWriter>();
        private BlockingCollection<LogMessage> _logQueue = new BlockingCollection<LogMessage>();
        private CancellationTokenSource _cts = new CancellationTokenSource();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="commonOptions"></param>
        /// <param name="options"></param>
        public Logger(Type t, LogOptions commonOptions, params LogOption[] options)
        {
            _commonOptions = commonOptions;
            _type = t;

            SetLogWriters(options);

            Task.Run(() => LogMessages(), _cts.Token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writerName"></param>
        /// <returns></returns>
        public LogWriter GetLogWriter(string writerName)
        {
            if (_logWriters.ContainsKey(writerName))
                return _logWriters[writerName];
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writerName"></param>
        /// <param name="options"></param>
        public void SetLogOptions(string writerName, LogOptions options)
        {
            if (_logWriters.ContainsKey(writerName))
            {
                _logWriters[writerName].SetCommonOptions(options);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public void SetAllOptions(LogOptions options)
        {
            foreach (var writer in _logWriters.Values)
            {
                writer.SetCommonOptions(options);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        public void AddLogWriter(LogWriter writer)
        {
            _logWriters[writer.Name] = writer;
        }

        internal void LogMessage(LogLevel level, string message)
        {
            if (level < _commonOptions.Verbosity)
                return;

            var logMessage = new LogMessage(level, message, _commonOptions.AppName, DateTime.Now, _type);
            _logQueue.TryAdd(logMessage);
        }

        internal void LogDeferred(LogLevel level, Func<string> msgFunc)
        {
            if (level >= _commonOptions.Verbosity)
                LogMessage(level, msgFunc());
        }

        private void SetLogWriters(params LogOption[] options)
        {
            if (options.Contains(LogOption.LogToDatabase))
                AddLogWriter(new LogDatabaseWriter(DefaultDatabaseWriter));

            if (options.Contains(LogOption.LogToEventViewer))
                AddLogWriter(new LogEventViewerWriter(DefaultEventViewerWriter));

            if (options.Contains(LogOption.LogToFile))
                AddLogWriter(new LogFileWriter(DefaultLogWriter));
        }

        private void LogMessages()
        {
            while (!_cts.IsCancellationRequested)
            {
                try
                {
                    if (_logQueue.TryTake(out var message))
                    {
                        foreach (var logger in _logWriters.Values)
                        {
                            logger.LogMessage(message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Error logging messages.");
                    Console.Error.WriteLine(ex);
                }
            }
        }
    }
}
