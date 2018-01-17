# 
# https://habrahabr.ru/post/346272/
# 
from functools import partial
from operator import is_
from itertools import filterfalse
from functools import reduce

def compose(*fns):
    init, *rest = reversed(fns)
    return lambda *a, **kw: reduce(lambda a, b: b(a), rest, init(*a, **kw))

is_none = partial(is_, None)
filter_none = partial(filterfalse, is_none)

# def is_even(item):
#     return item % 2 ==0
is_even = lambda x:  x % 2 ==0

filter_bool = partial(filter, bool)
filter_even = partial(filter, is_even)

items = [True, False, True, False]
print(list(filter_bool(items)))

items = [0, 1, 2, 3, 4, 5]
print(list(filter_even(items)))

seq = [None, 1, 2, None, 3]
no_none = (x for x in seq if x is not None)
print("no_none: ", list(no_none))
print("no_none: ", list(filter_none(seq)))
print("all none: ", list(compose(all, partial(map, is_none))))