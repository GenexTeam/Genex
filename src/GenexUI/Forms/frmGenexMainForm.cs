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
            _frmDockSceneManager.Show(this.dockPanel1, DockState.DockLeft);
        }
    }
}
