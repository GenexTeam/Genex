using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace GenexUI.Global
{
    //图标类型索引
    public enum ICON_TYPE
    {
        ICON_PROJECT,       //工程节点
        ICON_SCENE_DIR,     //场景目录节点
        ICON_SCENE          //场景节点
    }

    public class GxProject : GxNodeDataBase
    {
        private bool _isLoaded = false;
        private string _projectName;
        private string _projectFullPath;
        private string _sceneDirPath;
        private string _soundDirPath;
        private string _projectVersion;

        GxTreeNode _projectNode;

        public GxProject()
        {
        }

        /// <summary>
        /// 加载工程文件
        /// </summary>
        /// <param name="filename">工程文件路径</param>
        /// <returns></returns>
        public bool load(string filename)
        {
            try
            {
                Logger.Debug("Loading project XML file, filename = " + filename);
                XmlDocument document = new XmlDocument();
                document.Load(filename);

                _projectFullPath = filename;

                //读取Project属性
                XmlNode projectXmlNode = document.SelectSingleNode("/GameProject/Project");
                if (projectXmlNode != null)
                {
                    _projectName = projectXmlNode.Attributes["Name"].Value;
                    _projectVersion = projectXmlNode.Attributes["GxVersion"].Value;
                }

                //读取SceneDir
                XmlNode sceneDirXmlNode = document.SelectSingleNode("/GameProject/ResourceDir/SceneDir");
                if (sceneDirXmlNode == null)
                {
                    Logger.Error("Xml node [/GameProject/ResourceDir/SceneDir] not exists.");
                    return false;
                }
                _sceneDirPath = sceneDirXmlNode.InnerText;

                //读取SoundDir
                XmlNode soundDirXmlNode = document.SelectSingleNode("/GameProject/ResourceDir/SoundDir");
                if (soundDirXmlNode == null)
                {
                    Logger.Error("Xml node [/GameProject/ResourceDir/SoundDir] not exists.");
                    return false;
                }
                _soundDirPath = soundDirXmlNode.InnerText;

                //加载场景树
                XmlNode sceneTreeXmlNode = document.SelectSingleNode("/GameProject/SceneList");
                if (sceneTreeXmlNode == null)
                {
                    Logger.Error("Xml node [/GameProject/SceneList] not exists.");
                    return false;
                }

                if (sceneTreeXmlNode.ChildNodes.Count > 0)
                {
                    //初始化工程节点
                    _projectNode = new GxTreeNode(
                        _projectName + "[已加载]", 
                        this,
                        GXNodeType.GX_NODE_TYPE_PROJECT,
                        ICON_TYPE.ICON_PROJECT,
                        ICON_TYPE.ICON_PROJECT);

                    this.setRelatedXmlNode(sceneTreeXmlNode);

                    //遍历场景节点
                    loadNodes(sceneTreeXmlNode, null);
                }

            }
            catch (XmlException exception)
            {
                Logger.Error(exception.Message);
                return false;
            }

            //更新环境变量
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVariableType.GXENV_PROJECT_NAME, _projectName);
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVariableType.GXENV_PROJECT_FULL_PATH, _projectFullPath);
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVariableType.GXENV_PROJECT_FILE_NAME, getProjectFileName());
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVariableType.GXENV_PROJECT_FILE_NAME_WITHOUT_EXT, getProjectFileNameWithoutExt());
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVariableType.GXENV_PROJECT_DIR, getProjectDir());
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVariableType.GXENV_PROJECT_VERSION, getProjectVersion());

            _sceneDirPath = GlobalObj.getEnvManager().resolveEnv(_sceneDirPath);
            //_sceneDirPath = _sceneDirPath.Replace("//", "/");
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVariableType.GXENV_PROJECT_SCENE_DIR, getProjectSceneDir());
            Logger.Debug("Updated project env variables");


            //加载场景
            //loadSceneList();

            _isLoaded = true;
            return true;
        }

        /// <summary>
        /// 加载子节点（递归方法）
        /// </summary>
        /// <param name="childNode"></param>
        /// <param name="treeNode"></param>
        private void loadNodes(XmlNode childNode, GxTreeNode parentNode)
        {
            //如果父节点为空则默认为工程根节点
            if (parentNode == null)
            {
                parentNode = _projectNode;
            }

            //老衲要开始遍历了！
            foreach (XmlNode node in childNode.ChildNodes)
            {
                if (node.Name == "IncludeNode")
                {
                    string name = node.Attributes["Name"].Value;
                    string fullPath = node.Attributes["Path"].Value;
                    string type = node.Attributes["Type"].Value;

                    //如果节点是目录
                    if (type == "DirectoryType")
                    {
                        GxSceneDirectory gxDirectory = new GxSceneDirectory(fullPath, node);
                        GxTreeNode gxTreeNode = new GxTreeNode(
                            name,
                            gxDirectory,
                            GXNodeType.GX_NODE_TYPE_DIRECTORY,
                            ICON_TYPE.ICON_SCENE_DIR,
                            ICON_TYPE.ICON_SCENE_DIR
                            );

                        parentNode.Nodes.Add(gxTreeNode);

                        if (node.ChildNodes.Count > 0)
                        {
                            loadNodes(node, gxTreeNode);
                        }
                    }
                    else if (type == "SceneFileType")
                    {
                        GxScene gxScene = new GxScene(fullPath);
                        GxTreeNode gxTreeNode = new GxTreeNode(
                            name,
                            gxScene,
                            GXNodeType.GX_NODE_TYPE_SCENE,
                            ICON_TYPE.ICON_SCENE,
                            ICON_TYPE.ICON_SCENE
                            );

                        parentNode.Nodes.Add(gxTreeNode);
                    }
                    else
                    {
                        Logger.Error("Invalid node type.");
                    }
                }
            }
        }

        /// <summary>
        /// 卸载工程
        /// </summary>
        public void unload()
        {
            _isLoaded = false;
            _projectName = "";
            _projectFullPath = "";
            _sceneDirPath = "";
            _projectVersion = "";
            _projectNode = null;

            //更新环境变量
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVariableType.GXENV_PROJECT_NAME, "");
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVariableType.GXENV_PROJECT_FULL_PATH, "");
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVariableType.GXENV_PROJECT_FILE_NAME, "");
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVariableType.GXENV_PROJECT_FILE_NAME_WITHOUT_EXT, "");
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVariableType.GXENV_PROJECT_DIR, "");
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVariableType.GXENV_PROJECT_VERSION, "");
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVariableType.GXENV_PROJECT_SCENE_DIR, "");

            _isLoaded = false;
        }

        /// <summary>
        /// 移动一个节点
        /// </summary>
        /// <param name="srcNode">要移动的节点</param>
        /// <param name="detNode">父节点</param>
        /// <returns>返回移动成功的节点</returns>
        public GxTreeNode moveNode(GxTreeNode srcNode, GxTreeNode dstNode)
        {
            if (srcNode == null || dstNode == null)
            {
                Logger.Error("srcNode == null || dstNode == null.");
                return null;
            }

            if (srcNode == dstNode)
            {
                Logger.Error("srcNode is equals to dstNode.");
                return null;
            }

            if (findNode(srcNode) == false || findNode(dstNode) == false)
            {
                Logger.Error("srcNode or dstNode not in NodeCollection");
                return null;
            }

            //判断目标节点是否文件夹节点或工程节点
            GXNodeType dstNodeType = dstNode.getGxNodeType();
            if (dstNodeType != GXNodeType.GX_NODE_TYPE_DIRECTORY && dstNodeType != GXNodeType.GX_NODE_TYPE_PROJECT)
            {
                Logger.Error("destination node is not directory type or not project type.");
                return null;
            }

            //取得场景目录完整路径
            GxProject project = GlobalObj.getOpenningProject();
            string sceneDirFullPath = Path.GetFullPath(project.getProjectSceneDir());

            //如果原节点是目录
            if (srcNode.getGxNodeType() == GXNodeType.GX_NODE_TYPE_DIRECTORY)
            { 
                //取得源目录完整路径
                GxSceneDirectory gxSrcSceneDir = (GxSceneDirectory)srcNode.Tag;
                string srcDirFullPath = sceneDirFullPath + "\\" + gxSrcSceneDir.getPath();
                if (Directory.Exists(srcDirFullPath) == false)
                {
                    Logger.Error("srcNode dir path not exists.");
                    return null;
                }

                DirectoryInfo srcDirInfo = new DirectoryInfo(srcDirFullPath);
                string srcDirName = srcDirInfo.Name;

                //取得目标目录完整路径，默认为场景根目录
                string dstDirFullPath = sceneDirFullPath;
                if (dstNodeType == GXNodeType.GX_NODE_TYPE_DIRECTORY)
                {
                    GxSceneDirectory gxDstSceneDir = (GxSceneDirectory)dstNode.Tag;
                    dstDirFullPath = sceneDirFullPath + "\\" + gxDstSceneDir.getPath();
                }

                if (Directory.Exists(dstDirFullPath) == false)
                {
                    Logger.Error("dstNode dir path not exists.");
                    return null;
                }

                //取得移动后的目录路径
                string newDirFullPath = dstDirFullPath + "\\" + srcDirInfo.Name;

                //检查目标路径是否存在了
                if (Directory.Exists(newDirFullPath) == true)
                {
                    Logger.Error("Destination dir is exists!");
                    return null;
                }

                //检查目标路径是否与自己相等
                if (srcDirFullPath == newDirFullPath)
                {
                    Logger.Error("Two path equals.");
                    return null;
                }

                //移动目录
                Directory.Move(srcDirFullPath, newDirFullPath);
                Logger.Debug(string.Format("Direcotry [{0}] moved to [{1}] finished!", srcDirFullPath, newDirFullPath));

                //取得相关XML节点
                XmlNode srcRelatedXmlNode = gxSrcSceneDir.getRelatedXmlNode();
                XmlNode dstRelatedXmlNode = null;

                if (dstNodeType == GXNodeType.GX_NODE_TYPE_DIRECTORY)
                {
                    GxSceneDirectory gxDstSceneDir = (GxSceneDirectory)dstNode.Tag;
                    dstRelatedXmlNode = gxDstSceneDir.getRelatedXmlNode();
                }
                else if (dstNodeType == GXNodeType.GX_NODE_TYPE_PROJECT)
                {
                    dstRelatedXmlNode = this.getRelatedXmlNode();
                }

                //复制源节点的副本
                XmlNode appendNode = srcRelatedXmlNode.Clone();

                //把源节点删除
                srcRelatedXmlNode.ParentNode.RemoveChild(srcRelatedXmlNode);

                //把复制的副本插入到目标节点中
                dstRelatedXmlNode.AppendChild(appendNode);

                //更新源节点的关联XMLNode
                gxSrcSceneDir.setRelatedXmlNode(appendNode);

                //更新路径
                string newPath = "";
                if (dstNodeType == GXNodeType.GX_NODE_TYPE_DIRECTORY)
                {
                    GxSceneDirectory gxDstSceneDir = (GxSceneDirectory)dstNode.Tag;
                    newPath = gxDstSceneDir.getPath();
                }

                gxSrcSceneDir.saveRealPathToXml((newPath == "" ? newPath : newPath += "\\") + srcDirInfo.Name, true);
            }

            //移除原节点
            srcNode.Remove();

            //加到目标节点
            dstNode.Nodes.Add(srcNode);

            return srcNode;
        }

        /// <summary>
        /// 递归查找一个节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool findNode(GxTreeNode node, GxTreeNode parentNode = null)
        {
            //如果父节点为空，则从工程节点开始查找
            if (parentNode == null)
            {
                //判断要查找的是不是工程节点，是的话就从了师太吧
                if (node == _projectNode)
                {
                    return true;
                }

                parentNode = _projectNode;
            }

            foreach (GxTreeNode n in parentNode.Nodes)
            {
                if (n == node)
                {
                    return true;
                }

                if (n.Nodes.Count > 0)
                {
                    if (findNode(node, n) == true)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 获取工程节点
        /// </summary>
        /// <returns></returns>
        public GxTreeNode getProjectNode()
        {
            return _projectNode;
        }

        public string getProjectName()
        {
            return _projectName;
        }

        public string getFullPath()
        {
            return _projectFullPath;
        }

        public string getProjectFileName()
        {
            return Path.GetFileName(_projectFullPath);
        }

        public string getProjectFileNameWithoutExt()
        {
            return Path.GetFileNameWithoutExtension(_projectFullPath);
        }

        public string getProjectDir()
        {
            return Path.GetDirectoryName(_projectFullPath);
        }

        public string getProjectSceneDir()
        {
            return _sceneDirPath;
        }

        public string getProjectVersion()
        {
            return _projectVersion;
        }

        /// <summary>
        /// 工程是否成功加载
        /// </summary>
        /// <returns></returns>
        public bool isLoaded()
        {
            return _isLoaded;
        }
    }
}
