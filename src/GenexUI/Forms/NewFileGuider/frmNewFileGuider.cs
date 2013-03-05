using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GenexUI.Forms
{
    public partial class frmNewFileGuider : Form
    {
        public frmNewFileGuider()
        {
            InitializeComponent();
        }

        private void frmNewFileGuider_Load(object sender, EventArgs e)
        {

        }

        private void openFilePath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            filePath.Text = folderBrowserDialog1.SelectedPath;
        }
    }
}
