# collections.Counter lets you find the most common
# elements in an iterable:

import collections
c = collections.Counter('helloworld')

print("all items:", c)
print("most common:", c.most_common(3))
