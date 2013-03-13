// LuaTest.cpp : �������̨Ӧ�ó������ڵ㡣
//

#include "stdafx.h"
#include "LuaTest.h"

int HelloWorld( lua_State* L )
{
	const int nExpectParams = 0;

	// ����������
	int nParams = lua_gettop(L);
	if (nParams != nExpectParams)
	{
		printf("������������ȷ������ = %d����ǰ = %d\n", nExpectParams, nParams);
		return 0;
	}

	printf("Hello World!!!\n");

	return 0;
}

int Multiply( lua_State* L )
{
	const int nExpectParams = 2;

	// ����������
	int nParams = lua_gettop(L);
	if (nParams != nExpectParams)
	{
		printf("������������ȷ������ = %d����ǰ = %d\n", nExpectParams, nParams);
		lua_pushinteger(L, 0);
	}
	else // ����������ȷ
	{
		// ����������
		if (!lua_isnumber(L, 1) ||
			!lua_isnumber(L, 2))
		{
			printf("�������Ͳ���ȷ��\n");
			lua_pushinteger(L, 0);
		}
		else // ����������ȷ
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
	// ����һ�� Lua �����
	lua_State* L = lua_open();

	// ���� Lua �����⺯��
	luaL_openlibs(L);

	// ע�� C++ ������ʹ Lua �ű����Ե���
	luaL_register(L, "LuaTest", regFuncs);

	// C++ ���� Lua �ű�
	luaL_dofile(L, "Lua_Test.lua");

	// ��ȡ Lua ����ָ�룬��ѹ���ջ
	lua_getglobal(L, "InvHW");

	if (!!lua_isfunction(L, -1))
	{
		// todo ѹ�����

		// ���� Lua ����
		int nRet = lua_pcall(L, 0, 0, 0);
		if (nRet != 0)
		{
			printf("Lua ����ִ��ʧ�ܣ�%d\n", nRet);
		}
	}
	else
	{
		printf("��ȡ Lua ����ָ��ʧ�ܣ�\n");
	}

	// �ر� Lua �����
	lua_close(L);

	system("pause");
	return 0;
}

