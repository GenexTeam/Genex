using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace GenexUI.Utils
{
    public class KernelUtil
    {
        private static StackTrace _st = new StackTrace(new StackFrame(true));

        //获取行号
        public static int _GetLineNum()
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
            return st.GetFrame(1).GetFileLineNumber();
        }

        //获取文件名
        public static string _GetFileName()
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
            return st.GetFrame(1).GetFileName();
        }

        //获取函数
        public static string _GetFuncName()
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
            return st.GetFrame(1).GetMethod().Name;
        }
    }
}
