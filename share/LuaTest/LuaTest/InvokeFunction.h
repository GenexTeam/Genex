#pragma once

struct LuaArgument
{
	int m_nArgType;				// LUA_TNIL��LUA_TBOOLEAN��LUA_TNUMBER��LUA_TSTRING��LUA_TTABLE ...(�μ� lua.h Line 69)
	void* m_pArg;				// ������Ҫ�ͷ�

	LuaArgument(int nType, void* pArg)
	{
		m_nArgType = nType;
		m_pArg = pArg;
	}
};

// ִ�д������������б�� Lua ����
// szFunc��Lua ��������
// nRst������ֵ����
// nArgs�������б��в�������
// �����б��в�������Ϊ LuaArgument
extern int InvFunc(lua_State* L, const char* szFunc, int nRst = 0, int nArgs = 0, ...);
