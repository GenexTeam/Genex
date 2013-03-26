#include "stdafx.h"
#include "Dump.h"

int MyCPrint( lua_State* L )
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
				printf("参数 %d 类型为：NUMBER value = %g\n", i, dblRet);
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
			printf("参数 %d 类型为：TABLE len = %u pointer = 0x%p\n", i, nLen, (int)pAddr);
			break;

		case LUA_TFUNCTION:
			nLen = lua_objlen(L, i);
			pFunc = lua_tocfunction(L, i);
			printf("参数 %d 类型为：FUNCTION len = %u pointer = 0x%p\n", i, nLen, (int)pFunc);
			break;

		case LUA_TUSERDATA:
			pAddr = lua_touserdata(L, i);
			printf("参数 %d 类型为：USERDATA pointer = 0x%p\n", i, (int)pAddr);
			break;

		case LUA_TTHREAD:
			pL = lua_tothread(L, i);
			printf("参数 %d 类型为：THREAD pointer = 0x%p\n", i, (int)pL);
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
