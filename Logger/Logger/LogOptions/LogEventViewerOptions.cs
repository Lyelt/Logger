using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Logger.Enums;

namespace Logger
{
    public class LogEventViewerOptions : WriterOptions
    {
        public static LogEventViewerOptions Default
        {
            get
            {
                return new LogEventViewerOptions
                {
                    
                };
            }
        }
    }
}
