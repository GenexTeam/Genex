
-- �ļ��ھֲ�����
local hello_file = "hello in file! �ļ��ھֲ�����"

-- ȫ�ֱ���
hello_global = "Hello World!!!����һ��C++Ⱥ������"

table_global = {
	1.23, 321, "abc",
	[34] = "kkk",
	id = 333,
	ttt = { 99, hh = hello_file, "vvv" }
}

-- �� C ���������޸ĵ�ȫ�ֱ���
modify_test = "variable to modify"

local function funcTest(...)
	print("\n\n")
	print("In Lua:")
	print(...)
	print("\n\n")
	print("In C++:")
	LuaTest.MyCPrint(...)
end

-- ע�⣺������ local ���� function
-- ����Ҫ�� local ���� function��
-- �ɴ�����ֲ���������ͬ���ֵ�ȫ�ֱ�������ָ��ֲ�������
-- ���ô˼��ɿ��� lua �ļ�ĩ�ֶ���� OnEvent_%d ��ȫ�ֱ�����
-- �ֱ�ָ��ͬ�ľֲ��¼�������ʵ�ֺ���ע�Ṧ��
-- ���£�
funcTest = funcTest

function funcWithRst(...)
	local n = 0
	local d = 0.314
	local t = { id = 1234, "somthing", 333 }
	local s = "PK Win ʤ����"

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
	
	-- ע������ a û�г�ʼ����Ĭ��Ϊ nil
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
