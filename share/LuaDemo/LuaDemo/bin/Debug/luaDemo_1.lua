-- ʹ _G.print �����ض��� C# ������Զ��庯��
local print = MyCSPrint

-- ��Ϊ�����������Ҫ�� luaDemo_2.lua ʹ��
-- �������ﲻ���� local ���η�
function Print(...)
	-- �����в�����Ϊһ�� table ���� C# �ĺ���
	-- arg["n"] Ϊ��������
	print(arg)
end

local t = { "123a", "cba223" }
Print(t)
Print("����test~~~@$#!%@#$%&^(^&*)")
Print("����test~~~@$#!%@#$%&^(^&*)", t)
