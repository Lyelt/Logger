using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyeltLogger
{
    public class LogDatabaseWriter : LogWriter
    {
        private LogDatabaseOptions _options;

        public LogDatabaseWriter(string name) : this(name, LogOptions.Default, LogDatabaseOptions.Default) { }

        public LogDatabaseWriter(string name, LogOptions common) : this(name, common, LogDatabaseOptions.Default) { }

        public LogDatabaseWriter(string name, LogOptions common, LogDatabaseOptions options) : base(name, common)
        {
            _options = options;
        }

        public override void LogMessage(LogMessage message)
        {

        }

        public override void SetWriterOptions(WriterOptions options)
        {
            
        }
    }
}
