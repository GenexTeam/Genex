#pragma once

// 注意：一定要加上 extern "C"
extern "C"
{
	#include "inc/lualib.h"
	#include "inc/lauxlib.h"
}

// 输出不定长参数列表
extern int MyCPrint(lua_State* L);
