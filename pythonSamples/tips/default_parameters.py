
def append_length(lst=[]):
    lst.append(len(lst))
    return lst

print(append_length([1, 2])) # [1, 2, 2]
print(append_length()) # [0]
print(append_length()) # [0, 1] 

def fact(x, cache={0: 1}):
    print(f"  fact({x})")
    if x not in cache:
        cache[x] = x * fact(x - 1)
    return cache[x]

print(f"fact(5) = {fact(5)}")
print(f"fact(5) = {fact(5)}")
print(f"fact.__defaults__ = {fact.__defaults__}")