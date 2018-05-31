using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Logger.Enums;

namespace Logger
{
    public class LogDatabaseOptions : WriterOptions
    {
        public static LogDatabaseOptions Default
        {
            get
            {
                return new LogDatabaseOptions
                {
                    
                };
            }
        }
    }
}
