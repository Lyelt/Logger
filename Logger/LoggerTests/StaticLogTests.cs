using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger;
using static Logger.Enums;
using NUnit.Framework;

namespace LoggerTests
{
    [TestFixture]
    public class StaticLogTests
    {
        private string LOG_DIR = "..\\log\\";
        private string APP_NAME = "TestAppName";

        [SetUp]
        public void LogTestSetup()
        {
            
        }

        [Test]
        public void LogDebugTest()
        {
            Log.Debug("test debug message");
            DirectoryAssert.Exists(LOG_DIR);
            FileAssert.Exists(LOG_DIR + APP_NAME + ".log");
            
        }

        [Test]
        public void LogInfoTest()
        {
            Log.Information("test info message");
        }

        [Test]
        public void LogWarnTest()
        {
            Log.Warning("test warn message");
        }

        [Test]
        public void LogErrorTest()
        {
            Log.Error("test error message");
        }

        [Test]
        public void LogFatalTest()
        {
            Log.Fatal("test fatal message");
        }
    }
}
