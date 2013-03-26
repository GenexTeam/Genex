// LuaTest.cpp : �������̨Ӧ�ó������ڵ㡣
//

#include "stdafx.h"
#include <string.h>
#include "LuaTest.h"
#include "Dump.h"
#include "Stack.h"
#include "InvokeFunction.h"

const luaL_Reg regFuncs[] = {
	DefFunc(HelloWorld),
	DefFunc(Multiply),
	DefFunc(MyCPrint),
	{ NULL, NULL }
};

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

	// ��ȡȫ�ֱ���
	//const char* szVarName = "hello_file";
	const char* szVarName = "hello_global";
	lua_getglobal(L, szVarName);

	if (lua_isstring(L, -1))
	{
		const char* szHello = NULL;
		szHello = lua_tostring(L, -1);
		printf("%s\n", szHello);
	}
	else
	{
		printf("���� %s �����ַ�����\n", szVarName);
	}

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

int _tmain(int argc, _TCHAR* argv[])
{
	// ����һ�� Lua �����
	lua_State* L = lua_open();

	// ���� Lua �����⺯��
	luaL_openlibs(L);

	//////////////////////////////////////////////////////////////////////////
	// ��ջ��ʾ����
	//////////////////////////////////////////////////////////////////////////
	printf("��ջ��ʾ����\n");

	StackDemo();

	printf("//////////////////////////////////////////////////////////////////////////\n");

	//////////////////////////////////////////////////////////////////////////
	// ִ��һ�� Lua ����
	//////////////////////////////////////////////////////////////////////////
	printf("ִ��һ�� Lua ����\n");

	const char* szLua = "print(\"Good Job! �ɵ�Ư����\") for i = 1, 10 do print(i) end";

	// �������
	luaL_loadbuffer(L, szLua, strlen(szLua), NULL);

	// ִ�д���
	lua_pcall(L, 0, 0, 0);

	printf("//////////////////////////////////////////////////////////////////////////\n");

	//////////////////////////////////////////////////////////////////////////
	// ���� Lua �ű�
	//////////////////////////////////////////////////////////////////////////

	printf("���� Lua �ű�\n");

	// ע�� C++ ������ʹ Lua �ű����Ե���
	luaL_register(L, "LuaTest", regFuncs);

	// C++ ���� Lua �ű�(����ű����ʱ��ִ����print����)
	luaL_dofile(L, "Lua_Test.lua");

	// ִ�д������������ĺ���
	lua_Number nTmp = 1.254;
	const char* szTmp = "undefinded ���ԣ�";
	LuaArgument arg1(LUA_TNUMBER, &nTmp), arg2(LUA_TSTRING, (void *)szTmp);
	InvFunc(L, "funcWithRst", 4, 2, arg1, arg2);

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

	printf("//////////////////////////////////////////////////////////////////////////\n");

	// �ر� Lua �����
	lua_close(L);

	system("pause");
	return 0;
}
