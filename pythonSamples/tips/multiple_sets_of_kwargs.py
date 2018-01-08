

def process_data(a, b, c, d):
    print(a, b, c, d)

x = {'a': 1, 'b': 2}
y = {'c': 3, 'd': 4}

print(x)
print(y)

process_data(0, 0, 1, 1)
process_data(**x, **y)
process_data(**x, c=23, d=42)
