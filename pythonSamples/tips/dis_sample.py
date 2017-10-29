import dis

def greet(name):
    return 'Hello, ' + name + '!'
print(greet('Dan'))

print(dis.dis(greet))