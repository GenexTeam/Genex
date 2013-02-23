using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GenexUI.Global;
using WeifenLuo.WinFormsUI.Docking;

namespace GenexUI.forms.floating
{
    public partial class frmDockSceneManager : DockContent
    {
        public frmDockSceneManager()
        {
            InitializeComponent();
        }

        private void frmDockSceneExplorer_Load(object sender, EventArgs e)
        {

        }

        //================================================================
        //  ● 外部调用方法
        //================================================================
        public bool loadProject(string filename)
        {
            if (File.Exists(filename) == false)
            {
                MessageBox.Show(string.Format("工程文件不存在或文件路径错误。\n\n{0}", filename), "文件不存在", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            GxProject project = GlobalObj.getOpenningProject();
            if (project.load(filename) == true)
            {
                GxTreeNode projectNode = new GxTreeNode();
                projectNode.GxNodeType = GXNodeType.GX_NODE_TYPE_PROJECT;
                projectNode.Text = string.Format("{0} [已加载]", project.getProjectName());
                projectNode.Tag = project;

                tvwSceneList.Nodes.Add(projectNode);
            }
            
            return true;    
        }

        public void addNode(GxTreeNode sceneTreeNode)
        { 
            
        }

        public bool addNode(GXNodeType nodeType, string nodeName, GxTreeNode parentNode = null)
        {
            if (nodeType == GXNodeType.GX_NODE_TYPE_NONE)
            {
                return false;
            }

            return true;
        }
    }
}
