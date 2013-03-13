// DLLForLua.cpp : ���� DLL Ӧ�ó���ĵ���������
//

#include "stdafx.h"
#include "DLLForLua.h"

#define R(funcName) { #funcName, funcName }

static int HelloWorld(lua_State* L)
{
	MessageBox(NULL, TEXT("Hello World!!!"), NULL, MB_OK);

	return 0;
}

static int Add(lua_State* L)
{
	if (lua_gettop(L) == 2) // ��������
	{
		if (!!lua_isnumber(L, 1) &&
			!!lua_isnumber(L, 2)) // ����������������
		{
			int nAdd1, nAdd2;
			nAdd1 = (int)lua_tonumber(L, 1);
			nAdd2 = (int)lua_tonumber(L, 2);

			lua_pushinteger(L, nAdd1 + nAdd2);

			return 1; // ���� 1 ��ʾ���� 1 ������
		}
	}

	lua_pushinteger(L, 0); // ��������
	return 1;
}

static const luaL_Reg regFuncs[] = {
	R(HelloWorld),
	R(Add),
	{ NULL, NULL }
};

__declspec(dllexport) int luaopen_DLLForLua(lua_State* L)
{
	//MessageBox(NULL, TEXT("Register C++ Functions..."), NULL, MB_OK);

	luaL_register(L, "DLLForLua", regFuncs);
	return 1;
}
