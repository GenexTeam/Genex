using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace GenexUI.Global
{
    public class GxSceneDirectory : GxNodeData
    {
        public GxSceneDirectory(string dirPath = "", XmlNode relatedXmlNode = null)
        {
            base.setPath(dirPath);
            base.setRelatedXmlNode(relatedXmlNode);
        }
    }
}
