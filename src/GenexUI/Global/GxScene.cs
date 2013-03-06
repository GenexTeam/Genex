//===================================================================
// File    : GxScene.cs
// Purpose : 管理游戏场景数据的类
// Author  : AngryPowman
// Created : 2013/2/22 12:40:01
//===================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenexUI.Global
{
    public class GxScene
    {
        private string _filePath;
        private string _sceneName;

        public GxScene(string filePath = "")
        {
            _filePath = filePath;
        }

        public void setSceneName(string sceneName)
        {
            _sceneName = sceneName;
        }

        public string getSceneName()
        {
            return _sceneName;
        }

        public void setFilePath(string fullPath)
        {
            _filePath = fullPath;
        }

        public string getFilePath()
        {
            return _filePath;
        }
    }
}
