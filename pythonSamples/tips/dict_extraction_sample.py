

big_dict = {'a':1,'b':2, 'c':3, 'z':26} 
short_keys = ['a', 'z']

# 
# straightforward way
# 
short_dict = dict()
for key in short_keys:
    short_dict[key] = big_dict[key]
print(short_dict)

# 
# short way
# 
short_dict = dict((key, big_dict[key]) for key in short_keys if key in big_dict)
print(short_dict)