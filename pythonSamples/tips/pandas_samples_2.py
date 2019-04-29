#
#   https://realpython.com/fast-flexible-pandas/
#
import os
import pandas as pd

print(pd.__version__)
dir_path = os.path.dirname(os.path.realpath(__file__))
data_file_path = os.path.join(dir_path, 'demand_profile.csv')
print(f"file path: {data_file_path}")

df = pd.read_csv(data_file_path)
print(df.head())