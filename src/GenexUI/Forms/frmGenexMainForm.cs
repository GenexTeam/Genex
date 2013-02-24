using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GenexUI.forms.floating;
using GenexUI.Global;
using WeifenLuo.WinFormsUI.Docking;
using GenexUI.Global.LogServerSuit;

namespace GenexUI
{
    public partial class frmGenexMainForm : Form 
    {
        //浮动窗口
        private frmDockSceneManager _frmDockSceneManager;

        public frmGenexMainForm()
        {
            InitializeComponent();

            _frmDockSceneManager = new frmDockSceneManager();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            //初始化全局对象
            GlobalObj.init();
            LoggerProxy.WRITE_DEBUG("Init global obj OK.");

            //加载日志服务器
            GlobalObj.getLogServer().Show();

            //初始化界面布局
            _frmDockSceneManager.Show(this.dockPanel1, DockState.DockLeft);
            LoggerProxy.WRITE_DEBUG("Init ui layout OK");

            //GxProject gxProject = new GxProject();
            //gxProject.load("G:/revisioncontrol/git/genex/src/GenexUI/bin/SampleProject.gxprj");
            _frmDockSceneManager.loadProject("SampleProject.gxprj");
        }

        private void dockPanel1_ActiveContentChanged(object sender, EventArgs e)
        {

        }

        private void frmGenexMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalObj.getLogServer().Close();
            GlobalObj.getLogServer().destroy();
        }
    }
}
