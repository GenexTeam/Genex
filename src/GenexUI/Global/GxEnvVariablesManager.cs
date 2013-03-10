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

    public static class GxEnvManager
    {
        private static Dictionary<GxEnvVarType, GxEnvVariableData> _gxEvnVariableList = new Dictionary<GxEnvVarType, GxEnvVariableData>()
        {
            //以下环境变量需要加载工程才有效
            { GxEnvVarType.GXENV_PROJECT_NAME,              new GxEnvVariableData("$(ProjectName)") },
            { GxEnvVarType.GXENV_PROJECT_PATH,              new GxEnvVariableData("$(ProjectPath)") },
            { GxEnvVarType.GXENV_PROJECT_FILENAME_EXT,      new GxEnvVariableData("$(ProjectFileNameExt)") },
            { GxEnvVarType.GXENV_PROJECT_FILENAME,          new GxEnvVariableData("$(ProjectFileName)") },
            { GxEnvVarType.GXENV_PROJECT_FILE_EXT,          new GxEnvVariableData("$(ProjectFileExt)") },
            { GxEnvVarType.GXENV_PROJECT_DIR,               new GxEnvVariableData("$(ProjectDir)") },
            { GxEnvVarType.GXENV_PROJECT_SCENE_DIR,         new GxEnvVariableData("$(ProjectSceneDir)") },
            { GxEnvVarType.GXENV_PROJECT_SCENE_DIR_NAME,    new GxEnvVariableData("$(ProjectSceneDirName)") },
            { GxEnvVarType.GXENV_PROJECT_VERSION,           new GxEnvVariableData("$(ProjectVersion)") },
        };

        //更新环境变量的值
        public static void updateEnvVariable(GxEnvVarType envVariableName, string value)
        {
            GxEnvVariableData envVariableData = _gxEvnVariableList[envVariableName];
            envVariableData.envVariableValue = value;

            Logger.Info("Env Updated [" + envVariableName + ", " + value + "]");
        }

        //根据环境变量ID获取环境变量的值
        public static object getEnv(GxEnvVarType envVariableType)
        {
            GxEnvVariableData envVariableData = _gxEvnVariableList[envVariableType];
            if (envVariableData != null)
            {
                return envVariableData.envVariableValue;
            }

            return "";
        }

        //根据环境变量名获取环境变量的值
        public static object getEnv(string envVariableName)
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
        public static string resolveEnv(string str)
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
