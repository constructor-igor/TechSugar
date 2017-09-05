
import json

my_mapping = {'a': 23, 'b': 42, 'c': 0xc0ffee}

print(my_mapping)
print(json.dumps(my_mapping, indent=4, sort_keys=True))

# failed, because key must be string; error: 'TypeError: keys must be a string'.
# print(json.dumps({all: 'yup'}))