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
        GX_NODE_TYPE_DIRECTORY  = 0x02,     //过滤器节点
        GX_NODE_TYPE_SCENE      = 0x03      //场景节点
    };

    //Gx节点数据
    public class GxTreeNode : TreeNode
    {
        private GXNodeType _gxNodeType = GXNodeType.GX_NODE_TYPE_NONE;
        private int _transImageIndex = -1;

        public GxTreeNode(
            string text = "", 
            object tag = null, 
            GXNodeType type = 
            GXNodeType.GX_NODE_TYPE_NONE, 
            ICON_TYPE imageIndex = ICON_TYPE.ICON_SCENE,
            ICON_TYPE selectedImageIndex = ICON_TYPE.ICON_SCENE)
        {
            base.Text = text;
            base.Tag = tag;
            setGxNodeType(type);
            base.ImageIndex = Convert.ToInt32(imageIndex);
            base.SelectedImageIndex = Convert.ToInt32(selectedImageIndex);
        }

        public void setGxNodeType(GXNodeType nodeType)
        {
            _gxNodeType = nodeType;
        }

        public GXNodeType getGxNodeType()
        {
            return _gxNodeType;
        }

        public void setTransImageIndex(int index)
        {
            _transImageIndex = index;
        }

        public int getTransImageIndex()
        {
            return _transImageIndex;
        }

        public GxProject toGxProjectInstance()
        {
            return (GxProject)base.Tag;
        }

        public GxSceneDirectory toGxSceneDirectoryInstance()
        {
            return (GxSceneDirectory)base.Tag;
        }

        public GxScene toGxSceneInstance()
        {
            return (GxScene)base.Tag;
        }
    };
}
