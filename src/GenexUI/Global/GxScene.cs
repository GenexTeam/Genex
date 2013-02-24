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
        private string _sceneFileFullPath;

        public bool load(string filename)
        {

            return true;
        }

        public void setSceneFileFullPath(string filename)
        {
            _sceneFileFullPath = filename;
        }

        public string getSceneFileFullPath()
        {
            return _sceneFileFullPath;
        }
    }
}
