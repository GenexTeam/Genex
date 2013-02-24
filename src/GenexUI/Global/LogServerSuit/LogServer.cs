using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GenexUI.Global.LogServerSuit
{
    public partial class LogServer : Form
    {
        private delegate void DlAppendLog(LogInfo logInfo);

        private List<LogInfo> _logs = new List<LogInfo>();
        public LogServer()
        {
            InitializeComponent();
        }

        public bool init()
        {
            return true;
        }

        public void destroy()
        {
            notifyIcon1.Icon = null;
            this.Close();
        }

        public void AddLog(LogInfo logInfo)
        {
            _logs.Add(logInfo);

            if (txtLogs.InvokeRequired == true)
            {
                txtLogs.Invoke(new DlAppendLog(appendLog), new object[] {logInfo});
            }
            else
            {
                appendLog(logInfo);
            }
        }

        private void appendLog(LogInfo logInfo)
        {
            switch (logInfo.logLevel)
            {
                case LOG_LEVEL.DEBUG:
                    if (chkDebug.Checked == false)
                        return;
                    txtLogs.SelectionColor = Color.White;
                    break;
                case LOG_LEVEL.WARNNING:
                    if (chkWarning.Checked == false)
                        return;
                    txtLogs.SelectionColor = Color.Yellow;
                    break;
                case LOG_LEVEL.ERROR:
                    if (chkError.Checked == false)
                        return;
                    txtLogs.SelectionColor = Color.Red;
                    break;
            }

            string strLog = string.Format(
                "{0}  {1}:  [{2}::{3}(), line {4}] {5}\n",
                logInfo.time,
                logInfo.logLevel.ToString().ToLower(),
                Path.GetFileName(logInfo.file),
                logInfo.func,
                logInfo.line,
                logInfo.logContent
                );

            txtLogs.AppendText(strLog);
            txtLogs.SelectionColor = Color.White;
        }

        private void LogServer_Load(object sender, EventArgs e)
        {
            notifyIcon1.Icon = this.Icon;
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
        }

        private void LogServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            notifyIcon1.Visible = true;
            e.Cancel = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            显示ToolStripMenuItem_Click(sender, e);
        }

        private void 关于AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(null, "就这么个玩意儿你还想知道什么？", "关于", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void chkTopMost_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = chkTopMost.Checked;
        }
    }
}
