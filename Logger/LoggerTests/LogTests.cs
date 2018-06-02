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
            _log = LogManager.GetLogger<LogTests>(LogOption.LogToFile, LogOption.LogToEventViewer);
        }

        [Test]
        public void TestDebug()
        {
            _log.Debug("test debug message");
        }

        [Test]
        public void TestInfo()
        {
            _log.Information("test info message");
        }

        [Test]
        public void TestWarn()
        {
            _log.Warning("test warning message");
        }

        [Test]
        public void TestError()
        {
            _log.Error("test exception message");
        }

        [Test]
        public void TestFatal()
        {
            _log.Fatal("test fatal message");
        }

        [Test]
        public void AddCustomLogWriter()
        {
            Logger log = LogManager.GetLogger<LogTests>(LogOption.LogToDatabase);
            log.AddLogWriter(new LogFileWriter("TestCustomFileWriter"));
        }
    }
}
