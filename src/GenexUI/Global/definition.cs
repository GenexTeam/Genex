using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GenexUI.Global
{
    public enum GXNodeType
    { 
        GX_NODE_TYPE_NONE       = 0x00,     //无效节点
        GX_NODE_TYPE_PROJECT    = 0x01,     //工程节点
        GX_NODE_TYPE_FILTER     = 0x02,     //过滤器节点
        GX_NODE_TYPE_SCENE      = 0x03      //场景节点
    };

    public class GxTreeNode
    {
        public int nodeType;
        public string nodeName;

        public List<GxTreeNode> childrenNodes;
    };

    public class ProjectTreeNode : GxTreeNode
    {
        public string path;
        public string projectName;
    };

    public class SceneTreeNode : GxTreeNode
    {
        
    };
}
