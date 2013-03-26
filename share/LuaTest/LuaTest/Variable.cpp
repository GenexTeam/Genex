#include "stdafx.h"
#include "Dump.h"
#include "Variable.h"

int GlobalAndField( lua_State* L )
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
	lua_getglobal(L, szVarName); // ��ȡ������ѹ���ջ
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

	// ������������ջ
	lua_pop(L, 1);

	// ��ȡȫ�� table ����
	lua_getglobal(L, "table_global"); // table ������ַѹջ
	if (!lua_istable(L, -1))
	{
		// �� table_global ������ջ
		lua_pop(L, 1);
		return 0;
	}

	// ȡ�� table_global[3]
	// ������ 3 ѹջ
	lua_pushinteger(L, 3);

	// Ŀǰ table_global ��ַ���ڶ�ջ�� -2 �����������д���ִ���ˣ�
	// ��ȡ�� table_global[3]
	// �ڽ����� 3 ������ջ
	// �۽� table_global[3] ѹ���ջ
	lua_gettable(L, -2);

	if (lua_isstring(L, -1))
	{
		const char* szVar = lua_tostring(L, -1);
		printf("table_global[3] = %s\n", szVar);
	}

	// �� table_global[3] ������ջ
	lua_pop(L, 1);

	// ���� table_global �ڶ�ջ�� -1 ��
	// ��ȡ table ��� ttt ��(��Ҳ�Ǹ� table)����ѹ���ջ
	lua_getfield(L, -1, "ttt");
	if (!lua_istable(L, -1))
	{
		// �� table_global��table_global.ttt ������ջ
		lua_pop(L, 2);
		return 0;
	}

	// ��ȡ hh ���ֵ��ѹ���ջ
	lua_getfield(L, -1, "hh");
	if (lua_isstring(L, -1))
	{
		const char* szVar = lua_tostring(L, -1);
		printf("table_global.ttt.hh = %s\n", szVar);
	}

	// �� table_global��table_global.ttt��table_global.ttt.hh ������ջ
	lua_pop(L, 3);

	// �޸� modify_test ������ֵ
	// ��ȡ modify_test �ĵ�ַ
	lua_getglobal(L, "modify_test");

	// ����ֵѹ���ջ
	lua_pushstring(L, "������ֵ���޸ģ�����");

	// �޸ı�����������ֵ������ջ
	lua_setglobal(L, "modify_test");

	return 0;
}
