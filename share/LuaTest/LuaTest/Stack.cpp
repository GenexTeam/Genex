#include "stdafx.h"
#include "Dump.h"
#include "LuaTest.h"
#include "Stack.h"

void StackDemo()
{
	lua_State* L = luaL_newstate();

	// 初始化虚拟栈(压栈顺序：从左往右)
	// 索引绝对值从 1 开始，索引 < 0 则从右往左数起，索引 > 0 则从左往右数起
	lua_pushboolean(L, 1);
	lua_pushnumber(L, 10);
	lua_pushnil(L);
	lua_pushstring(L, "hello");
	// 结果：true 10 nil "hello"
	// 解释：第 2 个元素即为 10，第 -2 个元素(即第 3 个元素)为 nil
	MyCPrint(L); printf("\n");

	// 和栈顶(最右边)元素互换
	lua_insert(L, 3);
	// 结果：true 10 "hello" nil
	// 解释：nil 和 "hello" 互换位置
	MyCPrint(L); printf("\n");

	// 将栈顶元素移至指定位置，并将该位置的元素弹出栈
	lua_replace(L, 2);
	// 结果：true nil "hello"
	// 解释：nil 和 10 互换位置，10 出栈
	MyCPrint(L); printf("\n");

	// 复制指定位置的元素并压入栈
	lua_pushvalue(L, -3);
	// 结果：true nil "hello" true
	// 解释：复制 true 并压栈
	MyCPrint(L); printf("\n");

	// 设置堆栈大小，若堆栈比原来大，则用 nil 填充
	lua_settop(L, 6);
	// 结果：true nil "hello" true nil nil
	// 解释：原堆栈大小为 4，现设置为 6，则新分配部分用 nil 填充
	MyCPrint(L); printf("\n");

	// 移除一个元素
	lua_remove(L, -3);
	// 结果：true nil "hello" nil nil
	// 解释：移除从右数起第 3 个元素
	MyCPrint(L); printf("\n");

	// 若设置新的堆栈 N 比原来小，或 N < 0，则原堆栈只保留从左开始至第 N 个元素
	lua_settop(L, -4);
	// 结果：true nil
	// 解释：只保留从第 1 个元素 ~ 第 N 个元素
	MyCPrint(L); printf("\n");

	// 若设置 N == 0，则堆栈被清空
	lua_settop(L, 0);
	// 结果：(无输出)
	// 解释：堆栈被清空
	MyCPrint(L); printf("\n");

	lua_close(L);
}
