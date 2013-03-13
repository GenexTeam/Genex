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
				printf("���� %d ����Ϊ��NIL\n", i);
				break;

			case LUA_TBOOLEAN:
				bRet = lua_toboolean(L, i);
				printf("���� %d ����Ϊ��BOOLEAN value = %s\n", i, (!!bRet ? "true" : "false"));
				break;

			case LUA_TLIGHTUSERDATA:
				pAddr = lua_touserdata(L, i);
				printf("���� %d ����Ϊ��LIGHTUSERDATA pointer = 0x%08X\n", i, (int)pAddr);
				break;

			case LUA_TNUMBER:
				nRet = lua_tointeger(L, i);
				dblRet = lua_tonumber(L, i);
				if (nRet < dblRet) // ������
				{
					printf("���� %d ����Ϊ��NUMBER value = %lf\n", i, dblRet);
				}
				else
				{
					printf("���� %d ����Ϊ��NUMBER value = %ld\n", i, nRet);
				}
				break;

			case LUA_TSTRING:
				nLen = lua_objlen(L, i);
				szRet = lua_tostring(L, i);
				printf("���� %d ����Ϊ��STRING len = %u value = %s\n", i, nLen, szRet);
				break;

			case LUA_TTABLE:
				nLen = lua_objlen(L, i);
				pAddr = lua_topointer(L, i);
				printf("���� %d ����Ϊ��TABLE len = %u pointer = 0x%08X\n", i, nLen, (int)pAddr);
				break;

			case LUA_TFUNCTION:
				nLen = lua_objlen(L, i);
				pFunc = lua_tocfunction(L, i);
				printf("���� %d ����Ϊ��FUNCTION len = %u pointer = 0x%08X\n", i, nLen, (int)pFunc);
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
				nLen = lua_objlen(L, i);
				szRet = lua_typename(L, i);
				printf("���� %d ����δ֪��len = %u typename = %s\n", i, nLen, szRet);
				break;
		}
	}

	return 0;
}

void StackDemo()
{
	lua_State* L = luaL_newstate();

	// ��ʼ������ջ(ѹջ˳�򣺴�������)
	// ��������ֵ�� 1 ��ʼ������ < 0 ����������������� > 0 �������������
	lua_pushboolean(L, 1);
	lua_pushnumber(L, 10);
	lua_pushnil(L);
	lua_pushstring(L, "hello");
	// �����true 10 nil "hello"
	// ���ͣ��� 2 ��Ԫ�ؼ�Ϊ 10���� -2 ��Ԫ��(���� 3 ��Ԫ��)Ϊ nil
	MyPrint(L); 
	printf("\n");

	// ��ջ��(���ұ�)Ԫ�ػ���
	lua_insert(L, 3);
	// �����true 10 "hello" nil
	// ���ͣ�nil �� "hello" ����λ��
	MyPrint(L); printf("\n");

	// ��ջ��Ԫ������ָ��λ�ã�������λ�õ�Ԫ�ص���ջ
	lua_replace(L, 2);
	// �����true nil "hello"
	// ���ͣ�nil �� 10 ����λ�ã�10 ��ջ
	MyPrint(L); printf("\n");

	// ����ָ��λ�õ�Ԫ�ز�ѹ��ջ
	lua_pushvalue(L, -3);
	// �����true nil "hello" true
	// ���ͣ����� true ��ѹջ
	MyPrint(L); printf("\n");

	// ���ö�ջ��С������ջ��ԭ�������� nil ���
	lua_settop(L, 6);
	// �����true nil "hello" true nil nil
	// ���ͣ�ԭ��ջ��СΪ 4��������Ϊ 6�����·��䲿���� nil ���
	MyPrint(L);	printf("\n");

	// �Ƴ�һ��Ԫ��
	lua_remove(L, -3);
	// �����true nil "hello" nil nil
	// ���ͣ��Ƴ���������� 3 ��Ԫ��
	MyPrint(L);	printf("\n");

	// �������µĶ�ջ N ��ԭ��С���� N < 0����ԭ��ջֻ��������ʼ���� N ��Ԫ��
	lua_settop(L, -4);
	// �����true nil
	// ���ͣ�ֻ�����ӵ� 1 ��Ԫ�� ~ �� N ��Ԫ��
	MyPrint(L);	printf("\n");

	// ������ N == 0�����ջ�����
	lua_settop(L, 0);
	// �����(�����)
	// ���ͣ���ջ�����
	MyPrint(L);	printf("\n");

	lua_close(L);
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

