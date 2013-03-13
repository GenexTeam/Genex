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
		size_t nLen = 0;
		int bRet = 0;
		LUA_INTEGER nRet = 0;
		LUA_NUMBER dblRet = 0;
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
				bRet = lua_toboolean(L, i);
				printf("参数 %d 类型为：BOOLEAN value = %s\n", i, (!!bRet ? "true" : "false"));
				break;

			case LUA_TLIGHTUSERDATA:
				pAddr = lua_touserdata(L, i);
				printf("参数 %d 类型为：LIGHTUSERDATA pointer = 0x%08X\n", i, (int)pAddr);
				break;

			case LUA_TNUMBER:
				nRet = lua_tointeger(L, i);
				dblRet = lua_tonumber(L, i);
				if (nRet < dblRet) // 浮点数
				{
					printf("参数 %d 类型为：NUMBER value = %lf\n", i, dblRet);
				}
				else
				{
					printf("参数 %d 类型为：NUMBER value = %ld\n", i, nRet);
				}
				break;

			case LUA_TSTRING:
				nLen = lua_objlen(L, i);
				szRet = lua_tostring(L, i);
				printf("参数 %d 类型为：STRING len = %u value = %s\n", i, nLen, szRet);
				break;

			case LUA_TTABLE:
				nLen = lua_objlen(L, i);
				pAddr = lua_topointer(L, i);
				printf("参数 %d 类型为：TABLE len = %u pointer = 0x%08X\n", i, nLen, (int)pAddr);
				break;

			case LUA_TFUNCTION:
				nLen = lua_objlen(L, i);
				pFunc = lua_tocfunction(L, i);
				printf("参数 %d 类型为：FUNCTION len = %u pointer = 0x%08X\n", i, nLen, (int)pFunc);
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
				nLen = lua_objlen(L, i);
				szRet = lua_typename(L, i);
				printf("参数 %d 类型未知！len = %u typename = %s\n", i, nLen, szRet);
				break;
		}
	}

	return 0;
}

void StackDemo()
{
	lua_State* L = luaL_newstate();

	// 初始化虚拟栈(压栈顺序：从左往右)
	// 索引绝对值从 1 开始，索引 < 0 则从右往左数起，索引 > 0 则从左往右数起
	lua_pushboolean(L, 1);
	lua_pushnumber(L, 10);
	lua_pushnil(L);
	lua_pushstring(L, "hello");
	// 结果：true 10 nil "hello"
	// 解释：第 2 个元素即为 10，第 -2 个元素(即第 3 个元素)为 nil
	MyPrint(L); 
	printf("\n");

	// 和栈顶(最右边)元素互换
	lua_insert(L, 3);
	// 结果：true 10 "hello" nil
	// 解释：nil 和 "hello" 互换位置
	MyPrint(L); printf("\n");

	// 将栈顶元素移至指定位置，并将该位置的元素弹出栈
	lua_replace(L, 2);
	// 结果：true nil "hello"
	// 解释：nil 和 10 互换位置，10 出栈
	MyPrint(L); printf("\n");

	// 复制指定位置的元素并压入栈
	lua_pushvalue(L, -3);
	// 结果：true nil "hello" true
	// 解释：复制 true 并压栈
	MyPrint(L); printf("\n");

	// 设置堆栈大小，若堆栈比原来大，则用 nil 填充
	lua_settop(L, 6);
	// 结果：true nil "hello" true nil nil
	// 解释：原堆栈大小为 4，现设置为 6，则新分配部分用 nil 填充
	MyPrint(L);	printf("\n");

	// 移除一个元素
	lua_remove(L, -3);
	// 结果：true nil "hello" nil nil
	// 解释：移除从右数起第 3 个元素
	MyPrint(L);	printf("\n");

	// 若设置新的堆栈 N 比原来小，或 N < 0，则原堆栈只保留从左开始至第 N 个元素
	lua_settop(L, -4);
	// 结果：true nil
	// 解释：只保留从第 1 个元素 ~ 第 N 个元素
	MyPrint(L);	printf("\n");

	// 若设置 N == 0，则堆栈被清空
	lua_settop(L, 0);
	// 结果：(无输出)
	// 解释：堆栈被清空
	MyPrint(L);	printf("\n");

	lua_close(L);
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

