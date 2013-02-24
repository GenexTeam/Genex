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
using GenexUI.Global.LogServerSuit;

namespace GenexUI.forms.floating
{
    public partial class frmDockSceneManager : DockContent
    {
        private GxTreeNode _projectNode;
        private FileSystemWatcher _fileSystemWatcher;

        public frmDockSceneManager()
        {
            InitializeComponent();

            //初始化场景目录文件监控器
            _fileSystemWatcher = new FileSystemWatcher();
            _fileSystemWatcher.IncludeSubdirectories = true;
            _fileSystemWatcher.NotifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.Size;
            _fileSystemWatcher.Filter = "";
            _fileSystemWatcher.EnableRaisingEvents = false;
            initFileSystemWatcherEvents();
            LoggerProxy.WRITE_DEBUG("Init FileSystemWatcher object OK.");
        }

        private void frmDockSceneExplorer_Load(object sender, EventArgs e)
        {

        }

        private void initFileSystemWatcherEvents()
        {
            // *注：因为使用了过滤器，因此无法捕捉到文件夹，需要用其它方式判断是不是文件夹

            try
            {
                //文件改变事件
                _fileSystemWatcher.Changed += (sender, e) =>
                {
                    LoggerProxy.WRITE_WARNING(
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
                    LoggerProxy.WRITE_WARNING(
                        "A file event has watched, type = " + e.ChangeType.ToString() +
                        ", path = " + e.FullPath +
                        ", filename = " + e.Name);

                    if (Directory.Exists(e.FullPath) == true)
                    {
                        //文件夹被删除
                        LoggerProxy.WRITE_DEBUG("A directory deleted, path = " + e.FullPath);
                    }
                    else
                    { 
                        //文件被删除
                        LoggerProxy.WRITE_DEBUG("A file deleted, path = " + e.FullPath);
                    }
                };

                //文件名变更事件
                _fileSystemWatcher.Renamed += (sender, e) =>
                {
                    LoggerProxy.WRITE_WARNING(
                        "A file event has watched, type = " + e.ChangeType.ToString() +
                        ", path = " + e.FullPath +
                        ", filename = " + e.Name);

                    if (Directory.Exists(e.FullPath) == true)
                    {
                        //文件夹名变更
                        LoggerProxy.WRITE_DEBUG("A directory renamed, path = " + e.FullPath + ", oldname = " + e.OldName);
                    }
                    else
                    {
                        //文件名变更
                        LoggerProxy.WRITE_DEBUG("A file renamed, path = " + e.FullPath + ", oldname = " + e.OldName);
                    }
                };

                //文件创建事件
                _fileSystemWatcher.Created += (sender, e) =>
                {
                    LoggerProxy.WRITE_WARNING(
                        "A file event has watched, type = " + e.ChangeType.ToString() +
                        ", path = " + e.FullPath +
                        ", filename = " + e.Name);

                    if (Directory.Exists(e.FullPath) == true)
                    {
                        //文件夹被创建
                        LoggerProxy.WRITE_DEBUG("A directory created, path = " + e.FullPath);
                    }
                    else
                    { 
                        //文件被创建
                        LoggerProxy.WRITE_DEBUG("A file created, path = " + e.FullPath);
                    }
                };
            }
            catch (DirectoryNotFoundException iox)
            {
                LoggerProxy.WRITE_ERROR("\r\nEXCEPTION (onChanged): Directory No Found , " + iox.Message);
            }
            catch (FileNotFoundException iox)
            {
                LoggerProxy.WRITE_ERROR("\r\nEXCEPTION (onChanged): File Not Found, " + iox.Message);
            }
            catch (IOException iox)
            {
                LoggerProxy.WRITE_ERROR("\r\nEXCEPTION (onChanged): IO Error, " + iox.Message);
            }
            catch (Exception ex)
            {
                LoggerProxy.WRITE_ERROR("\r\nEXCEPTION (onChanged): " + ex.Message);
            }
        }

        //================================================================
        //  ● 外部调用方法
        //================================================================
        public void closeCurrentProject()
        {
            //卸载工程
            GxProject project = GlobalObj.getOpenningProject();
            project.unload();
            LoggerProxy.WRITE_DEBUG("Unloaded openning project.");

            //清除节点数据
            _projectNode = null;
            tvwSceneList.Nodes.Clear();
            LoggerProxy.WRITE_DEBUG("Cleared all project nodes.");

            _fileSystemWatcher.EnableRaisingEvents = false;
        }

        public bool loadProject(string filename)
        {
            if (File.Exists(filename) == false)
            {
                LoggerProxy.WRITE_ERROR("Failed to load project : file not found.");
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
                LoggerProxy.WRITE_ERROR("load project [" + filename + "] failed.");
                return false;
            }

            //加载场景树
            GxTreeNode projectNode = new GxTreeNode();
            projectNode.GxNodeType = GXNodeType.GX_NODE_TYPE_PROJECT;
            projectNode.Text = string.Format("{0} [已加载]", project.getProjectName());
            projectNode.Tag = project;
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

        private void traversalSceneList(string sceneDirPath, GxTreeNode parentNode)
        { 
            DirectoryInfo sceneDirInfo = new DirectoryInfo(sceneDirPath);

            try
            {
                foreach(DirectoryInfo dir in sceneDirInfo.GetDirectories())
                {
                    GxSceneDirectory gxSceneDir = new GxSceneDirectory();

                    GxTreeNode node = new GxTreeNode();
                    node.GxNodeType = GXNodeType.GX_NODE_TYPE_DIRECTORY;
                    node.Tag = gxSceneDir;
                    node.Text = dir.Name;
                    parentNode.Nodes.Add(node);

                    traversalSceneList(sceneDirInfo + "/" + dir.ToString() + "/", node);
                }

                foreach(FileInfo file in sceneDirInfo.GetFiles("*.gxs")) //查找文件
                {
                    GxScene gxScene = new GxScene();

                    GxTreeNode node = new GxTreeNode();
                    node.GxNodeType = GXNodeType.GX_NODE_TYPE_SCENE;
                    node.Tag = gxScene;
                    node.Text = file.Name;
                    parentNode.Nodes.Add(node);
                }
            }
            catch(Exception e)
            {
                LoggerProxy.WRITE_ERROR(e.Message);
            }
        }

        public void addNode(GxTreeNode sceneTreeNode)
        { 
            
        }

        public bool addNode(GXNodeType nodeType, string nodeName, GxTreeNode parentNode = null)
        {
            if (nodeType == GXNodeType.GX_NODE_TYPE_NONE)
            {
                return false;
            }

            return true;
        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
