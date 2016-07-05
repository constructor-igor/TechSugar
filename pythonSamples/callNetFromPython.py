import ctypes
source = 'CSharpLibrary.dll'
a = ctypes.cdll.LoadLibrary(source)
r = a.add(3, 5)
