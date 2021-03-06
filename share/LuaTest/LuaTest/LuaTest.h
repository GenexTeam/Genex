#pragma once
#include <stdlib.h>

// 注意：一定要加上 extern "C"
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

// 输出 HelloWorld
extern int HelloWorld(lua_State* L);

// 乘法
extern int Multiply(lua_State* L);
