using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenexUI.Global
{
    public class GxSceneDirectory
    {
        private string _dirPath;

        public GxSceneDirectory(string dirPath = "")
        {
            _dirPath = dirPath;
        }

        public void setPath(string dirpath)
        {
            _dirPath = dirpath;
        }

        public string getPath()
        {
            return _dirPath;
        }
    }
}
