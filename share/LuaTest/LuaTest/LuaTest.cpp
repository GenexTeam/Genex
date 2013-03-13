// LuaTest.cpp : �������̨Ӧ�ó������ڵ㡣
//

#include "stdafx.h"
#include <string.h>
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
		printf("����1���ͣ�%d\n����2���ͣ�%d\n", lua_type(L, 1), lua_type(L, 2));

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

int MyPrint(lua_State* L)
{
	// ��ȡ��������
	int nArgs = lua_gettop(L);

	for (int i = 1; i <= nArgs; i++)
	{
		// ��ȡ��������
		int nRet = 0;
		const void* pAddr = NULL;
		const char* szRet = NULL;
		lua_State* pL = NULL;
		lua_CFunction pFunc = 0;

		int nType = lua_type(L, i);
		switch (nType)
		{
			case LUA_TNIL:
				printf("���� %d ����Ϊ��NIL\n", i);
				break;

			case LUA_TBOOLEAN:
				nRet = lua_toboolean(L, i);
				printf("���� %d ����Ϊ��BOOLEAN value = %s\n", i, (!!nRet ? "true" : "false"));
				break;

			case LUA_TLIGHTUSERDATA:
				pAddr = lua_touserdata(L, i);
				printf("���� %d ����Ϊ��LIGHTUSERDATA pointer = 0x%08X\n", i, (int)pAddr);
				break;

			case LUA_TNUMBER:
				nRet = lua_tointeger(L, i);
				printf("���� %d ����Ϊ��NUMBER value = %d\n", i, nRet);
				break;

			case LUA_TSTRING:
				nRet = lua_objlen(L, i);
				szRet = lua_tostring(L, i);
				printf("���� %d ����Ϊ��STRING len = %d value = %s\n", i, nRet, szRet);
				break;

			case LUA_TTABLE:
				nRet = lua_objlen(L, i);
				pAddr = lua_topointer(L, i);
				printf("���� %d ����Ϊ��TABLE len = %d pointer = 0x%08X\n", i, nRet, (int)pAddr);
				break;

			case LUA_TFUNCTION:
				nRet = lua_objlen(L, i);
				pFunc = lua_tocfunction(L, i);
				printf("���� %d ����Ϊ��FUNCTION len = %d pointer = 0x%08X\n", i, nRet, (int)pFunc);
				break;

			case LUA_TUSERDATA:
				pAddr = lua_touserdata(L, i);
				printf("���� %d ����Ϊ��USERDATA pointer = 0x%08X\n", i, (int)pAddr);
				break;

			case LUA_TTHREAD:
				pL = lua_tothread(L, i);
				printf("���� %d ����Ϊ��THREAD pointer = 0x%08X\n", i, (int)pL);
				break;

			default:
				nRet = lua_objlen(L, i);
				printf("���� %d ����δ֪��len = %d", i, nRet);
				break;
		}
	}

	return 0;
}

int _tmain(int argc, _TCHAR* argv[])
{
	// ����һ�� Lua �����
	lua_State* L = lua_open();

	// ���� Lua �����⺯��
	luaL_openlibs(L);

	//////////////////////////////////////////////////////////////////////////
	// ִ��һ�� Lua ����
	//////////////////////////////////////////////////////////////////////////
	const char* szLua = "print(\"Good Job! �ɵ�Ư����\") for i = 1, 10 do print(i) end";
	
	// �������
	luaL_loadbuffer(L, szLua, strlen(szLua), NULL);

	// ִ�д���
	lua_pcall(L, 0, 0, 0);

	//////////////////////////////////////////////////////////////////////////
	// ���� Lua �ű�
	//////////////////////////////////////////////////////////////////////////

	// ע�� C++ ������ʹ Lua �ű����Ե���
	luaL_register(L, "LuaTest", regFuncs);

	// C++ ���� Lua �ű�(����ű����ʱ��ִ����print����)
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

