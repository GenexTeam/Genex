#include "stdafx.h"
#include "LuaTest.h"
#include "InvokeFunction.h"

int InvFunc( lua_State* L, const char* szFunc, int nRst /*= 0*/, int nArgs /*= 0*/, ... )
{
	// ����ԭ��ջ��С
	int nOrgStackSize = lua_gettop(L);

	// ��ȡ����ָ�룬��ѹ���ջ
	lua_getglobal(L, szFunc);

	// �Ƿ���
	if (lua_isfunction(L, -1))
	{
		// ������ѹ���ջ
		if (nArgs > 0)
		{
			va_list pArg;
			va_start(pArg, nArgs);

			int nParam = 0;
			for (int i = 0; i < nArgs; i++)
			{
				LuaArgument arg(LUA_TNONE, 0);
				arg = va_arg(pArg, LuaArgument);

				switch (arg.m_nArgType)
				{
					case LUA_TNIL:
						lua_pushnil(L);
						break;

					case LUA_TBOOLEAN:
						lua_pushboolean(L, *((bool *)arg.m_pArg));
						break;

					case LUA_TLIGHTUSERDATA:
						lua_pushlightuserdata(L, arg.m_pArg);
						break;

					case LUA_TNUMBER:
						lua_pushnumber(L, *((lua_Number *)arg.m_pArg));
						break;

					case LUA_TSTRING:
						lua_pushstring(L, (const char *)arg.m_pArg);
						break;

					//case LUA_TTABLE:
					//	break;

					case LUA_TFUNCTION:
						lua_pushcfunction(L, (lua_CFunction)arg.m_pArg);
						break;

					//case LUA_TUSERDATA:
					//	break;

					case LUA_TTHREAD:
						lua_pushthread(L);
						break;

					default:
						printf("���� %d ���Ͳ�֧�֣�type = %d\n", i, (int)arg.m_nArgType);
						break;
				}
			}

			va_end(pArg);
		}

		// ���� Lua ����
		if (lua_pcall(L, nArgs, nRst, 0))
		{
			printf("����[%s]ִ��ʧ�ܣ�\n%s\n", szFunc, lua_tostring(L, -1));
		}
		else // ����ִ�гɹ�
		{
			// ��ӡ���
			MyCPrint(L);
			printf("����[%s]ִ�гɹ���\n", szFunc);
		}
	}
	else // ���Ǻ���
	{
		printf("û�з��ֺ���[%s]\n", szFunc);
	}

	// �ָ���ջ��С
	lua_settop(L, nOrgStackSize);

	return 0;
}
