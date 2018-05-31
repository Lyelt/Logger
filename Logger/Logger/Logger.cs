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
    public partial class Logger
    {
        private LogOptions _commonOptions;

        private Type _type;
        private Dictionary<string, LogWriter> _logWriters;

        private BlockingCollection<LogMessage> _logQueue;
        private CancellationTokenSource _cts;

        public Logger(Type t, LogOptions commonOptions, params LogOption[] options)
        {
            _commonOptions = commonOptions;
            _type = t;
            _logQueue = new BlockingCollection<LogMessage>();
            _cts = new CancellationTokenSource();
            _logWriters = new Dictionary<string, LogWriter>();
            SetLogWriters(options);
        }

        public void SetOptions(string writerName, WriterOptions options)
        {

        }

        public void SetOptions(string writerName, LogOptions options)
        {
            if (_logWriters.ContainsKey(writerName))
            {
                _logWriters[writerName].SetCommonOptions(options);
            }
        }

        public void SetOptions(LogOptions options)
        {
            foreach (var writer in _logWriters.Values)
            {
                writer.SetCommonOptions(options);
            }
        }

        public void AddLogWriter(LogWriter writer)
        {
            _logWriters[writer.Name] = writer;
        }

        private void SetLogWriters(params LogOption[] options)
        {
            if (options.Contains(LogOption.LogToDatabase))
                AddLogWriter(new LogDatabaseWriter("DefaultDatabaseWriter"));

            if (options.Contains(LogOption.LogToEventViewer))
                AddLogWriter(new LogEventViewerWriter("DefaultEventViewerWriter"));

            if (options.Contains(LogOption.LogToFile))
                AddLogWriter(new LogFileWriter("DefaultFileWriter"));
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
