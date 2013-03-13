// LuaTest.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <string.h>
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

int MyPrint(lua_State* L)
{
	// 获取参数数量
	int nArgs = lua_gettop(L);

	for (int i = 1; i <= nArgs; i++)
	{
		// 获取参数类型
		int nRet = 0;
		const void* pAddr = NULL;
		const char* szRet = NULL;
		lua_State* pL = NULL;
		lua_CFunction pFunc = 0;

		int nType = lua_type(L, i);
		switch (nType)
		{
			case LUA_TNIL:
				printf("参数 %d 类型为：NIL\n", i);
				break;

			case LUA_TBOOLEAN:
				nRet = lua_toboolean(L, i);
				printf("参数 %d 类型为：BOOLEAN value = %s\n", i, (!!nRet ? "true" : "false"));
				break;

			case LUA_TLIGHTUSERDATA:
				pAddr = lua_touserdata(L, i);
				printf("参数 %d 类型为：LIGHTUSERDATA pointer = 0x%08X\n", i, (int)pAddr);
				break;

			case LUA_TNUMBER:
				nRet = lua_tointeger(L, i);
				printf("参数 %d 类型为：NUMBER value = %d\n", i, nRet);
				break;

			case LUA_TSTRING:
				nRet = lua_objlen(L, i);
				szRet = lua_tostring(L, i);
				printf("参数 %d 类型为：STRING len = %d value = %s\n", i, nRet, szRet);
				break;

			case LUA_TTABLE:
				nRet = lua_objlen(L, i);
				pAddr = lua_topointer(L, i);
				printf("参数 %d 类型为：TABLE len = %d pointer = 0x%08X\n", i, nRet, (int)pAddr);
				break;

			case LUA_TFUNCTION:
				nRet = lua_objlen(L, i);
				pFunc = lua_tocfunction(L, i);
				printf("参数 %d 类型为：FUNCTION len = %d pointer = 0x%08X\n", i, nRet, (int)pFunc);
				break;

			case LUA_TUSERDATA:
				pAddr = lua_touserdata(L, i);
				printf("参数 %d 类型为：USERDATA pointer = 0x%08X\n", i, (int)pAddr);
				break;

			case LUA_TTHREAD:
				pL = lua_tothread(L, i);
				printf("参数 %d 类型为：THREAD pointer = 0x%08X\n", i, (int)pL);
				break;

			default:
				nRet = lua_objlen(L, i);
				printf("参数 %d 类型未知！len = %d", i, nRet);
				break;
		}
	}

	return 0;
}

int _tmain(int argc, _TCHAR* argv[])
{
	// 创建一个 Lua 虚拟机
	lua_State* L = lua_open();

	// 载入 Lua 基础库函数
	luaL_openlibs(L);

	//////////////////////////////////////////////////////////////////////////
	// 执行一行 Lua 代码
	//////////////////////////////////////////////////////////////////////////
	const char* szLua = "print(\"Good Job! 干得漂亮！\") for i = 1, 10 do print(i) end";
	
	// 载入代码
	luaL_loadbuffer(L, szLua, strlen(szLua), NULL);

	// 执行代码
	lua_pcall(L, 0, 0, 0);

	//////////////////////////////////////////////////////////////////////////
	// 载入 Lua 脚本
	//////////////////////////////////////////////////////////////////////////

	// 注册 C++ 函数，使 Lua 脚本可以调用
	luaL_register(L, "LuaTest", regFuncs);

	// C++ 调用 Lua 脚本(载入脚本完成时已执行了print部分)
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

