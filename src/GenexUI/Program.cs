using GenexUI.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#if DEBUG
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.xml")]
#endif
namespace GenexUI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //DEBUG模式下启动控制台
#if DEBUG
            LogConsole.AllocConsole();
#endif
            //启动主窗口
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmGenexMainForm());

#if DEBUG
            LogConsole.FreeConsole();
#endif
        }
    }
}
