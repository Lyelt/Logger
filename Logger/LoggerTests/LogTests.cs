using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LyeltLogger;
using static LyeltLogger.Enums;

namespace LoggerTests
{
    [TestFixture]
    public class LogTests
    {
        private Logger _log;

        [OneTimeSetUp]
        public void LogSetup()
        {
            _log = LogManager.GetLogger<LogTests>(LogWriterOption.LogToFile, LogWriterOption.LogToEventViewer);
        }
        

        [Test]
        public void AddCustomLogWriter()
        {
            Logger log = LogManager.GetLogger<LogTests>(LogWriterOption.LogToDatabase);
            log.AddLogWriter(new LogFileWriter("TestCustomFileWriter"));
        }
    }
}
