using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GenexUI.Global
{
    public class SceneFileSystemWatcher : FileSystemWatcherBase
    {
        /// <summary>
        /// 初始化文件系统监视器
        /// </summary>
        protected override void triggerEvents()
        {
            // *注：因为使用了过滤器，因此无法捕捉到文件夹，需要用其它方式判断是不是文件夹

            try
            {
                //文件改变事件
                _fileSystemWatcher.Changed += (sender, e) =>
                {
                    Logger.Debug(
                        "A file event has watched, type = " + e.ChangeType.ToString() +
                        ", path = " + e.FullPath +
                        ", filename = " + e.Name);


                    //文件内容发生变更
                    if (Directory.Exists(e.FullPath) == false)
                    {

                    }
                };

                //文件删除事件
                _fileSystemWatcher.Deleted += (sender, e) =>
                {
                    Logger.Warn(
                        "A file event has watched, type = " + e.ChangeType.ToString() +
                        ", path = " + e.FullPath +
                        ", filename = " + e.Name);

                    if (Directory.Exists(e.FullPath) == true)
                    {
                        //文件夹被删除
                        Logger.Debug("A directory deleted, path = " + e.FullPath);
                    }
                    else
                    {
                        //文件被删除
                        Logger.Debug("A file deleted, path = " + e.FullPath);
                    }
                };

                //文件名变更事件
                _fileSystemWatcher.Renamed += (sender, e) =>
                {
                    Logger.Warn(
                        "A file event has watched, type = " + e.ChangeType.ToString() +
                        ", path = " + e.FullPath +
                        ", filename = " + e.Name);

                    if (Directory.Exists(e.FullPath) == true)
                    {
                        //文件夹名变更
                        Logger.Debug("A directory renamed, path = " + e.FullPath + ", oldname = " + e.OldName);
                    }
                    else
                    {
                        //文件名变更
                        Logger.Debug("A file renamed, path = " + e.FullPath + ", oldname = " + e.OldName);
                    }
                };

                //文件创建事件
                _fileSystemWatcher.Created += (sender, e) =>
                {
                    Logger.Warn(
                        "A file event has watched, type = " + e.ChangeType.ToString() +
                        ", path = " + e.FullPath +
                        ", filename = " + e.Name);

                    if (Directory.Exists(e.FullPath) == true)
                    {
                        //文件夹被创建
                        Logger.Debug("A directory created, path = " + e.FullPath);
                    }
                    else
                    {
                        //文件被创建
                        Logger.Debug("A file created, path = " + e.FullPath);
                    }
                };
            }
            catch (DirectoryNotFoundException iox)
            {
                Logger.Error("\r\nEXCEPTION (onChanged): Directory No Found , " + iox.Message);
            }
            catch (FileNotFoundException iox)
            {
                Logger.Error("\r\nEXCEPTION (onChanged): File Not Found, " + iox.Message);
            }
            catch (IOException iox)
            {
                Logger.Error("\r\nEXCEPTION (onChanged): IO Error, " + iox.Message);
            }
            catch (Exception ex)
            {
                Logger.Error("\r\nEXCEPTION (onChanged): " + ex.Message);
            }
        }
    }
}
