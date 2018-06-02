using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyeltLogger
{
    public class LogEventViewerWriter : LogWriter
    {
        public LogEventViewerWriter(string name) : this(name, LogOptions.Default, LogEventViewerOptions.Default) { }

        public LogEventViewerWriter(string name, LogOptions common) : this(name, common, LogEventViewerOptions.Default) { }

        public LogEventViewerWriter(string name, LogOptions common, LogEventViewerOptions options) : base(name, common)
        {

        }

        public override void LogMessage(LogMessage message)
        {

        }

        public override void SetWriterOptions(WriterOptions options)
        {
            
        }
    }
}
