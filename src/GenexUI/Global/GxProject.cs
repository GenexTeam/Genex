using GenexUI.Utils;
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

    public class GxProject : GxNodeData
    {
        private bool _isLoaded = false;
        private string _projectName;
        private string _projectFullPath;
        private string _sceneDirPath;
        private string _soundDirPath;
        private string _projectVersion;
        private int _sceneAutoIndent;
        XmlDocument _document;

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
                filename = Path.GetFullPath(filename);
                Logger.Debug("Loading project XML file, filename = " + filename);
                _document = new XmlDocument();
                _document.Load(filename);

                _projectFullPath = filename;

                //============================================================================
                //读取Project属性
                //============================================================================
                XmlNode projectXmlNode = _document.SelectSingleNode("/GameProject/Project");
                if (projectXmlNode == null)
                {
                    Logger.Error("Xml node [/GameProject/Project] not exists.");
                    return false;
                }

                _projectName = projectXmlNode.Attributes["Name"].Value;
                _projectVersion = projectXmlNode.Attributes["GxVersion"].Value;

                //============================================================================
                //读取SceneDir
                //============================================================================
                XmlNode sceneDirXmlNode = _document.SelectSingleNode("/GameProject/ResourceDir/SceneDir");
                if (sceneDirXmlNode == null)
                {
                    Logger.Error("Xml node [/GameProject/ResourceDir/SceneDir] not exists.");
                    return false;
                }
                _sceneDirPath = sceneDirXmlNode.InnerText;


                //============================================================================
                //读取SoundDir
                //============================================================================
                XmlNode soundDirXmlNode = _document.SelectSingleNode("/GameProject/ResourceDir/SoundDir");
                if (soundDirXmlNode == null)
                {
                    Logger.Error("Xml node [/GameProject/ResourceDir/SoundDir] not exists.");
                    return false;
                }
                _soundDirPath = soundDirXmlNode.InnerText;

                //============================================================================
                //读取场景自动编号
                //============================================================================
                XmlNode sceneAutoIndentXmlNode = _document.SelectSingleNode("/GameProject/SceneAutoIndent");
                if (sceneAutoIndentXmlNode == null)
                {
                    Logger.Error("Xml node [/GameProject/SceneAutoIndent] not exists.");
                    return false;
                }
                _sceneAutoIndent = Convert.ToInt32(sceneAutoIndentXmlNode.InnerText);

                //============================================================================
                //加载场景树
                //============================================================================
                XmlNode sceneTreeXmlNode = _document.SelectSingleNode("/GameProject/SceneList");
                if (sceneTreeXmlNode == null)
                {
                    Logger.Error("Xml node [/GameProject/SceneList] not exists.");
                    return false;
                }


                //============================================================================
                //更新环境变量
                //============================================================================

                //环境变量更新：工程名称
                GxEnvManager.updateEnvVariable(GxEnvVarType.GXENV_PROJECT_NAME, _projectName);

                //环境变量更新：工程文件路径
                GxEnvManager.updateEnvVariable(GxEnvVarType.GXENV_PROJECT_PATH, filename);

                //环境变量更新：工程文件名（带扩展名）
                GxEnvManager.updateEnvVariable(GxEnvVarType.GXENV_PROJECT_FILENAME_EXT, Path.GetFileName(filename));

                //环境变量更新：工程文件名（不带扩展名）
                GxEnvManager.updateEnvVariable(GxEnvVarType.GXENV_PROJECT_FILENAME, Path.GetFileNameWithoutExtension(filename));

                //环境变量更新：工程文件扩展名
                string ext = Path.GetExtension(filename);
                ext = ext.Substring(1, ext.Length - 1);
                GxEnvManager.updateEnvVariable(GxEnvVarType.GXENV_PROJECT_FILE_EXT, ext);

                //环境变量更新：工程目录路径
                GxEnvManager.updateEnvVariable(GxEnvVarType.GXENV_PROJECT_DIR, Path.GetDirectoryName(filename));

                //环境变量更新：游戏场景目录路径
                string sceneDirPath = GxEnvManager.resolveEnv(_sceneDirPath);
                GxEnvManager.updateEnvVariable(GxEnvVarType.GXENV_PROJECT_SCENE_DIR, sceneDirPath);

                //环境变量更新：游戏场景目录名称
                GxEnvManager.updateEnvVariable(GxEnvVarType.GXENV_PROJECT_SCENE_DIR_NAME, Path.GetFileName(sceneDirPath));

                //环境变量更新：工程版本号
                GxEnvManager.updateEnvVariable(GxEnvVarType.GXENV_PROJECT_VERSION, _projectVersion);


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
                    this.setPath(GxEnvManager.getEnv(GxEnvVarType.GXENV_PROJECT_SCENE_DIR));

                    //遍历场景节点
                    loadNodes(sceneTreeXmlNode, null);
                }

            }
            catch (XmlException exception)
            {
                Logger.Error(exception.Message);
                return false;
            }

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
                    string path = GxEnvManager.getEnv(GxEnvVarType.GXENV_PROJECT_SCENE_DIR) + "\\" + node.Attributes["Path"].Value;
                    string type = node.Attributes["Type"].Value;

                    //如果节点是目录
                    if (type == "DirectoryType")
                    {
                        GxSceneDirectory gxDirectory = new GxSceneDirectory(path, node);
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
                        int sceneId = Convert.ToInt32(node.Attributes["Id"].Value);
                        GxScene gxScene = new GxScene(sceneId,path);
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
            /*GlobalObj.getEnvManager().updateEnvVariable(GxEnvVarType.GXENV_PROJECT_NAME, "");
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVarType.GXENV_PROJECT_PATH, "");
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVarType.GXENV_PROJECT_FILE_NAME, "");
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVarType.GXENV_PROJECT_FILE_NAME_WITHOUT_EXT, "");
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVarType.GXENV_PROJECT_DIR, "");
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVarType.GXENV_PROJECT_VERSION, "");
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVarType.GXENV_PROJECT_SCENE_DIR, "");*/

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

            //目标节点不能是源节点的子节点
            if (findNode(dstNode, srcNode) == true)
            {
                Logger.Error("dstNode is the childNode of srcNode");
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
            string sceneDirFullPath = GxEnvManager.getEnv(GxEnvVarType.GXENV_PROJECT_SCENE_DIR);

            //如果原节点是目录
            if (srcNode.getGxNodeType() == GXNodeType.GX_NODE_TYPE_DIRECTORY)
            { 
                //取得源目录完整路径
                GxNodeData srcNodeData = (GxNodeData)srcNode.Tag;
                string srcDirFullPath = srcNodeData.getPath();
                if (Directory.Exists(srcDirFullPath) == false)
                {
                    Logger.Error("srcNode dir path not exists.");
                    return null;
                }

                //DirectoryInfo srcDirInfo = new DirectoryInfo(srcDirFullPath);
                string srcDirName = Path.GetFileName(srcDirFullPath);

                //取得目标目录完整路径，默认为场景根目录
                GxNodeData dstNodeData = (GxNodeData)dstNode.Tag;
                string dstDirFullPath = dstNodeData.getPath();

                if (Directory.Exists(dstDirFullPath) == false)
                {
                    Logger.Error("dstNode dir path not exists.");
                    return null;
                }

                //取得移动后的目录路径
                string newDirFullPath = dstDirFullPath + "\\" + srcDirName;

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

                //取得节点相关XML元素
                XmlNode srcRelatedXmlNode = srcNodeData.getRelatedXmlNode();
                XmlNode dstRelatedXmlNode = dstNodeData.getRelatedXmlNode();

                //复制源xml节点的副本
                XmlNode appendNode = srcRelatedXmlNode.CloneNode(true);

                //把复制的副本插入到目标节点中
                dstRelatedXmlNode.AppendChild(appendNode);

                //把源xml节点删除
                srcRelatedXmlNode.ParentNode.RemoveChild(srcRelatedXmlNode);

                //更新源节点的关联XMLNode
                srcNodeData.setRelatedXmlNode(appendNode);

                //取得父节点的基本数据
                string newPath = IOUtil.getRelPath(newDirFullPath, sceneDirFullPath);
                srcNodeData.saveRealPathToXml(newPath, srcNode, true);
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
        /// 创建一个新的场景文件
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="filename"></param>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        /*public bool createNewScene(string sceneName, string filename, GxTreeNode parentNode = null)
        {
            if (sceneName.Length == 0 || filename.Length == 0)
            {
                Logger.Warn("sceneName.Length == 0 || filename.Length == 0");
                return false;
            }

            //如果文件已经存在则返回
            if (File.Exists(filename) == true)
            {
                Logger.Error("Create scene file failed, file is exists.");
                return false;
            }

            XmlDocument newSceneXmlDoc = new XmlDocument();
            newSceneXmlDoc.Save(filename);

            return true;
        }*/

        /// <summary>
        /// 以默认目录名新建一个目录
        /// </summary>
        /// <param name="directoryName"></param>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        public bool createNewDirectory(GxTreeNode parentNode)
        {
            GxTreeNode treeNode = (GxTreeNode)parentNode;
            if (treeNode == null)
            {
                return false;
            }

            GxNodeData nodeDataBase = (GxNodeData)parentNode.Tag;
            if (nodeDataBase != null)
            {
                string parentPath = "";

                //if (treeNode.getGxNodeType() == GXNodeType.GX_NODE_TYPE_DIRECTORY)
                //{
                    parentPath = nodeDataBase.getPath();
                //}
               // else if (treeNode.getGxNodeType() == GXNodeType.GX_NODE_TYPE_PROJECT)
                //{
                 //   parentPath = GxEnvManager.getEnv(GxEnvVarType.GXENV_PROJECT_SCENE_DIR);
                //}

                Logger.Debug("parent full path = " + Path.GetFullPath(parentPath));
                if (Directory.Exists(parentPath) == true)
                {
                    //循环创建文件夹并找到一个不存在的计数，作为默认文件夹
                    int count = 1;
                    const string defaultDirectoryName = "新建场景目录";

                    string newDirectoryName = "";
                    string newDirectoryFullPath = "";

                    while (true)
                    {
                        newDirectoryName = defaultDirectoryName + count.ToString();
                        newDirectoryFullPath = parentPath + "\\" + newDirectoryName;

                        //目录如果存在，则计数+1
                        if (Directory.Exists(newDirectoryFullPath) == true)
                        {
                            Logger.Info("[" + newDirectoryFullPath + "] is exists.");
                            count++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    //取得父节点XML对象，创建文件夹节点到XML
                    XmlNode parentXmlNode = nodeDataBase.getRelatedXmlNode();
                    XmlElement newDirectoryXmlElement = null;
                    if (parentXmlNode != null)
                    {
                        //创建一个IncludeNode节点
                        newDirectoryXmlElement = _document.CreateElement("IncludeNode");
                        newDirectoryXmlElement.SetAttribute("Name", newDirectoryName);
                        newDirectoryXmlElement.SetAttribute("Type", "DirectoryType");
                        newDirectoryXmlElement.SetAttribute("Path", newDirectoryFullPath);

                        //插入到父节点
                        parentXmlNode.AppendChild(newDirectoryXmlElement);
                        parentXmlNode.OwnerDocument.Save(GxEnvManager.getEnv(GxEnvVarType.GXENV_PROJECT_PATH));
                    }
                    else
                    {
                        Logger.Error("parentXmlNode == null");
                        return false;
                    }
                    
                    //创建目录到硬盘
                    Directory.CreateDirectory(Path.GetFullPath(newDirectoryFullPath));

                    //添加TreeView节点到场景列表
                    GxSceneDirectory sceneDir = new GxSceneDirectory(newDirectoryFullPath, newDirectoryXmlElement);
                    GxTreeNode newDirectoryNode = new GxTreeNode(newDirectoryName, sceneDir, GXNodeType.GX_NODE_TYPE_DIRECTORY, ICON_TYPE.ICON_SCENE_DIR, ICON_TYPE.ICON_SCENE_DIR);
                    parentNode.Nodes.Add(newDirectoryNode);
                }
                else
                {
                    Logger.Error("Parent directory is not exists.");
                    return false;
                }
            }
            else
            {
                Logger.Error("nodeDataBase == null");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获取工程节点
        /// </summary>
        /// <returns></returns>
        public GxTreeNode getProjectNode()
        {
            return _projectNode;
        }

        public int getSceneAutoIndent()
        {
            return _sceneAutoIndent;
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
