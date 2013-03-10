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
        GXENV_PROJECT_NAME,                     //当前打开的工程名称
        GXENV_PROJECT_FULL_PATH,                //当前打开的工程文件全路径
        GXENV_PROJECT_FILE_NAME,                //当前打开的工程文件名
        GXENV_PROJECT_FILE_NAME_WITHOUT_EXT,    //不带扩展名的工程文件名
        GXENV_PROJECT_DIR,                      //当前打开的工程目录路径
        GXENV_PROJECT_SCENE_DIR,                //当前打开的工程场景目录路径
        GXENV_PROJECT_VERSION,                  //当前打开的工程版本号
    };
}
