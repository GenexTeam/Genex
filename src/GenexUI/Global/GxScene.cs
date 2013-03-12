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
    public class GxScene : GxNodeData
    {
        private int _sceneId;
        private string _sceneName;

        public GxScene(int sceneId, string filePath = "", XmlNode relatedXmlNode = null)
        {
            base.setPath(filePath);
            base.setRelatedXmlNode(relatedXmlNode);
        }

        public void setSceneId(int id)
        {
            _sceneId = id;
        }

        public int getSceneId()
        {
            return _sceneId;
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
