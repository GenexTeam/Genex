#pragma once

// ע�⣺һ��Ҫ���� extern "C"
extern "C"
{
#include "inc/lauxlib.h"
};

#ifdef _DEBUG
#pragma comment(lib, "lua51d.lib")
#else
#pragma comment(lib, "lua51.lib")
#endif
