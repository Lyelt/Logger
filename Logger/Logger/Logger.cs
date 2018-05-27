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
        private Type _type;
        private LogOptions _options;

        private List<LogTask> _logWriters;

        private BlockingCollection<LogMessage> _logQueue;
        private CancellationTokenSource _cts;

        public Logger(Type t, LogOptions options)
        {
            _type = t;
            _options = options;
            _logQueue = new BlockingCollection<LogMessage>();
            _cts = new CancellationTokenSource();
            RefreshLogOptions();
        }

        public void SetOptions(LogOptions options)
        {
            _options = options;
            RefreshLogOptions();
        }

        public void AddLogTask(LogTask task)
        {
            _logWriters.Add(task);
        }

        private void RefreshLogOptions()
        {
            _logWriters = new List<LogTask>();

            if (_options.Options.Contains(LogOption.LogToFile))
            {
                _logWriters.Add(new LogFileTask());
            }

            if (_options.Options.Contains(LogOption.LogToEventViewer))
            {
                _logWriters.Add(new LogEventViewerTask());
            }

            if (_options.Options.Contains(LogOption.LogToDatabase))
            {
                _logWriters.Add(new LogDatabaseTask());
            }
        }

        internal void LogMessage(LogLevel level, string message)
        {
            if (level < _options.Verbosity)
                return;

            var logMessage = new LogMessage(level, message, _options.AppName, DateTime.Now, _type);
            _logQueue.TryAdd(logMessage);
        }

        internal void LogDeferred(LogLevel level, Func<string> msgFunc)
        {
            if (level >= _options.Verbosity)
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
                        foreach (var logger in _logWriters)
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
