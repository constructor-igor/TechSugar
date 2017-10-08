# 
# https://www.youtube.com/watch?v=aysceqdGFw8&__s=mysqo5rgfat5kha72eve 
# 
import collections
import multiprocessing
import os
import time
from pprint import pprint


Scientist = collections.namedtuple('Scientist', ['name', 'field', 'born', 'nobel'])

scientists = (
    Scientist(name="Ada Lovelace", field="math", born=1815, nobel=False),
    Scientist(name="Emmy", field="math", born=1882, nobel=False),
    Scientist(name="Marie", field="physics", born=1867, nobel=True),
    Scientist(name="Tu", field="chemistry", born=1930, nobel=True),
    Scientist(name="Ada Yoanth", field="chemistry", born=1939, nobel=True),
    Scientist(name="Vera", field="astronomy", born=1928, nobel=False),
    Scientist(name="Sally", field="physics", born=1951, nobel=False)
)

def transform(x):
    print(f'Process {os.getpid()} working record: {x.name}')
    time.sleep(1)
    result = {'name':x.name, 'age': 2017-x.born}
    print(f'Process {os.getpid()} done processing record {x.name}')
    return result

if __name__ == "__main__":

    pprint(scientists)
    print()

    start = time.time()

    # local sequential
    # result = tuple(map(transform, scientists))

    # multi processing 
    pool = multiprocessing.Pool() #processes=len(scientists)
    result = pool.map(transform, scientists)

    
    end = time.time()

    print(f'\nTime to complete: {end-start:.2f}s\n')
    pprint(result)