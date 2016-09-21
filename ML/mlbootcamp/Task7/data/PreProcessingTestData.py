import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
plt.style.use('ggplot')

#
#   http://mlbootcamp.ru/article/tutorial/
#

testFilePath = '@original\\x_test.csv'
testResultFilePath = "x_test.csv-target.csv"
testResultNoHeaderFilePath = "x_test.csv-target-no-header.csv"
data = pd.read_csv(testFilePath, na_values='None')

print data.shape
print data.head()
print data.tail()

print data.head()

print "describe"
data_describe = data.describe()
print data_describe

print "columns with single value"
singleValue_columns = [c for c in data.columns if data[c].unique().size == 1]
print singleValue_columns
#for c in singleValue_columns:
#    del data[c]
del data['cacheL3IsShared']
del data['IA.64_Technology']
del data['SSE4a']
del data['SSE2']
del data['SSE']
del data['X3DNow_Pro_Technology']
del data['MMX_Technology']
del data['FXSR.FXSAVE.FXRSTOR']
del data['CLF_._Cache_Line_Flush']
del data['CX8_._CMPXCHG8B']
del data['CMOV_._Conditionnal_Move_Inst.']
del data['SEP_._Fast_System_Call']
del data['TBM']
del data['BMI']

#binary_columns    = [c for c in categorical_columns if data_describe[c]['unique'] == 2]

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

#print data[binary_columns].describe();

data_nonbinary = pd.get_dummies(data[nonbinary_columns])
print data_nonbinary.columns

data_numerical = data[numerical_columns]
data_numerical = (data_numerical - data_numerical.mean()) / data_numerical.std()
data_numerical.describe()

#data_class = data['time']
#del data['time']
#data = pd.concat((data_numerical, data_nonbinary, data_class), axis=1)
data = pd.concat((data_numerical, data[binary_columns], data_nonbinary), axis=1)
data = pd.DataFrame(data, dtype=float)
print data.shape
print data.columns

data.to_csv(testResultFilePath, sep=',', header=True, index=False)
data.to_csv(testResultNoHeaderFilePath, sep=',', header=False, index=False)