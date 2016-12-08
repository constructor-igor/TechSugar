import sys
sys.path.append(r"D:\My\@gh\TechSugar\pythonSamples\CSharpLibrary\CSharpLibrary\bin\Debug")

import ctypes
source = 'CSharpLibrary.dll'
a = ctypes.cdll.LoadLibrary(source)

from DynamicCS import DynamicCalc


clr.AddReference(r"CSharpLibrary.dll")