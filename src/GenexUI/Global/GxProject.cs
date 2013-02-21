using System;
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
                Debug.Print(exception.Message);
                return false;
            }

            //更新环境变量
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVariableType.GXENV_PROJECT_NAME, _projectName);
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVariableType.GXENV_PROJECT_FULL_PATH, _projectFullPath);
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVariableType.GXENV_PROJECT_FILE_NAME, getProjectFileName());
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVariableType.GXENV_PROJECT_FILE_NAME_WITHOUT_EXT, getProjectFileNameWithoutExt());
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVariableType.GXENV_PROJECT_DIR, getProjectDir());
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVariableType.GXENV_PROJECT_VERSION, getProjectVersion());
            GlobalObj.getEnvManager().updateEnvVariable(GxEnvVariableType.GXENV_PROJECT_SCENE_DIR, getProjectSceneDir());

            _isLoaded = true;
            return true;
        }

        public string getProjectName()
        {
            return _projectName;
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
            string str = GlobalObj.getEnvManager().resolveEnv(_sceneDirPath);
            str = str.Replace("//", "/");
            return str;
        }

        public string getProjectVersion()
        {
            return _projectVersion;
        }

        public bool isLoaded()
        {
            return _isLoaded;
        }
    }
}
