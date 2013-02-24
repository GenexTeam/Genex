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

    //Gx节点数据
    public class GxTreeNode : TreeNode
    {
        private GXNodeType _gxNodeType;
        public GXNodeType GxNodeType
        {
            get
            {
                return _gxNodeType;
            }
            set
            {
                _gxNodeType = value;
            }
        }

        public GxProject toGxProjectInstance()
        {
            return (GxProject)base.Tag;
        }

        public GxScene toGxSceneInstance()
        {
            return (GxScene)base.Tag;
        }
    };
}
