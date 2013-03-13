local dll = package.loadlib("DLLForLua.dll", "luaopen_DLLForLua")
if dll then
	print("Load DLL OK!!!", dll)

	dll()
	DLLForLua.HelloWorld()

	local nSum = DLLForLua.Add(12345, 54321)
	print(nSum)

	--[[
	for i, v in pairs(_G) do
		print(i, v)
	end
	--]]
else
	print("Load DLL failed...")
end
