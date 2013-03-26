
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

local dll = package.loadlib("DLLForLua.dll", "luaopen_DLLForLua")
if dll then
	print("Load DLL OK!!!", dll)

	dll()
	DLLForLua.HelloWorld()

	local nSum = DLLForLua.Add(12345, 54321)
	print(nSum)

	dump()

	--[[
	for i, v in pairs(_G) do
		print(i, v)
	end
	--]]
else
	print("Load DLL failed...")
end
