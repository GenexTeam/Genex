#pragma once
#include <stdlib.h>

// ע�⣺һ��Ҫ���� extern "C"
extern "C"
{
#include "inc/lualib.h"
#include "inc/lauxlib.h"
}

#ifdef _DEBUG
#pragma comment(lib, "lua51d.lib")
#else
#pragma comment(lib, "lua51.lib")
#endif

#define DefFunc(x) { #x, x }

// ��� HelloWorld
static int HelloWorld(lua_State* L);

// �˷�
static int Multiply(lua_State* L);

// ������в���
static int MyPrint(lua_State* L);

// ��ջ��ʾ Demo
void StackDemo();

const luaL_Reg regFuncs[] = {
	DefFunc(HelloWorld),
	DefFunc(Multiply),
	DefFunc(MyPrint),
	{ NULL, NULL }
};