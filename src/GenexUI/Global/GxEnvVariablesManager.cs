//===================================================================
// File    : GxEnvVariablesManager
// Purpose : 管理环境变量的类
// Author  : AngryPowman
// Created : 2013/2/21 19:25:26
//===================================================================

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace GenexUI.Global
{
    /*
     * 环境变量说明参考文档 [/docs/01_Gx环境变量大全.doc]
     */

    //环境变量ID定义
    public enum GxEnvVariableType
    {
        GXENV_PROJECT_NAME,                     //当前打开的工程名称
        GXENV_PROJECT_FULL_PATH,                //当前打开的工程文件全路径
        GXENV_PROJECT_FILE_NAME,                //当前打开的工程文件名
        GXENV_PROJECT_FILE_NAME_WITHOUT_EXT,    //不带扩展名的工程文件名
        GXENV_PROJECT_DIR,                      //当前打开的工程目录路径
        GXENV_PROJECT_SCENE_DIR,                //当前打开的工程场景目录路径
        GXENV_PROJECT_VERSION,                  //当前打开的工程版本号
    };

    public class GxEnvVariableData
    {
        public GxEnvVariableData(string name)
        {
            envVariableName = name;
        }

        public string envVariableName;
        public string envVariableValue;
    };

    public class GxEnvManager
    {
        private Dictionary<GxEnvVariableType, GxEnvVariableData> _gxEvnVariableList = new Dictionary<GxEnvVariableType, GxEnvVariableData>()
        {
            //以下环境变量需要加载工程才有效
            { GxEnvVariableType.GXENV_PROJECT_NAME,                     new GxEnvVariableData("$(ProjectName)") },
            { GxEnvVariableType.GXENV_PROJECT_FULL_PATH,                new GxEnvVariableData("$(ProjectFullPath)") },
            { GxEnvVariableType.GXENV_PROJECT_FILE_NAME,                new GxEnvVariableData("$(ProjectFileName)") },
            { GxEnvVariableType.GXENV_PROJECT_FILE_NAME_WITHOUT_EXT,    new GxEnvVariableData("$(ProjectFileNameWithoutExtension)") },
            { GxEnvVariableType.GXENV_PROJECT_DIR,                      new GxEnvVariableData("$(ProjectDir)") },
            { GxEnvVariableType.GXENV_PROJECT_SCENE_DIR,                new GxEnvVariableData("$(ProjectSceneDir)") },
            { GxEnvVariableType.GXENV_PROJECT_VERSION,                  new GxEnvVariableData("$(ProjectVersion)") },

        };

        //更新环境变量的值
        public void updateEnvVariable(GxEnvVariableType envVariableName, string value)
        {
            GxEnvVariableData envVariableData = _gxEvnVariableList[envVariableName];
            envVariableData.envVariableValue = value;

            Debug.Print("Env Updated [" + envVariableName + ", " + value + "]");
        }

        //根据环境变量ID获取环境变量的值
        public object getEnv(GxEnvVariableType envVariableType)
        {
            GxEnvVariableData envVariableData = _gxEvnVariableList[envVariableType];
            if (envVariableData != null)
            {
                return envVariableData.envVariableValue;
            }

            return "";
        }

        //根据环境变量名获取环境变量的值
        public object getEnv(string envVariableName)
        {
            foreach (KeyValuePair<GxEnvVariableType, GxEnvVariableData> v in _gxEvnVariableList)
            {
                if (v.Value.envVariableName == envVariableName)
                {
                    return v.Value.envVariableValue;
                }
            }

            return "";
        }

        //替换字符串中的环境变量
        public string resolveEnv(string str)
        {
            string resolvedStr = str;
            foreach (KeyValuePair<GxEnvVariableType, GxEnvVariableData> v in _gxEvnVariableList)
            {
                str = str.Replace(v.Value.envVariableName, v.Value.envVariableValue);
            }

            return str;
        }

    }
}
