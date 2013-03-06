using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace GenexUI.Global
{
    public class GxProject
    {
        private bool _isLoaded = false;
        private string _projectName;
        private string _projectFullPath;
        private string _sceneDirPath;
        private string _soundDirPath;
        private string _projectVersion;

        List<GxTreeNode> _sceneList;

        public GxProject()
        {
            _sceneList = new List<GxTreeNode>();
        }

        //加载工程文件
        public bool load(string filename)
        {
            try
            {
                Logger.Debug("Loading project XML file, filename = " + filename);
                XmlDocument document = new XmlDocument();
                document.Load(filename);

                _projectFullPath = filename;

                //读取Project属性
                XmlNode xmlNode = document.SelectSingleNode("/GameProject/Project");
                if (xmlNode != null)
                {
                    _projectName = xmlNode.Attributes["Name"].Value;
                    _projectVersion = xmlNode.Attributes["GxVersion"].Value;
                }

                //读取SceneDir
                xmlNode = document.SelectSingleNode("/GameProject/ResourceDir/SceneDir");
                if (xmlNode != null)
                {
                    Logger.Error("Xml node [/GameProject/ResourceDir/SceneDir] not exists.");
                    return false;
                }
                _sceneDirPath = xmlNode.InnerText;

                //读取SoundDir
                xmlNode = document.SelectSingleNode("/GameProject/ResourceDir/SoundDir");
                if (xmlNode == null)
                {
                    Logger.Error("Xml node [/GameProject/ResourceDir/SoundDir] not exists.");
                    return false;
                }
                _soundDirPath = xmlNode.InnerText;

                //加载场景树
                xmlNode = document.SelectSingleNode("/GameProject/SceneList");
                if (xmlNode == null)
                {
                    Logger.Error("Xml node [/GameProject/SceneList] not exists.");
                    return false;
                }

                if (xmlNode.ChildNodes.Count > 0)
                {
                    foreach (XmlNode node in xmlNode.ChildNodes)
                    {
                        if (node.Name == "IncludeNode")
                        {
                            string name = node.Attributes["Name"].Value;
                            string fullPath = Path.GetFullPath(node.Attributes["Path"].Value);
                            string type = node.Attributes["Type"].Value;

                            //如果节点是目录
                            if (type == "DirectoryType")
                            {
                                GxSceneDirectory gxDirectory = new GxSceneDirectory(fullPath);
                                GxTreeNode gxTreeNode = new GxTreeNode(name, gxDirectory);
                                _sceneList.Add(gxTreeNode);

                                if (node.ChildNodes.Count > 0)
                                {
                                    loadNodes(node, gxTreeNode);
                                }
                            }
                            else if (type == "SceneFileType")
                            {
                                GxScene gxScene = new GxScene(fullPath);
                                GxTreeNode gxTreeNode = new GxTreeNode(name, gxScene);
                                _sceneList.Add(gxTreeNode);
                            }
                            else
                            {
                                Logger.Error("Invalid node type.");
                                return false;
                            }
                        }
                    }
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

        private void loadNodes(XmlNode childNode, GxTreeNode treeNode)
        {
            foreach (XmlNode node in childNode.ChildNodes)
            {
                if (node.Name == "IncludeNode")
                {
                    string name = node.Attributes["Name"].Value;
                    string fullPath = Path.GetFullPath(node.Attributes["Path"].Value);
                    string type = node.Attributes["Type"].Value;

                    //如果节点是目录
                    if (type == "DirectoryType")
                    {
                        GxSceneDirectory gxDirectory = new GxSceneDirectory(fullPath);
                        GxTreeNode gxTreeNode = new GxTreeNode(name, gxDirectory);
                        treeNode.Nodes.Add(gxTreeNode);

                        if (node.ChildNodes.Count > 0)
                        {
                            loadNodes(node, gxTreeNode);
                        }
                    }
                    else if (type == "SceneFileType")
                    {
                        GxScene gxScene = new GxScene(fullPath);
                        GxTreeNode gxTreeNode = new GxTreeNode(name, gxScene);
                        treeNode.Nodes.Add(gxTreeNode);
                    }
                }
            }
        }

        public void unload()
        {
            _isLoaded = false;
            _projectName = "";
            _projectFullPath = "";
            _sceneDirPath = "";
            _projectVersion = "";
            _sceneList.Clear();

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
        /// 获取场景列表
        /// </summary>
        /// <returns></returns>
        public List<GxTreeNode> getSceneList()
        {
            return _sceneList;
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
            return Path.GetDirectoryName(_projectFullPath).Replace("\\", "/");
        }

        public string getProjectSceneDir()
        {
            return _sceneDirPath;
        }

        public string getProjectVersion()
        {
            return _projectVersion;
        }

        public bool isLoaded()
        {
            return _isLoaded;
        }

        //获取游戏场景文件列表
        //返回场景文件路径列表
        public bool loadSceneList()
        {
            Logger.Debug("loading scene list, sceneDirPath = " + _sceneDirPath);
            if (Directory.Exists(_sceneDirPath) == false)
            {
                return false;
            }

            string [] fileList = Directory.EnumerateDirectories(_sceneDirPath, "*.*", SearchOption.AllDirectories).ToArray<string>();
            if (fileList.Length != 0)
            {
                foreach (string file in fileList)
                {
                    Debug.Print("Searched File [" + file +"]");
                }
            }

            return true;
        }
    }
}
