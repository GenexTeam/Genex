using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using GenexUI.Global;
using ICSharpCode.SharpZipLib.Zip;

namespace GenexUI.Forms
{
    public partial class frmNewFileGuider : Form
    {
        public static Dictionary<string, List<string>> dicTemplate;
        public static Dictionary<string, string> dicDescription;

        public templateTypeList templateType;

        public enum templateTypeList:int
        {
            general,
            scene,
            script,
            document,
            image
        }

        public frmNewFileGuider()
        {
            InitializeComponent();
        }



        private void frmNewFileGuider_Load(object sender, EventArgs e)
        {
            dicTemplate = new Dictionary<string, List<string>>();
            dicDescription=new Dictionary<string,string>();
            templateFileList.Nodes.Clear();
            string path = "TemplateCategory.xml";
            initalTreeView(templateFileList.Nodes, path);
            templateShow(templateType);
        }

        private void templateShow(templateTypeList templateType)
        {
            try
            {
                templateFileList.Nodes[0].Nodes[0].Expand();
                templateFileList.Nodes[0].Nodes[0].Nodes[(int)templateType].Expand();
                templateFileList.SelectedNode=templateFileList.Nodes[0].Nodes[0].Nodes[(int)templateType];

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
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
                    parentNode.Expand();
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
                        if (node.Name == xmlNode.Name)
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
                        else
                        {
                            XmlElement xe = node as XmlElement;
                            TreeNode newNode = new TreeNode();
                            foreach (XmlAttribute attr in xe.Attributes)
                            {
                                newNode.Text = attr.Value;
                                if (dicTemplate.ContainsKey(treeNode.Text))
                                {
                                    dicTemplate[treeNode.Text].Add(attr.Value);
                                }
                                else
                                {
                                    List<string> list = new List<string>();
                                    list.Add(attr.Value);
                                    dicTemplate.Add(treeNode.Text, list);
                                }
                            }
                            SearchXmlDoc(node, newNode);
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

        private void templateFileList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            templateList.Clear();
            if (dicTemplate.ContainsKey(templateFileList.SelectedNode.Text))
            {
                foreach (string templateName in dicTemplate[templateFileList.SelectedNode.Text])
                {
                    //解压模板文件templateName.zip
                    UnZipFile(Directory.GetCurrentDirectory()+@"\"+templateName + ".zip");
                }
            }
            templateList.Items[0].Selected = true;
            templateList.Select();
            if (dicDescription.ContainsKey(templateList.SelectedItems[0].Text))
                templateDescription.Text = dicDescription[templateList.SelectedItems[0].Text];
        }
        /// <summary>  
        /// 功能：解压zip格式的文件。  
        /// </summary>  
        /// <param name="zipFilePath">压缩文件路径</param> 
        public void  UnZipFile(string zipFilePath)
        {
            if (zipFilePath == string.Empty)
                return;
            if (!File.Exists(zipFilePath))
                return;
            try
            {
                using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
                {
                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        string directoryName = Path.GetDirectoryName(theEntry.Name);
                        string fileName = Path.GetFileName(theEntry.Name);
                        if (!directoryName.EndsWith("//"))
                            directoryName += "//";
                        if (fileName != String.Empty)
                        {
                            int size = 2048;
                            byte[] data = new byte[2048];
                            String str = "";
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    str += System.Text.Encoding.Default.GetString(data);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            //解析config配置文件
                            if (!string.IsNullOrEmpty(str))
                            {
                                XmlDocument dom = new XmlDocument();
                                System.IO.StringReader xmlSR = new System.IO.StringReader(str.Trim());
                                dom.Load(xmlSR);
                                XmlNode node = dom.SelectSingleNode("Template");
                                XmlAttributeCollection xe = node.Attributes;
                                templateList.Items.Add(xe.GetNamedItem("Name").Value);
                                dicDescription.Add(xe.GetNamedItem("Name").Value, xe.GetNamedItem("Description").Value);
                            }
                        }
                    } 
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private void templateList_Click(object sender, EventArgs e)
        {
            if (dicDescription.ContainsKey(templateList.SelectedItems[0].Text))
                templateDescription.Text = dicDescription[templateList.SelectedItems[0].Text];
        }

    }
}
