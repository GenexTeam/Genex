-- ���ﲻ���� local ���η�
function MyLuaPrint(...)
	local t = { "haha", 123, szContent }
	--require("luaDemo_1")
	--dofile("luaDemo_1.lua")
	loadfile("luaDemo_1.lua")
	Print(...) -- ���� luaDemo_1.lua ����Ĵ�ӡ����

	return 2012, t, "abc"
end
