﻿//===================================================================
// File    : GlobalObj.cs
// Purpose : 
// Author  : AngryPowman
// Created : 2013/2/22 0:32:07
//===================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Reflection;

namespace GenexUI.Global
{
    public class GlobalObj
    {
        public static bool init()
        {
            _openningProject = new GxProject();
            return true;
        }

        //当前打开的工程
        private static GxProject _openningProject;
        public static GxProject getOpenningProject()
        {
            return _openningProject;
        }
    }
}
