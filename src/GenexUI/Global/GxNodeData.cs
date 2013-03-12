using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace GenexUI.Global
{
    public class GxNodeData
    {
        private XmlNode _xmlNode;
        private string _path;

        /// <summary>
        /// 设置路径
        /// </summary>
        /// <param name="dirpath"></param>
        public void setPath(string dirpath)
        {
            _path = dirpath;
        }

        /// <summary>
        /// 获取路径
        /// </summary>
        /// <returns></returns>
        public string getPath()
        {
            return _path;
        }

        /// <summary>
        /// 设置与该对象相关的XML节点
        /// </summary>
        /// <param name="node"></param>
        public void setRelatedXmlNode(XmlNode node)
        {
            _xmlNode = node;
        }

        /// <summary>
        /// 获取与该对象相关的XML节点
        /// </summary>
        /// <returns></returns>
        public XmlNode getRelatedXmlNode()
        {
            return _xmlNode;
        }

        /// <summary>
        /// 保存路径到xml
        /// </summary>
        /// <param name="path"></param>
        /// <param name="treeNode">要递归更新路径的树节点</param>
        /// <param name="isSave"></param>
        public void saveRealPathToXml(string path, GxTreeNode treeNode, bool isSave = true)
        {
            string fullPath = GxEnvManager.getEnv(GxEnvVarType.GXENV_PROJECT_SCENE_DIR) + "\\" + path;
            setPath(fullPath);
            _xmlNode.Attributes["Path"].Value = path;

            //更新子节点路径
            if (_xmlNode.HasChildNodes == true)
            {
                updateChildNodePath(path, _xmlNode);
            }

            //更新树子节点
            GxNodeData treeNodeData = (GxNodeData)treeNode.Tag;
            if (treeNodeData != null)
            {
                treeNodeData.setPath(fullPath);

                if (treeNode.Nodes.Count > 0)
                {
                    updateChildTreeNodePath(path, treeNode);
                }
            }

            if (isSave == true)
            {
                _xmlNode.OwnerDocument.Save(GxEnvManager.getEnv(GxEnvVarType.GXENV_PROJECT_PATH));
            }
        }

        /// <summary>
        /// 更新XML子节点路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="parentNode"></param>
        private void updateChildNodePath(string path, XmlNode parentNode)
        {
            foreach (XmlNode node in parentNode.ChildNodes)
            {
                //取得文件名
                string filename = Path.GetFileName(node.Attributes["Path"].Value);
                string fullPath = path + "\\" + filename;
                node.Attributes["Path"].Value = fullPath;

                if (node.HasChildNodes == true)
                {
                    updateChildNodePath(fullPath, node);
                }
            }

        }

        /// <summary>
        /// 更新树的子节点路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="parentTreeNode"></param>
        private void updateChildTreeNodePath(string path, GxTreeNode parentTreeNode)
        {
            foreach (GxTreeNode node in parentTreeNode.Nodes)
            {
                //更新树节点路径
                GxNodeData treeNodeData = (GxNodeData)node.Tag;
                if (treeNodeData != null)
                {
                    //取得文件名
                    string filename = Path.GetFileName(treeNodeData.getPath());
                    string fullPath = path + "\\" + filename;
                    treeNodeData.setPath(GxEnvManager.getEnv(GxEnvVarType.GXENV_PROJECT_SCENE_DIR) + "\\" + fullPath);

                    if (node.Nodes.Count > 0)
                    {
                        updateChildTreeNodePath(fullPath, node);
                    }
                }
            }
        }

    }
}
