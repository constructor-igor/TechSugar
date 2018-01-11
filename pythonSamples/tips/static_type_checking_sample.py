# 
# https://medium.com/@ageitgey/learn-how-to-use-static-type-checking-in-python-3-6-in-10-minutes-12c86d72677b
# 

# 
# for VSC it's necessary to add "python.linting.mypyEnabled": true
# 


def foo(parameter):
    return parameter + parameter


def foo_typed(parameter: str) -> str:
    return parameter + parameter

variable_name: type
my_string: str = "My String Value"

print(my_string)
print(foo('a'))
print(foo(5))

print(foo_typed('b'))
print(foo_typed(6))
