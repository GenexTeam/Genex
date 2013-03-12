using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GenexUI.Forms.Floating
{
    public partial class frmNewSceneFile : Form
    {
        public frmNewSceneFile(int id, string dirPath)
        {
            InitializeComponent();

            txtSceneName.Text = "新的场景" + id.ToString();
            txtFileName.Text = Path.GetFullPath(dirPath) + "\\Scene" + id.ToString() + ".gxs";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNewSceneFile_Load(object sender, EventArgs e)
        {

        }
    }
}
