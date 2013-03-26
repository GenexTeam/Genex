#include "stdafx.h"
#include "LuaTest.h"
#include "InvokeFunction.h"

int InvFunc( lua_State* L, const char* szFunc, int nRst /*= 0*/, int nArgs /*= 0*/, ... )
{
	// 保存原堆栈大小
	int nOrgStackSize = lua_gettop(L);

	// 获取函数指针，并压入堆栈
	lua_getglobal(L, szFunc);

	// 是否函数
	if (lua_isfunction(L, -1))
	{
		// 将参数压入堆栈
		if (nArgs > 0)
		{
			va_list pArg;
			va_start(pArg, nArgs);

			int nParam = 0;
			for (int i = 0; i < nArgs; i++)
			{
				LuaArgument arg(LUA_TNONE, 0);
				arg = va_arg(pArg, LuaArgument);

				switch (arg.m_nArgType)
				{
					case LUA_TNIL:
						lua_pushnil(L);
						break;

					case LUA_TBOOLEAN:
						lua_pushboolean(L, *((bool *)arg.m_pArg));
						break;

					case LUA_TLIGHTUSERDATA:
						lua_pushlightuserdata(L, arg.m_pArg);
						break;

					case LUA_TNUMBER:
						lua_pushnumber(L, *((lua_Number *)arg.m_pArg));
						break;

					case LUA_TSTRING:
						lua_pushstring(L, (const char *)arg.m_pArg);
						break;

					//case LUA_TTABLE:
					//	break;

					case LUA_TFUNCTION:
						lua_pushcfunction(L, (lua_CFunction)arg.m_pArg);
						break;

					//case LUA_TUSERDATA:
					//	break;

					case LUA_TTHREAD:
						lua_pushthread(L);
						break;

					default:
						printf("参数 %d 类型不支持！type = %d\n", i, (int)arg.m_nArgType);
						break;
				}
			}

			va_end(pArg);
		}

		// 调用 Lua 函数
		if (lua_pcall(L, nArgs, nRst, 0))
		{
			printf("函数[%s]执行失败：\n%s\n", szFunc, lua_tostring(L, -1));
		}
		else // 函数执行成功
		{
			// 打印结果
			MyCPrint(L);
			printf("函数[%s]执行成功！\n", szFunc);
		}
	}
	else // 不是函数
	{
		printf("没有发现函数[%s]\n", szFunc);
	}

	// 恢复堆栈大小
	lua_settop(L, nOrgStackSize);

	return 0;
}
