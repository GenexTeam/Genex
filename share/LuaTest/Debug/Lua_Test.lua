-- 注意：不能用 local 修饰 function
function InvHW()
    LuaTest.HelloWorld()

    local nMP1 = 123
    local nMP2 = 321
    local nRet = LuaTest.Multiply(nMP1, nMP2)
    print(nMP1 .. " * " .. nMP2 .. " = " .. nRet)

    print("haha")
end

print(123, "abc")
