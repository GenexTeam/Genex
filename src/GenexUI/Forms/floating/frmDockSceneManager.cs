using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public void loadProject(GxProject project)
        { 
        
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
