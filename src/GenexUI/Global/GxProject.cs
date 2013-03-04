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
        private string _projectVersion;

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

                //读取SceneDirectory
                xmlNode = document.SelectSingleNode("/GameProject/SceneDirectory");
                if (xmlNode != null)
                {
                    _sceneDirPath = xmlNode.Attributes["Path"].Value;
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
            _sceneDirPath = _sceneDirPath.Replace("//", "/");
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVariableType.GXENV_PROJECT_SCENE_DIR, getProjectSceneDir());
            Logger.Debug("Updated project env variables");


            //加载场景
            loadSceneList();

            _isLoaded = true;
            return true;
        }

        public void unload()
        {
            _isLoaded = false;
            _projectName = "";
            _projectFullPath = "";
            _sceneDirPath = "";
            _projectVersion = "";

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
