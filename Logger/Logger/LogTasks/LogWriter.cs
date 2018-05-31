using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public abstract class LogWriter
    {
        public string Name { get; set; }

        protected LogOptions _commonOptions;
        protected WriterOptions _writerOptions;

        public LogWriter(string name, LogOptions options)
        {
            Name = name;
            SetCommonOptions(options);
        }

        public void SetCommonOptions(LogOptions options)
        {
            _commonOptions = options;
        }

        public abstract void SetWriterOptions(WriterOptions options);

        public abstract void LogMessage(LogMessage message);
    }
}
