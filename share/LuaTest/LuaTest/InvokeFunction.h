#pragma once

struct LuaArgument
{
	int m_nArgType;				// LUA_TNIL、LUA_TBOOLEAN、LUA_TNUMBER、LUA_TSTRING、LUA_TTABLE ...(参见 lua.h Line 69)
	void* m_pArg;				// 用完需要释放

	LuaArgument(int nType, void* pArg)
	{
		m_nArgType = nType;
		m_pArg = pArg;
	}
};

// 执行带不定长参数列表的 Lua 函数
// szFunc：Lua 函数名称
// nRst：返回值个数
// nArgs：参数列表中参数个数
// 参数列表中参数类型为 LuaArgument
extern int InvFunc(lua_State* L, const char* szFunc, int nRst = 0, int nArgs = 0, ...);
