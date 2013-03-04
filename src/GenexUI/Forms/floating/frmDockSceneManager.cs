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

        //图标类型索引
        enum ICON_TYPE
        {
            ICON_PROJECT,       //工程节点
            ICON_SCENE_DIR,     //场景目录节点
            ICON_SCENE          //场景节点
        }

        //工程节点
        private GxTreeNode _projectNode;

        //剪切板的节点
        GxTreeNode _clipboardTreeNode;

        //剪切板的操作类型
        OPERATION_TYPE _lastOpType;

        //文件系统监视器
        private FileSystemWatcher _fileSystemWatcher;


        public frmDockSceneManager()
        {
            InitializeComponent();

            //初始化剪切板
            //_clipboardTreeNode = new GxTreeNode();
            //Logger.Debug("Init scene file clipboard OK.");

            //初始化场景目录文件监控器
            _fileSystemWatcher = new FileSystemWatcher();
            _fileSystemWatcher.IncludeSubdirectories = true;
            _fileSystemWatcher.NotifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.Size;
            _fileSystemWatcher.Filter = "";
            _fileSystemWatcher.EnableRaisingEvents = false;
            initFileSystemWatcherEvents();
            Logger.Debug("Init FileSystemWatcher object OK.");
        }

        private void frmDockSceneExplorer_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 初始化文件系统监视器
        /// </summary>
        private void initFileSystemWatcherEvents()
        {
            // *注：因为使用了过滤器，因此无法捕捉到文件夹，需要用其它方式判断是不是文件夹

            try
            {
                //文件改变事件
                _fileSystemWatcher.Changed += (sender, e) =>
                {
                    Logger.Debug(
                        "A file event has watched, type = " + e.ChangeType.ToString() +
                        ", path = " + e.FullPath +
                        ", filename = " + e.Name);


                    //文件内容发生变更
                    if (Directory.Exists(e.FullPath) == false)
                    {

                    }
                };

                //文件删除事件
                _fileSystemWatcher.Deleted += (sender, e) =>
                {
                    Logger.Warn(
                        "A file event has watched, type = " + e.ChangeType.ToString() +
                        ", path = " + e.FullPath +
                        ", filename = " + e.Name);

                    if (Directory.Exists(e.FullPath) == true)
                    {
                        //文件夹被删除
                        Logger.Debug("A directory deleted, path = " + e.FullPath);
                    }
                    else
                    {
                        //文件被删除
                        Logger.Debug("A file deleted, path = " + e.FullPath);
                    }
                };

                //文件名变更事件
                _fileSystemWatcher.Renamed += (sender, e) =>
                {
                    Logger.Warn(
                        "A file event has watched, type = " + e.ChangeType.ToString() +
                        ", path = " + e.FullPath +
                        ", filename = " + e.Name);

                    if (Directory.Exists(e.FullPath) == true)
                    {
                        //文件夹名变更
                        Logger.Debug("A directory renamed, path = " + e.FullPath + ", oldname = " + e.OldName);
                    }
                    else
                    {
                        //文件名变更
                        Logger.Debug("A file renamed, path = " + e.FullPath + ", oldname = " + e.OldName);
                    }
                };

                //文件创建事件
                _fileSystemWatcher.Created += (sender, e) =>
                {
                    Logger.Warn(
                        "A file event has watched, type = " + e.ChangeType.ToString() +
                        ", path = " + e.FullPath +
                        ", filename = " + e.Name);

                    if (Directory.Exists(e.FullPath) == true)
                    {
                        //文件夹被创建
                        Logger.Debug("A directory created, path = " + e.FullPath);
                    }
                    else
                    {
                        //文件被创建
                        Logger.Debug("A file created, path = " + e.FullPath);
                    }
                };
            }
            catch (DirectoryNotFoundException iox)
            {
                Logger.Error("\r\nEXCEPTION (onChanged): Directory No Found , " + iox.Message);
            }
            catch (FileNotFoundException iox)
            {
                Logger.Error("\r\nEXCEPTION (onChanged): File Not Found, " + iox.Message);
            }
            catch (IOException iox)
            {
                Logger.Error("\r\nEXCEPTION (onChanged): IO Error, " + iox.Message);
            }
            catch (Exception ex)
            {
                Logger.Error("\r\nEXCEPTION (onChanged): " + ex.Message);
            }
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

            _fileSystemWatcher.EnableRaisingEvents = false;
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
            traversalSceneList(sceneDirPath, projectNode);

            tvwSceneList.Nodes.Add(projectNode);

            //开启文件监控
            _fileSystemWatcher.Path = sceneDirPath;
            _fileSystemWatcher.EnableRaisingEvents = true;
            _fileSystemWatcher.WaitForChanged(WatcherChangeTypes.All, 1000);

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
                    gxSceneDir.setDirectoryPath(dir.FullName);

                    GxTreeNode node = new GxTreeNode();
                    node.setGxNodeType(GXNodeType.GX_NODE_TYPE_DIRECTORY);
                    node.Tag = gxSceneDir;
                    node.Text = dir.Name;
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 1;
                    parentNode.Nodes.Add(node);

                    traversalSceneList(sceneDirInfo + "/" + dir.ToString() + "/", node);
                }

                foreach (FileInfo file in sceneDirInfo.GetFiles("*.gxs")) //查找文件
                {
                    GxScene gxScene = new GxScene();
                    gxScene.setSceneFileFullPath(file.FullName);

                    GxTreeNode node = new GxTreeNode();
                    node.setGxNodeType(GXNodeType.GX_NODE_TYPE_SCENE);
                    node.Tag = gxScene;
                    node.Text = file.Name;
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

        private void ctmSceneList_Cut_Click(object sender, EventArgs e)
        {
            GxTreeNode selectedNode = (GxTreeNode)tvwSceneList.SelectedNode;
            if (selectedNode != null)
            {
                GXNodeType nodeType = selectedNode.getGxNodeType();
                if (nodeType == GXNodeType.GX_NODE_TYPE_NONE || nodeType == GXNodeType.GX_NODE_TYPE_PROJECT)
                {
                    Logger.Error("could not cut an invalid node.");
                    return;
                }

                //把当前处于剪切状态的透明图标还原为正常
                //restoreNodeOriginalIcon();

                //取得要剪切的节点的图标索引
                int rawImageIndex = Convert.ToInt32(getRawImageIndex(selectedNode.getGxNodeType()));

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

        private void ctmSceneList_Copy_Click(object sender, EventArgs e)
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

        private void ctmSceneList_Paste_Click(object sender, EventArgs e)
        {
            paste();
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
        private void paste()
        {
            if (_clipboardTreeNode == null)
            {
                Logger.Debug("clipboard is null.");
                return;
            }

            //取得剪切板的节点类型
            GXNodeType nodeType = _clipboardTreeNode.getGxNodeType();
            if (nodeType == GXNodeType.GX_NODE_TYPE_NONE || nodeType == GXNodeType.GX_NODE_TYPE_PROJECT)
            {
                Logger.Error("could not paste an invalid node. nodeType = " + nodeType.ToString());
                return;
            }

            //获取要粘贴的地方，默认粘贴地方为场景根目录
            string dstDirPath;
            GxProject curProject = GlobalObj.getOpenningProject();
            if (curProject != null)
            {
                dstDirPath = curProject.getProjectSceneDir();
            }
            else
            {
                Logger.Error("no project is openning");
                return;
            }

            //取得选中节点
            GxTreeNode dstTreeNode = (GxTreeNode)tvwSceneList.SelectedNode;
            if (dstTreeNode == null)
            {
                Logger.Debug("selected node is null.");
                return;
            }

            //要粘贴在directory节点下
            if (dstTreeNode.getGxNodeType() == GXNodeType.GX_NODE_TYPE_DIRECTORY)
            {
                //取得目标目录
                GxSceneDirectory gxSceneDir = (GxSceneDirectory)dstTreeNode.Tag;
                dstDirPath = gxSceneDir.getDirectoryPath();

                //如果源节点是目录
                if (nodeType == GXNodeType.GX_NODE_TYPE_DIRECTORY)
                {
                    GxSceneDirectory srcSceneDir = (GxSceneDirectory)_clipboardTreeNode.Tag;
                    string srcDirPath = srcSceneDir.getDirectoryPath();
                    if (srcSceneDir != null)
                    {
                        try
                        {
                            if (Directory.Exists(srcDirPath) == true && Directory.Exists(dstDirPath) == true)
                            {
                                //源目录信息
                                DirectoryInfo srcDirInfo = new DirectoryInfo(srcDirPath);
        
                                //移动目录
                                string dstDirFullPath = dstDirPath + "\\" + srcDirInfo.Name;

                                //如果是剪切操作
                                if (_lastOpType == OPERATION_TYPE.OP_CUT)
                                {
                                    //源路径和目标路径不一致
                                    Directory.Move(srcDirPath, dstDirFullPath);
                                    Logger.Debug("Direcotry [srcDirPath] moved to [dstDirFullPath] finished!");
                                }
                                else if (_lastOpType == OPERATION_TYPE.OP_COPY)
                                { 
                                
                                }

                                //更新节点状态
                                srcSceneDir.setDirectoryPath(dstDirFullPath);
                                _clipboardTreeNode.Remove();
                                dstTreeNode.Nodes.Add(_clipboardTreeNode);
                                clearClipboard();
                            }
                        }
                        catch (IOException exception)
                        {
                            MessageBox.Show(exception.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Logger.Error(exception.Message);
                            clearClipboard();
                        }
                    }
                }
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
            Logger.Info(((GxTreeNode)e.Node).getGxNodeType().ToString());
        }

        private void ctmSceneList_Reload_Click(object sender, EventArgs e)
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
    }
}
