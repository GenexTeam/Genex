local function funcTest(...)
	print("\n\n")
	print("In Lua:")
	print(...)
	print("\n\n")
	print("In C++:")
	LuaTest.MyPrint(...)
end

-- ע�⣺������ local ���� function
function InvHW()
    LuaTest.HelloWorld()

    local nMP1 = 123
    local nMP2 = 321
    local nRet = LuaTest.Multiply(nMP1, nMP2)
    print(nMP1 .. " * " .. nMP2 .. " = " .. nRet)

	-- ע������ a û�г�ʼ����Ĭ��Ϊ nil
    funcTest("haha", true, nil, a, 123, { 123, "aad2331" }, InvHW, funcTest)
end

print(123, "abc")
