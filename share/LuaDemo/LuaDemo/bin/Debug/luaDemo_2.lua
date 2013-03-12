-- 这里不能用 local 修饰符
function MyLuaPrint(...)
	local t = { "haha", 123, szContent }
	--require("luaDemo_1")
	--dofile("luaDemo_1.lua")
	loadfile("luaDemo_1.lua")
	Print(...) -- 调用 luaDemo_1.lua 里面的打印函数

	return 2012, t, "abc"
end
