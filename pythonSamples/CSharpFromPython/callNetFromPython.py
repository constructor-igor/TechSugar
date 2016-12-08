import ctypes
source = 'CSharpLibrary.dll'
a = ctypes.cdll.LoadLibrary(source)

r = a.AddInt(3, 5)

getGreeting = a.GetGreeting
gResult = getGreeting()

getGreeting.restype = ctypes.c_char_p
gResult = getGreeting()

a.str.restype = ctypes.c_wchar
g = a.GetGreeting()
s = a.AddString("3", "5")
arr = a.AddArray(3,5)
