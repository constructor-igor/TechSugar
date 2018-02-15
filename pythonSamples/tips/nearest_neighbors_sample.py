# 
# https://www.youtube.com/watch?v=EEUXKG97YRw
# 
import numpy as np

print("1000 points in 3 dimensions")
X = np.random.random((1000, 3))
print(X.shape)
print(X)

print("Broadcasting to find pairwise differences")
diff = X.reshape(1000, 1, 3) - X
print(diff.shape)
print(diff)

print("Aggregate to find pairwise distances")
D = (diff ** 2).sum(2)
print(D.shape)
print(D)

print("set diagonal to infinity to skip self-neighbors")
i = np.arange(1000)
D[i, i] = np.inf
print(D)

print("print the indices of the nearest neigbor")
i = np.argmin(D, 1)
print(i[:10])

print("double-check with scikit-learn")
from sklearn.neighbors import NearestNeighbors
d, i = NearestNeighbors().fit(X).kneighbors(X, 2)
print(i[:10])
