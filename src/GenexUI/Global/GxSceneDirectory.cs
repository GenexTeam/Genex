using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenexUI.Global
{
    public class GxSceneDirectory
    {
        private string _directoryPath;

        public void setDirectoryPath(string dirpath)
        {
            _directoryPath = dirpath;
        }

        public string getDirectoryPath()
        {
            return _directoryPath;
        }
    }
}
