import pandas as pd


df = pd.DataFrame(columns = ["a", "b"])
# df.append({'a':10.0, 'b': 20.0}, ignore_index=True)
df.loc[0] = [10.0, 20.0]
df.loc[1] = {"a": 11.0, "b": 21.0}
df.loc[2] = {"a": 12.0, "b": "31.0"}

print("df.head: ", df.head())
print("length: ", len(df["a"]))

for index in range(0,  len(df["b"])):
    print("index: ", index)
    print("value: ", float(df["b"][index]))

found_row = df.loc[df['a'] == 11]
print("found: ", found_row)
