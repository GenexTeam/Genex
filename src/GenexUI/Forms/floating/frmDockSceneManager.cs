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
        //操作类型（复制， 剪切）
        enum OPERATION_TYPE
        {
            OP_NONE,    //无
            OP_COPY,    //复制
            OP_CUT      //剪切
        }

        //工程节点
        private GxTreeNode _projectNode;

        //剪切板的节点
        GxTreeNode _clipboardTreeNode;

        //剪切板的操作类型
        OPERATION_TYPE _lastOpType;

        public frmDockSceneManager()
        {
            InitializeComponent();

            //初始化剪切板
            //_clipboardTreeNode = new GxTreeNode();
            //Logger.Debug("Init scene file clipboard OK.");

        }

        private void frmDockSceneExplorer_Load(object sender, EventArgs e)
        {
            
        }

        //================================================================
        //  ● 外部调用方法
        //================================================================

        /// <summary>
        /// 关闭当前工程
        /// </summary>
        public void closeCurrentProject()
        {
            //卸载工程
            GxProject project = GlobalObj.getOpenningProject();
            project.unload();
            Logger.Debug("Unloaded openning project.");

            //清除节点数据
            _projectNode = null;
            tvwSceneList.Nodes.Clear();
            Logger.Debug("Cleared all project nodes.");


        }

        /// <summary>
        /// 加载工程
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool loadProject(string filename)
        {
            Logger.Debug("Loading Project, Path = " + filename);

            if (File.Exists(filename) == false)
            {
                Logger.Error("Failed to load project : file not found.");
                MessageBox.Show(string.Format("{0}\n\n工程文件不存在或文件路径错误。", filename), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //关闭当前已打开的工程
            GxProject project = GlobalObj.getOpenningProject();
            if (project.isLoaded() == true)
            {
                closeCurrentProject();
            }

            //取得传入文件的全路径
            filename = Path.GetFullPath(filename);

            //加载工程文件
            if (project.load(filename) == false)
            {
                Logger.Error("load project [" + filename + "] failed.");
                return false;
            }
            Logger.Debug("Project is loaded.");

            //加载场景树
            GxTreeNode projectNode = new GxTreeNode();
            projectNode.setGxNodeType(GXNodeType.GX_NODE_TYPE_PROJECT);
            projectNode.Text = string.Format("{0} [已加载]", project.getProjectName());
            projectNode.Tag = project;
            projectNode.ImageIndex = 0;
            projectNode.SelectedImageIndex = 0;
            _projectNode = projectNode;

            string sceneDirPath = project.getProjectSceneDir();
            //traversalSceneList(sceneDirPath, projectNode);

            GxTreeNode projectTreeNode = project.getProjectNode();
            tvwSceneList.Nodes.Add(projectTreeNode);

            return true;
        }

        /// <summary>
        /// 处理场景列表
        /// </summary>
        /// <param name="sceneDirPath"></param>
        /// <param name="parentNode"></param>
        private void traversalSceneList(string sceneDirPath, GxTreeNode parentNode)
        {
            DirectoryInfo sceneDirInfo = new DirectoryInfo(sceneDirPath);

            try
            {
                foreach (DirectoryInfo dir in sceneDirInfo.GetDirectories())
                {
                    GxSceneDirectory gxSceneDir = new GxSceneDirectory();
                    gxSceneDir.setPath(dir.FullName);

                    GxTreeNode node = new GxTreeNode();
                    node.setGxNodeType(GXNodeType.GX_NODE_TYPE_DIRECTORY);
                    node.Tag = gxSceneDir;
                    node.Text = dir.Name;
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 1;
                    parentNode.Nodes.Add(node);

                    traversalSceneList(sceneDirInfo + "\\" + dir.ToString() + "\\", node);
                }

                foreach (FileInfo file in sceneDirInfo.GetFiles("*.gxs")) //查找文件
                {
                    GxScene gxScene = new GxScene(file.FullName);
                    GxTreeNode node = new GxTreeNode();
                    node.setGxNodeType(GXNodeType.GX_NODE_TYPE_SCENE);
                    node.Tag = gxScene;
                    node.Text = Path.GetFileNameWithoutExtension(file.Name);
                    node.ImageIndex = 2;
                    node.SelectedImageIndex = 2;
                    parentNode.Nodes.Add(node);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }

        private void ctmSceneNode_Cut_Click(object sender, EventArgs e)
        {
            //设置剪切板内容
            GxTreeNode selectedNode = (GxTreeNode)tvwSceneList.SelectedNode;
            if (selectedNode != null)
            {
                GXNodeType nodeType = selectedNode.getGxNodeType();
                if (nodeType == GXNodeType.GX_NODE_TYPE_NONE || nodeType == GXNodeType.GX_NODE_TYPE_PROJECT)
                {
                    Logger.Error("could not cut an invalid node.");
                    return;
                }

                //当前节点是否已经是剪切板的节点
                if (selectedNode == _clipboardTreeNode)
                {
                    Logger.Error("The selected node is equals _cliboardTreeNode.");
                    return;
                }

                //把当前处于剪切状态的透明图标还原为正常
                //restoreNodeOriginalIcon();

                //取得要剪切的节点的图标索引
                int rawImageIndex = Convert.ToInt32(selectedNode.ImageIndex);

                //查找是否含有透明的图标
                string tranImageKey = "TransIndex_" + Convert.ToString(rawImageIndex);
                int tranImageIndex = imgTreeIcons.Images.IndexOfKey(tranImageKey);

                //找不到rawImageIndex索引的透明版本图标则生成一个
                if (tranImageIndex == -1)
                {
                    //生成一个透明的图标
                    Bitmap bitmap = new Bitmap(imgTreeIcons.Images[rawImageIndex]);
                    bitmap = transparentAdjust(bitmap, 180);
                    imgTreeIcons.Images.Add(tranImageKey, bitmap);
                    tranImageIndex = imgTreeIcons.Images.IndexOfKey(tranImageKey);
                }

                //更新图标
                selectedNode.ImageIndex = tranImageIndex;
                selectedNode.SelectedImageIndex = tranImageIndex;
                selectedNode.setTransImageIndex(tranImageIndex);

                //设置剪切板内容
                setClipboard(selectedNode, OPERATION_TYPE.OP_CUT);
            }
        }

        /// <summary>
        /// 把当前处于剪切状态的透明图标还原为正常
        /// </summary>
        private void restoreNodeOriginalIcon()
        {
            //如果当前有一个剪切操作的节点，则先把其透明图标还原
            if (_clipboardTreeNode != null && _lastOpType == OPERATION_TYPE.OP_CUT)
            {
                if (_clipboardTreeNode.getTransImageIndex() != -1)
                {
                    int index = Convert.ToInt32(getRawImageIndex(_clipboardTreeNode.getGxNodeType()));
                    _clipboardTreeNode.ImageIndex = index;
                    _clipboardTreeNode.SelectedImageIndex = index;
                }
            }
        }


        /// <summary>
        /// 处理图像透明度
        /// </summary>
        /// <param name="srcBitmap"></param>
        /// <param name="transparent"></param>
        /// <returns></returns>
        public Bitmap transparentAdjust(Bitmap srcBitmap, int transparent)
        {
            try
            {
                int w = srcBitmap.Width;
                int h = srcBitmap.Height;
                Bitmap dstBitmap = new Bitmap(srcBitmap.Width, srcBitmap.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                System.Drawing.Imaging.BitmapData srcData = srcBitmap.LockBits(new Rectangle(0, 0, w, h), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                System.Drawing.Imaging.BitmapData dstData = dstBitmap.LockBits(new Rectangle(0, 0, w, h), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                
                unsafe
                {
                    byte* pIn = (byte*)srcData.Scan0.ToPointer();
                    byte* pOut = (byte*)dstData.Scan0.ToPointer();
                    byte* p;
                    int stride = srcData.Stride;
                    int r, g, b;
                    for (int y = 0; y < h; y++)
                    {
                        //更改所有点的透明通道
                        for (int x = 0; x < w; x++)
                        {
                            p = pIn;
                            b = pIn[0];
                            g = pIn[1];
                            r = pIn[2];
                            pOut[1] = (byte)g;
                            pOut[2] = (byte)r;
                            pOut[3] = (byte)transparent;
                            pOut[0] = (byte)b;
                            pIn += 4;
                            pOut += 4;
                        }
                        pIn += srcData.Stride - w * 4;
                        pOut += srcData.Stride - w * 4;
                    }
                    srcBitmap.UnlockBits(srcData);
                    dstBitmap.UnlockBits(dstData);
                    return dstBitmap;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return null;
            }

        }

        private void ctmSceneNode_Copy_Click(object sender, EventArgs e)
        {
            GxTreeNode selectedNode = (GxTreeNode)tvwSceneList.SelectedNode;
            if (selectedNode != null)
            {
                GXNodeType nodeType = selectedNode.getGxNodeType();
                if (nodeType == GXNodeType.GX_NODE_TYPE_NONE || nodeType == GXNodeType.GX_NODE_TYPE_PROJECT)
                {
                    Logger.Error("could not copy an invalid node.");
                    return;
                }

                //设置剪切板内容
                setClipboard(selectedNode, OPERATION_TYPE.OP_COPY);
            }
        }

        /// <summary>
        /// 根据节点类型获取原始图标
        /// </summary>
        /// <param name="nodeType"></param>
        /// <returns></returns>
        private ICON_TYPE getRawImageIndex(GXNodeType nodeType)
        {
            switch (nodeType)
            {
                case GXNodeType.GX_NODE_TYPE_PROJECT:
                    return ICON_TYPE.ICON_PROJECT;
                case GXNodeType.GX_NODE_TYPE_DIRECTORY:
                    return ICON_TYPE.ICON_SCENE_DIR;
                case GXNodeType.GX_NODE_TYPE_SCENE:
                    return ICON_TYPE.ICON_SCENE;
            }

            return ICON_TYPE.ICON_SCENE;
        }

        private void tvwSceneList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                TreeNode node = tvwSceneList.GetNodeAt(e.X, e.Y);
                if (node != null)
                {
                    tvwSceneList.SelectedNode = node;
                }

            }
        }

        private void ctmSceneNode_Paste_Click(object sender, EventArgs e)
        {
            if (_clipboardTreeNode == null)
            {
                Logger.Debug("clipboard is null.");
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            tvwSceneList.BeginUpdate();
            GxTreeNode selectedNode = (GxTreeNode)tvwSceneList.SelectedNode;
            if (selectedNode != null)
            {
                if (_lastOpType == OPERATION_TYPE.OP_CUT)
                {
                    moveNode(_clipboardTreeNode, selectedNode);
                }
                else if (_lastOpType == OPERATION_TYPE.OP_COPY)
                { 
                
                }
            }
            tvwSceneList.EndUpdate();
            clearClipboard();
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// 设置剪切板内容
        /// </summary>
        /// <param name="clipNode"></param>
        /// <param name="opType"></param>
        private void setClipboard(GxTreeNode clipNode, OPERATION_TYPE opType)
        {
            clearClipboard();

            _clipboardTreeNode = clipNode;
            _lastOpType = opType;
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        /// <param name="opType"></param>
        private void moveNode(GxTreeNode srcNode, GxTreeNode dstNode)
        {
            GxTreeNode movedNode = GlobalObj.getOpenningProject().moveNode(srcNode, dstNode);
            if (movedNode != null)
            {
                tvwSceneList.SelectedNode = movedNode;
            }
            else
            {
                //MessageBox.Show("移动节点失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 清空剪切板
        /// </summary>
        private void clearClipboard()
        {
            restoreNodeOriginalIcon();
            _clipboardTreeNode = null;
            _lastOpType = OPERATION_TYPE.OP_NONE;
        }

        private void tvwSceneList_AfterSelect(object sender, TreeViewEventArgs e)
        {
#if DEBUG
            tvwSceneList.BeginUpdate();
            Logger.Info(((GxTreeNode)e.Node).getGxNodeType().ToString());
            tvwSceneList.EndUpdate();
#endif
        }

        private void ctmSceneNode_Reload_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("重新载入将关闭当前处于打开状态的所有文件，是否确定？", "询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            GxProject project = GlobalObj.getOpenningProject();
            if (project.isLoaded() == true)
            {
                string filename = project.getFullPath();
                closeCurrentProject();
                loadProject(filename);
            }
        }

        private void frmDockSceneManager_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Right)
            {
                return;
            }

            GxTreeNode selectedNode = (GxTreeNode)tvwSceneList.SelectedNode;
            if (selectedNode != null)
            {
                GXNodeType type = selectedNode.getGxNodeType();
                if (type == GXNodeType.GX_NODE_TYPE_DIRECTORY)
                {
                    ctmSceneNode_Open.Visible = false;
                    ctmSceneNode_Add.Visible = true;

                }
            }
            
        }

        private void tvwSceneList_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void tvwSceneList_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        private void tvwSceneList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(GxTreeNode)))
            {
                e.Effect = DragDropEffects.Scroll;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void tvwSceneList_DragDrop(object sender, DragEventArgs e)
        {

            //根据鼠标坐标确定要移动到的目标节点
            Point position = new Point(e.X, e.Y);
            position = tvwSceneList.PointToClient(position);

            //取得目标节点
            GxTreeNode targetNode = (GxTreeNode)tvwSceneList.GetNodeAt(position);

            //获取被拖动的节点
            GxTreeNode dragedNode = (GxTreeNode)e.Data.GetData(typeof(GxTreeNode).ToString());

            if (dragedNode.getGxNodeType() == GXNodeType.GX_NODE_TYPE_PROJECT)
            {
                return;
            }

            if (targetNode == dragedNode)
            {
                return;
            }

            //移动节点
            moveNode(dragedNode, targetNode);
        }

        private void tvwSceneList_DragOver(object sender, DragEventArgs e)
        {
            Point position = new Point(0, 0);
            position.X = e.X;
            position.Y = e.Y;
            position = tvwSceneList.PointToClient(position);
            GxTreeNode dropNode = (GxTreeNode)tvwSceneList.GetNodeAt(position);
            tvwSceneList.SelectedNode = dropNode;
            GXNodeType type = dropNode.getGxNodeType();

            if (type == GXNodeType.GX_NODE_TYPE_DIRECTORY || type == GXNodeType.GX_NODE_TYPE_PROJECT ||
                type == GXNodeType.GX_NODE_TYPE_SCENE)
            {
                e.Effect = DragDropEffects.Scroll;
            }
        }
    }
}
