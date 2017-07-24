
# Reference: https://arrow.apache.org/docs/python/parquet.html
#           https://www.continuum.io/blog/developer-blog/introducing-fastparquet

import fastparquet

import numpy as np
import pandas as pd
# import pyarrow as pa

def test_1():
    df = pd.DataFrame({'one': [-1, np.nan, 2.5], 'two': ['foo', 'bar', 'baz'],'three': [True, False, True]})
    # write data
    fastparquet.write('test_1.parq', df, compression='UNCOMPRESSED')

def test_2():
    # to make and save a large-ish DataFrame
    N = 10000000
 
    df = pd.DataFrame({'ints': np.random.randint(0, 1000, size=N),
                   'floats': np.random.randn(N),
                   'times': pd.DatetimeIndex(start='1980', freq='s', periods=N)})
    df.to_csv('test_2.csv')
    fastparquet.write('test_2_UNCOMPRESSED.parq', df, compression='UNCOMPRESSED')
    # fastparquet.write('test_2_GZIP.parq', df, compression='GZIP')

if __name__ == "__main__":
    test_1()
    test_2()
