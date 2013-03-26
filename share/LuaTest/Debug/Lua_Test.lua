-- 注意：不能用 local 修饰 function
function funcTest(...)
	print("\n\n")
	print("In Lua:")
	print(...)
	print("\n\n")
	print("In C++:")
	LuaTest.MyCPrint(...)
end

function funcWithRst(...)
	local n = 0
	local d = 0.314
	local t = { id = 1234, "somthing", 333 }
	local s = "PK Win 胜利！"

	print(#arg, arg.n, arg[1], arg[2])
	return n, d, t, s
end

function InvHW()
    LuaTest.HelloWorld()

    local nMP1 = 123
    local nMP2 = 321
    local nRet = LuaTest.Multiply(nMP1, nMP2)
    print(nMP1 .. " * " .. nMP2 .. " = " .. nRet)

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
