import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
plt.style.use('ggplot')

#
#   http://mlbootcamp.ru/article/tutorial/
#

trainingFilePath = 'trainingData.csv'
data = pd.read_csv(trainingFilePath, header=None, na_values='?')

print data.shape
print data.head()
print data.tail()

data.columns = ['A' + str(i) for i in range(1, 16)] + ['class']
print data.head()
print "test access to cell"
print data['A5'][480]
print data.at[480, 'A5']

print "describe"
print data.describe()

print "categorical and  numerical"
categorical_columns = [c for c in data.columns if data[c].dtype.name == 'object']
numerical_columns   = [c for c in data.columns if data[c].dtype.name != 'object']
print categorical_columns
print numerical_columns

print "data[categorical_columns].describe():"
print data[categorical_columns].describe()
print "data.describe(include=[object]):"
print data.describe(include=[object])

print "unique"
for c in categorical_columns:
    print data[c].unique()

#from pandas.tools.plotting import scatter_matrix
#scatter_matrix(data, alpha=0.05, figsize=(10, 10));

#print data.corr()

#
# fill NA values by average
#
print data.count(axis=0);
data = data.fillna(data.median(axis=0), axis=0)
print data.count(axis=0);

data_describe = data.describe(include=[object])
for c in categorical_columns:
    data[c] = data[c].fillna(data_describe[c]['top'])
data.describe(include=[object])

binary_columns    = [c for c in categorical_columns if data_describe[c]['unique'] == 2]
nonbinary_columns = [c for c in categorical_columns if data_describe[c]['unique'] > 2]
print binary_columns, nonbinary_columns

for c in binary_columns[0:]:
    top = data_describe[c]['top']
    top_items = data[c] == top
    data.loc[top_items, c] = 0
    data.loc[np.logical_not(top_items), c] = 1

print data[binary_columns].describe();

data_nonbinary = pd.get_dummies(data[nonbinary_columns])
print data_nonbinary.columns

data_numerical = data[numerical_columns]
data_numerical = (data_numerical - data_numerical.mean()) / data_numerical.std()
data_numerical.describe()

data_class = data['class']
del data['class']
data = pd.concat((data_numerical, data[binary_columns], data_nonbinary, data_class), axis=1)
data = pd.DataFrame(data, dtype=float)
print data.shape
print data.columns

data.to_csv(trainingFilePath+"-target.csv", sep=',', header=False)