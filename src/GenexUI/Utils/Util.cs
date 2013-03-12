using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace GenexUI.Utils
{
    public static class IOUtil
    {
        /// <summary>
        /// 获取myPath相对于refPath的相对路径
        /// </summary>
        /// <param name="myPath">我的路径</param>
        /// <param name="refPath">作为参考的路径</param>
        /// <returns></returns>
        public static string getRelPath(string myPath, string refPath)
        {
            myPath = Path.GetFullPath(myPath);
            refPath = Path.GetFullPath(refPath);

            if (myPath.Equals(refPath))
            {
                return ".\\";
            }

            int index = myPath.IndexOf(refPath);
            if (index != -1)
            {
                //+1和-1是为了去掉截取之后前面带有的“\”路径分隔符
                return myPath.Substring(refPath.Length + 1, myPath.Length - refPath.Length - 1);
            }

            return "";
        }
    }
}
