
-- 文件内局部变量
local hello_file = "hello in file! 文件内局部变量"

-- 全局变量
hello_global = "Hello World!!!这是一个C++群！！！"

table_global = {
	1.23, 321, "abc",
	[34] = "kkk",
	id = 333,
	ttt = { 99, hh = hello_file, "vvv" }
}

-- 待 C 宿主程序修改的全局变量
modify_test = "variable to modify"

local function funcTest(...)
	print("\n\n")
	print("In Lua:")
	print(...)
	print("\n\n")
	print("In C++:")
	LuaTest.MyCPrint(...)
end

-- 注意：不能用 local 修饰 function
-- 若需要用 local 修饰 function，
-- 可创建与局部函数名相同名字的全局变量，并指向局部函数。
-- 利用此技巧可在 lua 文件末手动添加 OnEvent_%d 等全局变量，
-- 分别指向不同的局部事件函数，实现函数注册功能
-- 如下：
funcTest = funcTest

function funcWithRst(...)
	local n = 0
	local d = 0.314
	local t = { id = 1234, "somthing", 333 }
	local s = "PK Win 胜利！"

	print(#arg, arg.n, arg[1], arg[2])
	return n, d, t, s
end

function InvHW()
    LuaTest.GlobalAndField()

    local nMP1 = 123
    local nMP2 = 321
    local nRet = LuaTest.Multiply(nMP1, nMP2)
    print(nMP1 .. " * " .. nMP2 .. " = " .. nRet)

	print("modify_test = " .. modify_test)
	
	-- 注：变量 a 没有初始化，默认为 nil
    funcTest(1.2, "haha", true, nil, a, 123, { 123, "aad2331" }, InvHW, funcTest)
end

local function dump()
	local nLevel = 1
	while true do
		local info = debug.getinfo(nLevel, "nfSlu")
		if not info then break end

		for i, v in pairs(info) do
			print(string.format("level = %d %s %s", nLevel, tostring(i), tostring(v)))
		end

		nLevel = nLevel + 1
	end
end

dump()
print(123, "abc")

for i,v in pairs(LuaTest) do
	print(i, v)
end
