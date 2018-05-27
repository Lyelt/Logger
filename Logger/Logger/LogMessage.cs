using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Logger.Enums;

namespace Logger
{
    public class LogMessage
    {
        public string Message { get; set; }

        public LogLevel Level { get; set; }

        public string AppName { get; set; }

        public DateTime MessageTime { get; set; }

        public Type Class { get; set; }

        public LogMessage(LogLevel level, string message, string appName, DateTime dt, Type type)
        {
            Level = level;
            Message = message;
            AppName = AppName;
            MessageTime = dt;
            Class = type;
        }

        public override string ToString()
        {
            return $"[{MessageTime}] <{Level}> in {AppName}.{Class.Name}: {Message}";
        }
    }
}
