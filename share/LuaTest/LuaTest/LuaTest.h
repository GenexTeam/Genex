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
static int HelloWorld(lua_State* L);

// 乘法
static int Multiply(lua_State* L);

const luaL_Reg regFuncs[] = {
	DefFunc(HelloWorld),
	DefFunc(Multiply),
	{ NULL, NULL }
};
