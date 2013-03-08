using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GenexUI.Forms.Floating
{
    public partial class frmNewSceneFile : Form
    {
        public frmNewSceneFile()
        {
            InitializeComponent();
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
