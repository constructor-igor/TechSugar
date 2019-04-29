import itertools

lst = ["1st", "2nd", "3rd", "4th"]
a, b, c = itertools.tee(iter(list(range(10))))
print(f"{next(a)}, {next(c)}")
print(f"{next(a)}, {next(b)}")
print(f"{next(a)}, {next(b)}, {next(c)}")