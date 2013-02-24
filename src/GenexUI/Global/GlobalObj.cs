using GenexUI.Global.LogServer;
//===================================================================
// File    : GlobalObj.cs
// Purpose : 
// Author  : AngryPowman
// Created : 2013/2/22 0:32:07
//===================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenexUI.Global
{
    public class GlobalObj
    {
        public static bool init()
        {
            _gxEnvManager = new GxEnvManager();
            _openningProject = new GxProject();
            _logServer = new LogServer();
            _logServer.init();

            return true;
        }

        //环境变量管理器
        private static GxEnvManager _gxEnvManager;
        public static GxEnvManager getEnvManager()
        {
            return _gxEnvManager;
        }

        //当前打开的工程
        private static GxProject _openningProject;
        public static GxProject getOpenningProject()
        {
            return _openningProject;
        }

        //日志服务器
        private static LogServer _logServer;
        public static LogServer getLogServer()
        {
            return _logServer;
        }
    }
}
