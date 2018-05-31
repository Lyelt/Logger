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
        private LogFileOptions _fileOptions;

        public LogFileWriter(string name) : this(name, LogOptions.Default, LogFileOptions.Default) { }

        public LogFileWriter(string name, LogOptions common) : this(name, common, LogFileOptions.Default) { }

        public LogFileWriter(string name, LogOptions common, LogFileOptions options) : base(name, common)
        {
            _fileOptions = options;
            var dirInfo = Directory.CreateDirectory(_fileOptions.LogDirectory);
            _logFileName = dirInfo.FullName + _commonOptions.AppName + LOG_EXT;
        }

        public override void SetWriterOptions(WriterOptions options)
        {
            if (options is LogFileOptions fileOpts)
            {
                _fileOptions = fileOpts;
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
                if (_fileOptions.RotateLogs)
                    RotateLogFiles();
            }
        }

        private void RotateLogFiles()
        {

        }
    }
}
