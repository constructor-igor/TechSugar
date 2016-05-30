import numpy as np
from sklearn.tree import DecisionTreeClassifier
X = np.array([[1, 2], [3, 4], [5, 6]])
y = np.array([0, 1, 0])
clf = DecisionTreeClassifier()
clf.fit(X, y)

importances = clf.feature_importances_

testNanValue = float('nan')
testOneValue = 1.0

print np.isnan(testOneValue)
print np.isnan(testNanValue)