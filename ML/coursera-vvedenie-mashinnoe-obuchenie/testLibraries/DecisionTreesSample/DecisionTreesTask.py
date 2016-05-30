import pandas

data = pandas.read_csv('titanic.csv', index_col='PassengerId')

count = data['Pclass'].value_counts()
print count