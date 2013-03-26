// LuaTest.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <string.h>
#include "LuaTest.h"
#include "Dump.h"
#include "Stack.h"
#include "InvokeFunction.h"

const luaL_Reg regFuncs[] = {
	DefFunc(HelloWorld),
	DefFunc(Multiply),
	DefFunc(MyCPrint),
	{ NULL, NULL }
};

int HelloWorld( lua_State* L )
{
	const int nExpectParams = 0;

	// 检查参数数量
	int nParams = lua_gettop(L);
	if (nParams != nExpectParams)
	{
		printf("参数数量不正确！期望 = %d，当前 = %d\n", nExpectParams, nParams);
		return 0;
	}

	// 获取全局变量
	//const char* szVarName = "hello_file";
	const char* szVarName = "hello_global";
	lua_getglobal(L, szVarName);

	if (lua_isstring(L, -1))
	{
		const char* szHello = NULL;
		szHello = lua_tostring(L, -1);
		printf("%s\n", szHello);
	}
	else
	{
		printf("变量 %s 不是字符串！\n", szVarName);
	}

	return 0;
}

int Multiply( lua_State* L )
{
	const int nExpectParams = 2;

	// 检查参数数量
	int nParams = lua_gettop(L);
	if (nParams != nExpectParams)
	{
		printf("参数数量不正确！期望 = %d，当前 = %d\n", nExpectParams, nParams);
		lua_pushinteger(L, 0);
	}
	else // 参数数量正确
	{
		printf("参数1类型：%d\n参数2类型：%d\n", lua_type(L, 1), lua_type(L, 2));

		// 检查参数类型
		if (!lua_isnumber(L, 1) ||
			!lua_isnumber(L, 2))
		{
			printf("参数类型不正确！\n");
			lua_pushinteger(L, 0);
		}
		else // 参数类型正确
		{
			int nMP1, nMP2;
			nMP1 = (int)lua_tonumber(L, 1);
			nMP2 = (int)lua_tonumber(L, 2);

			lua_pushinteger(L, nMP1 * nMP2);
		}
	}

	return 1;
}

int _tmain(int argc, _TCHAR* argv[])
{
	// 创建一个 Lua 虚拟机
	lua_State* L = lua_open();

	// 载入 Lua 基础库函数
	luaL_openlibs(L);

	//////////////////////////////////////////////////////////////////////////
	// 堆栈演示函数
	//////////////////////////////////////////////////////////////////////////
	printf("堆栈演示函数\n");

	StackDemo();

	printf("//////////////////////////////////////////////////////////////////////////\n");

	//////////////////////////////////////////////////////////////////////////
	// 执行一行 Lua 代码
	//////////////////////////////////////////////////////////////////////////
	printf("执行一行 Lua 代码\n");

	const char* szLua = "print(\"Good Job! 干得漂亮！\") for i = 1, 10 do print(i) end";

	// 载入代码
	luaL_loadbuffer(L, szLua, strlen(szLua), NULL);

	// 执行代码
	lua_pcall(L, 0, 0, 0);

	printf("//////////////////////////////////////////////////////////////////////////\n");

	//////////////////////////////////////////////////////////////////////////
	// 载入 Lua 脚本
	//////////////////////////////////////////////////////////////////////////

	printf("载入 Lua 脚本\n");

	// 注册 C++ 函数，使 Lua 脚本可以调用
	luaL_register(L, "LuaTest", regFuncs);

	// C++ 调用 Lua 脚本(载入脚本完成时已执行了print部分)
	luaL_dofile(L, "Lua_Test.lua");

	// 执行带不定长参数的函数
	lua_Number nTmp = 1.254;
	const char* szTmp = "undefinded 测试！";
	LuaArgument arg1(LUA_TNUMBER, &nTmp), arg2(LUA_TSTRING, (void *)szTmp);
	InvFunc(L, "funcWithRst", 4, 2, arg1, arg2);

	// 获取 Lua 函数指针，并压入堆栈
	lua_getglobal(L, "InvHW");

	if (!!lua_isfunction(L, -1))
	{
		// todo 压入参数

		// 调用 Lua 函数
		int nRet = lua_pcall(L, 0, 0, 0);
		if (nRet != 0)
		{
			printf("Lua 函数执行失败：%d\n", nRet);
		}
	}
	else
	{
		printf("获取 Lua 函数指针失败！\n");
	}

	printf("//////////////////////////////////////////////////////////////////////////\n");

	// 关闭 Lua 虚拟机
	lua_close(L);

	system("pause");
	return 0;
}
