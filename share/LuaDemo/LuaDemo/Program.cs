using System;
using LuaInterface;

namespace LuaDemo
{
    class MyLuaEngine
    {
        public void MyCSPrint(LuaTable luaTbl)
        {
            // pairs
            //foreach (object oKey in luaTbl.Keys)
            //{
            //    if (oKey.ToString() == "n") // “参数个数”索引
            //        continue;

            //    Console.Write("{0}\t", luaTbl[oKey]);
            //}

            // ipairs
            for (int i = 1; i <= int.Parse(luaTbl["n"].ToString()); i++)
            {
                Console.Write("{0}\t", luaTbl[i]);
            }

            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Lua luaVM = new Lua();
            MyLuaEngine myLuaEngine = new MyLuaEngine();

            Console.WriteLine("LuaDemo Starting...");
            Console.WriteLine("By TZWSOHO 2012");

            luaVM.RegisterFunction("MyCSPrint", myLuaEngine, myLuaEngine.GetType().GetMethod("MyCSPrint"));
            
            // Lua 调用 C# 函数
            luaVM.DoFile("luaDemo_1.lua");
            luaVM.DoString("Print(nil, \"测试啊啊testing~~~\");");

            // C# 调用 Lua 函数
            luaVM.DoFile("luaDemo_2.lua");
            LuaFunction luaFunc = luaVM.GetFunction("MyLuaPrint");
            if (luaFunc != null)
            {
                object[] objRet = luaFunc.Call("abc", 123);
                Console.WriteLine("函数返回 {0} 个参数！", objRet.Length);
            }
            else
            {
                Console.WriteLine("获取 lua 函数失败！");
            }

            Console.WriteLine("按任意键退出...");
            Console.ReadKey();
        }
    }
}
