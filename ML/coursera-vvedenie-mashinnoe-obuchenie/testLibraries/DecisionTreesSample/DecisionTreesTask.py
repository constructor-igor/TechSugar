import numpy as np
from sklearn.tree import DecisionTreeClassifier
import pandas

data = pandas.read_csv('titanic.csv', index_col='PassengerId')
updatedData = data[['Pclass','Fare', 'Age', 'Sex', 'Survived']]

#
# http://stackoverflow.com/questions/13413590/how-to-drop-rows-of-pandas-dataframe-whose-value-of-certain-column-is-nan
#
#filteredData = updatedData[np.isfinite(updatedData['Pclass']) & np.isfinite(updatedData['Fare']) & np.isfinite(updatedData['Age']) & np.isfinite(updatedData['Sex']) & np.isfinite(updatedData['Survived'])]
#filteredData = updatedData[np.isfinite(updatedData['Pclass']) & np.isfinite(updatedData['Fare'])]
updatedData = updatedData.dropna()

target = updatedData['Survived']
#
# http://stackoverflow.com/questions/13411544/delete-column-from-pandas-dataframe
#
#updatedData = updatedData[['Pclass','Fare', 'Age', 'Sex']]
updatedData.drop('Survived', axis=1, inplace=True)

X = updatedData
y = target
clf = DecisionTreeClassifier(random_state=241)
clf.fit(X, y)

#print("count: %s " % (target.count()))
#
#count = data['Pclass'].value_counts()
#print count