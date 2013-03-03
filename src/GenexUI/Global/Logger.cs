using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace GenexUI.Global
{
    public static class Logger
    {
        private static ILog _log;
        static Logger()
        { 
            _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public static void Debug(object message)
        {
            _log.Debug(message);
        }

        public static void Error(object message)
        {
            _log.Error(message);
        }

        public static void Fatal(object message)
        {
            _log.Fatal(message);
        }

        public static void Info(object message)
        {
            _log.Info(message);
        }

        public static void Warn(object message)
        {
            _log.Warn(message);
        }
    }

    public static class LogConsole
    {
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();

    }
}
