-- 使 _G.print 函数重定向到 C# 里面的自定义函数
local print = MyCSPrint

-- 因为这个函数后面要给 luaDemo_2.lua 使用
-- 所以这里不能用 local 修饰符
function Print(...)
	-- 将所有参数化为一个 table 传给 C# 的函数
	-- arg["n"] 为参数个数
	print(arg)
end

local t = { "123a", "cba223" }
Print(t)
Print("测试test~~~@$#!%@#$%&^(^&*)")
Print("测试test~~~@$#!%@#$%&^(^&*)", t)
