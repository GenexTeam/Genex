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
using log4net;
using System.Reflection;
using GenexUI.Forms;

namespace GenexUI
{
    public partial class frmGenexMainForm : Form 
    {
        //浮动窗口
        private frmDockSceneManager _frmDockSceneManager;
        private frmNewFileGuider _newFileGuider;

        public frmGenexMainForm()
        {
            InitializeComponent();

            //初始化全局对象
            GlobalObj.init();
            Logger.Debug("Init global obj OK.");

            //初始化新建文件管理器
            _newFileGuider = new frmNewFileGuider();

            //初始化场景管理器
            _frmDockSceneManager = new frmDockSceneManager();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            //初始化界面布局
            initLayout();
            _frmDockSceneManager.loadProject("SampleProject\\SampleProject.gxprj");
        }

        private void initLayout()
        {
            //初始化场景管理器布局
            _frmDockSceneManager.Show(this.dockPanel1, DockState.DockLeft);
            Logger.Debug("Init ui layout OK");
        }

        private void dockPanel1_ActiveContentChanged(object sender, EventArgs e)
        {

        }

        private void frmGenexMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _newFileGuider.Close();
            _newFileGuider.Dispose();

            _frmDockSceneManager.Close();
            _frmDockSceneManager.Dispose();
        }

        private void 新建文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _newFileGuider.ShowDialog();
        }
    }
}
