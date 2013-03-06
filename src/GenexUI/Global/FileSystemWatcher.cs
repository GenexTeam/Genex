using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GenexUI.Global
{
    public abstract class FileSystemWatcherBase
    {
        //文件系统监视器
        protected FileSystemWatcher _fileSystemWatcher;

        /// <summary>
        /// 初始化文件系统监控对象
        /// </summary>
        /// <param name="path"></param>
        public void init(string path)
        {
            //初始化场景目录文件监控器
            _fileSystemWatcher = new FileSystemWatcher();
            _fileSystemWatcher.IncludeSubdirectories = true;
            _fileSystemWatcher.NotifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.Size;
            _fileSystemWatcher.Filter = "";
            _fileSystemWatcher.EnableRaisingEvents = false;
            _fileSystemWatcher.Path = path;
            Logger.Debug("Init FileSystemWatcher object OK.");

            triggerEvents();

            setEnabled(true);
        }

        /// <summary>
        /// 启动或关闭监控
        /// </summary>
        /// <param name="enabled"></param>
        public void setEnabled(bool enabled)
        {
            _fileSystemWatcher.EnableRaisingEvents = enabled;
            _fileSystemWatcher.WaitForChanged(WatcherChangeTypes.All, 1000);
        }

        /// <summary>
        /// 监控事件（子类实现）
        /// </summary>
        protected abstract void triggerEvents();
    }
}
