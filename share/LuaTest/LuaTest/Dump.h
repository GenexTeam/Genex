#pragma once

// ע�⣺һ��Ҫ���� extern "C"
extern "C"
{
	#include "inc/lualib.h"
	#include "inc/lauxlib.h"
}

// ��������������б�
extern int MyCPrint(lua_State* L);
