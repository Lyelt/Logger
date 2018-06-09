using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using LyeltLogger;
using static LyeltLogger.Enums;

namespace LoggerTests
{
    [TestFixture]
    public class FileLogTests
    {
        private const string WRITER_NAME = "Log File Writer";
        private Logger _log;

        [OneTimeSetUp]
        public void Setup()
        {
            LogOptions opts = new LogOptions("Log Test App", LogLevel.Debug, true);
            _log = LogManager.GetLogger<FileLogTests>(opts);
            var writer = new LogFileWriter(WRITER_NAME, "..\\testLogs\\");
            writer.CompressArchivedFiles = false;
            writer.MaxLogFileSize = 50;
            writer.RotateLogs = true;
            _log.AddLogWriter(writer);
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

        private void AssertLogGenerics()
        {
            Assert.That(_log.GetLogWriter(WRITER_NAME) is LogFileWriter);
            var writer = _log.GetLogWriter(WRITER_NAME) as LogFileWriter;
            DirectoryAssert.Exists(writer.LogDirectory);
        }
    }
}
