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
extern int HelloWorld(lua_State* L);

// �˷�
extern int Multiply(lua_State* L);
