using GenexUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenexUI.Global.LogServer
{
    //日志等级
    public enum LOG_LEVEL
    {
        DEBUG = 0,
        WARNNING,
        ERROR
    }

    //日志数据类
    public class LogInfo
    {
        public LOG_LEVEL logLevel;
        public string logContent;
        public string file;
        public string func;
        public int line;
        public DateTime time;
    }

    class LoggerProxy
    {
        /// <summary>
        /// 输出调试文本
        /// </summary>
        /// <param name="str">调试文本内容</param>
        public static void WRITE_DEBUG(string str)
        {
            LogInfo logInfo = new LogInfo();
            logInfo.time = DateTime.Now;
            logInfo.logLevel = LOG_LEVEL.DEBUG;
            logInfo.logContent = str;
            logInfo.file = KernelUtil._GetFileName();
            logInfo.func = KernelUtil._GetFuncName();
            logInfo.line = KernelUtil._GetLineNum();

            addLog(logInfo);
        }

        /// <summary>
        /// 输出调试文本（带不定参）
        /// </summary>
        /// <param name="str">调试文本内容</param>
        /// <param name="values">参数列表</param>
        public static void WRITE_DEBUG(string str, params object[] values)
        {
            LogInfo logInfo = new LogInfo();
            logInfo.time = DateTime.Now;
            logInfo.logLevel = LOG_LEVEL.DEBUG;
            logInfo.logContent = string.Format(str, values);
            logInfo.file = KernelUtil._GetFileName();
            logInfo.func = KernelUtil._GetFuncName();
            logInfo.line = KernelUtil._GetLineNum();

            addLog(logInfo);
        }

        /// <summary>
        /// 输出警告文本
        /// </summary>
        /// <param name="str">警告文本内容</param>
        public static void WRITE_WARNING(string str)
        {
            LogInfo logInfo = new LogInfo();
            logInfo.time = DateTime.Now;
            logInfo.logLevel = LOG_LEVEL.WARNNING;
            logInfo.logContent = str;
            logInfo.file = KernelUtil._GetFileName();
            logInfo.func = KernelUtil._GetFuncName();
            logInfo.line = KernelUtil._GetLineNum();

            addLog(logInfo);
        }

        /// <summary>
        /// 输出警告文本
        /// </summary>
        /// <param name="str">警告文本内容</param>
        /// <param name="values">参数列表</param>
        public static void WRITE_WARNING(string str, params object[] values)
        {
            LogInfo logInfo = new LogInfo();
            logInfo.time = DateTime.Now;
            logInfo.logLevel = LOG_LEVEL.WARNNING;
            logInfo.logContent = string.Format(str, values);
            logInfo.file = KernelUtil._GetFileName();
            logInfo.func = KernelUtil._GetFuncName();
            logInfo.line = KernelUtil._GetLineNum();

            addLog(logInfo);
        }

        /// <summary>
        /// 输出错误文本
        /// </summary>
        /// <param name="str">错误文本内容</param>
        public static void WRITE_ERROR(string str)
        {
            LogInfo logInfo = new LogInfo();
            logInfo.time = DateTime.Now;
            logInfo.logLevel = LOG_LEVEL.ERROR;
            logInfo.logContent = str;
            logInfo.file = KernelUtil._GetFileName();
            logInfo.func = KernelUtil._GetFuncName();
            logInfo.line = KernelUtil._GetLineNum();

            addLog(logInfo);
        }

        /// <summary>
        /// 输出错误文本
        /// </summary>
        /// <param name="str">错误文本内容</param>
        /// <param name="values">参数列表</param>
        public static void WRITE_ERROR(string str, params object[] values)
        {
            LogInfo logInfo = new LogInfo();
            logInfo.time = DateTime.Now;
            logInfo.logLevel = LOG_LEVEL.ERROR;
            logInfo.logContent = string.Format(str, values);
            logInfo.file = KernelUtil._GetFileName();
            logInfo.func = KernelUtil._GetFuncName();
            logInfo.line = KernelUtil._GetLineNum();

            addLog(logInfo);
        }

        private static void addLog(LogInfo logInfo)
        {
            GlobalObj.getLogServer().AddLog(logInfo);
        }
    }
}
