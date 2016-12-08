import ctypes
import clr

source = 'CSharpLibrary.dll'
a = ctypes.cdll.LoadLibrary(source)
from CSharpLibrary import TestCSharp
t = TestCSharp()

getHello = t.GetHello()
