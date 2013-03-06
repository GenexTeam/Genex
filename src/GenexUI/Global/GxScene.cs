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
using System.Xml;

namespace GenexUI.Global
{
    public class GxScene : GxNodeDataBase
    {
        private string _sceneName;

        public GxScene(string filePath = "", XmlNode relatedXmlNode = null)
        {
            base.setPath(filePath);
            base.setRelatedXmlNode(relatedXmlNode);
        }

        public void setSceneName(string sceneName)
        {
            _sceneName = sceneName;
        }

        public string getSceneName()
        {
            return _sceneName;
        }
    }
}
