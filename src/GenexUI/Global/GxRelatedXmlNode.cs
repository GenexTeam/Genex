using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace GenexUI.Global
{
    public class GxNodeDataBase
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
        /// <param name="dirpath"></param>
        public void saveRealPathToXml(string path, bool isSave = true)
        {
            setPath(path);
            _xmlNode.Attributes["Path"].Value = path;

            //更新子节点路径
            if (_xmlNode.HasChildNodes == true)
            {
                updateChildNodePath(path);
            }

            if (isSave == true)
            {
                GxProject project = GlobalObj.getOpenningProject();
                _xmlNode.OwnerDocument.Save(project.getFullPath());
            }
        }

        /// <summary>
        /// 更新子节点路径
        /// </summary>
        /// <param name="path"></param>
        private void updateChildNodePath(string path)
        {
            foreach (XmlNode node in _xmlNode.ChildNodes)
            {
                //取得文件名
                string filename = Path.GetFileName(node.Attributes["Path"].Value);
                string fullPath = path + "\\" + filename;
                node.Attributes["Path"].Value = fullPath;

                if (node.HasChildNodes == true)
                {
                    updateChildNodePath(fullPath);
                }
            }
        }

    }
}
