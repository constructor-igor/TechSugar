import ctypes
source = 'CSharpLibrary.dll'
a = ctypes.cdll.LoadLibrary(source)
r = a.AddInt(3, 5)
s = a.AddString('3', '5')
a.AddArray(3,5)