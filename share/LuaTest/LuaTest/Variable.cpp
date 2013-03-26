#include "stdafx.h"
#include "Dump.h"
#include "Variable.h"

int GlobalAndField( lua_State* L )
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
	lua_getglobal(L, szVarName); // 获取变量并压入堆栈
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

	// 将变量弹出堆栈
	lua_pop(L, 1);

	// 获取全局 table 变量
	lua_getglobal(L, "table_global"); // table 变量地址压栈
	if (!lua_istable(L, -1))
	{
		// 将 table_global 弹出堆栈
		lua_pop(L, 1);
		return 0;
	}

	// 取出 table_global[3]
	// 将索引 3 压栈
	lua_pushinteger(L, 3);

	// 目前 table_global 地址处于堆栈的 -2 处，以下这行代码执行了：
	// ①取出 table_global[3]
	// ②将索引 3 弹出堆栈
	// ③将 table_global[3] 压入堆栈
	lua_gettable(L, -2);

	if (lua_isstring(L, -1))
	{
		const char* szVar = lua_tostring(L, -1);
		printf("table_global[3] = %s\n", szVar);
	}

	// 将 table_global[3] 弹出堆栈
	lua_pop(L, 1);

	// 现在 table_global 在堆栈的 -1 处
	// 获取 table 里的 ttt 域(这也是个 table)，并压入堆栈
	lua_getfield(L, -1, "ttt");
	if (!lua_istable(L, -1))
	{
		// 将 table_global、table_global.ttt 弹出堆栈
		lua_pop(L, 2);
		return 0;
	}

	// 获取 hh 域的值并压入堆栈
	lua_getfield(L, -1, "hh");
	if (lua_isstring(L, -1))
	{
		const char* szVar = lua_tostring(L, -1);
		printf("table_global.ttt.hh = %s\n", szVar);
	}

	// 将 table_global、table_global.ttt、table_global.ttt.hh 弹出堆栈
	lua_pop(L, 3);

	// 修改 modify_test 变量的值
	// 获取 modify_test 的地址
	lua_getglobal(L, "modify_test");

	// 将新值压入堆栈
	lua_pushstring(L, "变量的值已修改！！！");

	// 修改变量，并将新值弹出堆栈
	lua_setglobal(L, "modify_test");

	return 0;
}
