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

            //初始化界面布局
            _frmDockSceneManager.Show(this.dockPanel1, DockState.DockLeft);

            GxProject gxProject = new GxProject();
            gxProject.load("G:/revisioncontrol/git/genex/src/GenexUI/bin/SampleProject.gxprj");
        }
    }
}
