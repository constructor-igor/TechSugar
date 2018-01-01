# 
# https://stackoverflow.com/questions/134934/display-number-with-leading-zeros
# 
print("display-number-with-leading-zeros\n")

print("\nsolution #1")
a, b, c = 1, 10, 100
for var in {a, b, c}:
    message = f'{var:05}'
    print("message: ", message)

print("\nsolution #2")
for var in {a, b, c}:
    message = '{:05}'.format(var)
    print("message: ", message)

print("\nsolution #3")
for var in {a, b, c}:
    message = '{num:05d}'.format(num=var)
    print("message: ", message)

print("\nsolution #4")
for var in {a, b, c}:
    message = "{:0>5}".format(var)
    print("message: ", message)

print("\nsolution #5")
for var in {a, b, c}:
    message = str(var).rjust(5, '0')
    print("message: ", message)

print("\nsolution #6")
for var in {a, b, c}:
    message = str(var).zfill(5)
    print("message: ", message)

print("finish")
