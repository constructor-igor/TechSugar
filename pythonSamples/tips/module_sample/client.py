# 
# https://realpython.com/python-modules-packages/
# 
# sys.path.append(r'C:\Users\john')
import mod

print("mod: ", mod)
print(mod.s)
print(mod.a)
mod.foo(['one', 'two', 'three'])
x = mod.Foo()
print(x)

import sys
print(sys.path)

print("mod.__file__: ", mod.__file__)
import re
print("re.__file__: ", re.__file__)

s = "local s"
print("s: ", s)
from mod import s
print("s: ", s)
from mod import s as alt_s
print("alt_s: ", alt_s)

import mod as alt_mod
print("alt_mod.s, ", alt_mod.s)

print("dir(): ", dir())
print("dir(mod): ", dir(mod))

from fact import fact
print("fact(6): ", fact(6))

print("<<import mod again (no additional output)")
import mod
import mod
print("import mod again (no additional output)>>")

print("<<import mod again (with reload)")
import importlib
importlib.reload(mod)
print("import mod again (with reload)>>")

print("pkg sample")
import pkg.mod1, pkg.mod2
# import pkg
pkg.mod1.foo()
print("pkg.mod2.Bar(): ", pkg.mod2.Bar())
from pkg.mod1 import foo
foo()