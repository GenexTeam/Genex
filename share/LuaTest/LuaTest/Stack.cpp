#include "stdafx.h"
#include "Dump.h"
#include "LuaTest.h"
#include "Stack.h"

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
	MyCPrint(L); printf("\n");

	// ��ջ��(���ұ�)Ԫ�ػ���
	lua_insert(L, 3);
	// �����true 10 "hello" nil
	// ���ͣ�nil �� "hello" ����λ��
	MyCPrint(L); printf("\n");

	// ��ջ��Ԫ������ָ��λ�ã�������λ�õ�Ԫ�ص���ջ
	lua_replace(L, 2);
	// �����true nil "hello"
	// ���ͣ�nil �� 10 ����λ�ã�10 ��ջ
	MyCPrint(L); printf("\n");

	// ����ָ��λ�õ�Ԫ�ز�ѹ��ջ
	lua_pushvalue(L, -3);
	// �����true nil "hello" true
	// ���ͣ����� true ��ѹջ
	MyCPrint(L); printf("\n");

	// ���ö�ջ��С������ջ��ԭ�������� nil ���
	lua_settop(L, 6);
	// �����true nil "hello" true nil nil
	// ���ͣ�ԭ��ջ��СΪ 4��������Ϊ 6�����·��䲿���� nil ���
	MyCPrint(L); printf("\n");

	// �Ƴ�һ��Ԫ��
	lua_remove(L, -3);
	// �����true nil "hello" nil nil
	// ���ͣ��Ƴ���������� 3 ��Ԫ��
	MyCPrint(L); printf("\n");

	// �������µĶ�ջ N ��ԭ��С���� N < 0����ԭ��ջֻ��������ʼ���� N ��Ԫ��
	lua_settop(L, -4);
	// �����true nil
	// ���ͣ�ֻ�����ӵ� 1 ��Ԫ�� ~ �� N ��Ԫ��
	MyCPrint(L); printf("\n");

	// ������ N == 0�����ջ�����
	lua_settop(L, 0);
	// �����(�����)
	// ���ͣ���ջ�����
	MyCPrint(L); printf("\n");

	lua_close(L);
}
