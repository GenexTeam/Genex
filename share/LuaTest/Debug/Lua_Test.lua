local function funcTest(...)
	print("\n\n")
	print("In Lua:")
	print(...)
	print("\n\n")
	print("In C++:")
	LuaTest.MyPrint(...)
end

-- 注意：不能用 local 修饰 function
function InvHW()
    LuaTest.HelloWorld()

    local nMP1 = 123
    local nMP2 = 321
    local nRet = LuaTest.Multiply(nMP1, nMP2)
    print(nMP1 .. " * " .. nMP2 .. " = " .. nRet)

	-- 注：变量 a 没有初始化，默认为 nil
    funcTest("haha", true, nil, a, 123, { 123, "aad2331" }, InvHW, funcTest)
end

print(123, "abc")
