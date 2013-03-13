// LuaTest.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "LuaTest.h"

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

	printf("Hello World!!!\n");

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

	// 注册 C++ 函数，使 Lua 脚本可以调用
	luaL_register(L, "LuaTest", regFuncs);

	// C++ 调用 Lua 脚本
	luaL_dofile(L, "Lua_Test.lua");

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

	// 关闭 Lua 虚拟机
	lua_close(L);

	system("pause");
	return 0;
}

