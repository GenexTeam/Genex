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
        private Dictionary<GxEnvVarType, GxEnvVariableData> _gxEvnVariableList = new Dictionary<GxEnvVarType, GxEnvVariableData>()
        {
            //以下环境变量需要加载工程才有效
            { GxEnvVarType.GXENV_PROJECT_NAME,                     new GxEnvVariableData("$(ProjectName)") },
            { GxEnvVarType.GXENV_PROJECT_FULL_PATH,                new GxEnvVariableData("$(ProjectFullPath)") },
            { GxEnvVarType.GXENV_PROJECT_FILE_NAME,                new GxEnvVariableData("$(ProjectFileName)") },
            { GxEnvVarType.GXENV_PROJECT_FILE_NAME_WITHOUT_EXT,    new GxEnvVariableData("$(ProjectFileNameWithoutExtension)") },
            { GxEnvVarType.GXENV_PROJECT_DIR,                      new GxEnvVariableData("$(ProjectDir)") },
            { GxEnvVarType.GXENV_PROJECT_SCENE_DIR,                new GxEnvVariableData("$(ProjectSceneDir)") },
            { GxEnvVarType.GXENV_PROJECT_VERSION,                  new GxEnvVariableData("$(ProjectVersion)") },

        };

        //更新环境变量的值
        public void updateEnvVariable(GxEnvVarType envVariableName, string value)
        {
            GxEnvVariableData envVariableData = _gxEvnVariableList[envVariableName];
            envVariableData.envVariableValue = value;

            Debug.Print("Env Updated [" + envVariableName + ", " + value + "]");
        }

        //根据环境变量ID获取环境变量的值
        public object getEnv(GxEnvVarType envVariableType)
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
            foreach (KeyValuePair<GxEnvVarType, GxEnvVariableData> v in _gxEvnVariableList)
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
            foreach (KeyValuePair<GxEnvVarType, GxEnvVariableData> v in _gxEvnVariableList)
            {
                str = str.Replace(v.Value.envVariableName, v.Value.envVariableValue);
            }

            return str;
        }

    }
}
