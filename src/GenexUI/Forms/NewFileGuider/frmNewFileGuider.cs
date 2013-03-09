using GenexUI.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

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
            string path = "TemplateCategory.xml";
            initalTreeView(templateFileList.Nodes, path);
        }

    
        private void initalTreeView(TreeNodeCollection nodes,string path)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                TreeNode parentNode = new TreeNode();
                parentNode.Text = "模板";
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    XmlElement xe = node as XmlElement;
                    TreeNode newNode = new TreeNode();
                    foreach (XmlAttribute attr in xe.Attributes)
                    {
                        newNode.Text = attr.Value;
                    }
                    SearchXmlDoc(node, newNode);
                    parentNode.Nodes.Add(newNode);
                }
                nodes.Add(parentNode);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        /// <summary>
        /// 递归遍历xml 为treeview绑定数据
        /// </summary>
        /// <param name="node"></param>
        /// <param name="newNode"></param>
        private void SearchXmlDoc(XmlNode xmlNode, TreeNode treeNode)
        {
            if (xmlNode.ChildNodes.Count == 0)
                return;
            else
            {
                foreach(XmlNode node in  xmlNode.ChildNodes)
                {
                    try
                    {
                        if (node.Name ==xmlNode.Name)
                        {
                            XmlElement xe = node as XmlElement;
                            TreeNode newNode = new TreeNode();
                            foreach (XmlAttribute attr in xe.Attributes)
                            {
                                newNode.Text = attr.Value;
                            }
                            SearchXmlDoc(node, newNode);
                            treeNode.Nodes.Add(newNode);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex.Message);
                    }
                }
            }
        }

        private void openFilePath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            filePath.Text = folderBrowserDialog1.SelectedPath;
        }
    }
}
