using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenexUI.Global
{
    /*
     * 环境变量说明参考文档 [/doc/01_Gx环境变量大全.doc]
     */

    //环境变量ID定义
    public enum GxEnvVarType
    {
        GXENV_PROJECT_NAME,                     //工程名称
        GXENV_PROJECT_PATH,                     //工程文件全路径
        GXENV_PROJECT_FILENAME_EXT,             //工程文件名（带扩展名）
        GXENV_PROJECT_FILENAME,                 //工程文件名（不带扩展名）
        GXENV_PROJECT_FILE_EXT,                 //工程文件的扩展名
        GXENV_PROJECT_DIR,                      //工程目录路径
        GXENV_PROJECT_SCENE_DIR,                //游戏场景文件目录路径
        GXENV_PROJECT_SCENE_DIR_NAME,           //游戏场景文件目录名称
        GXENV_PROJECT_VERSION                   //当前打开的工程版本号
    };
}
