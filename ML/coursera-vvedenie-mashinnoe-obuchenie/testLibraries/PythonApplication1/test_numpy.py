print('Hello World')
import numpy

X = np.random.normal(loc=1, scale=10, size=(1000, 50))
X

m = np.mean(X, axis=0)
std = np.std(X, axis=0)
X_norm = ((X - m)  / std)
X_norm

Z = np.array([[4, 5, 0], 
             [1, 9, 3],              
             [5, 1, 1],
             [3, 3, 3], 
             [9, 9, 9], 
             [4, 7, 1]])
r = np.sum(Z, axis=1)
np.nonzero(r > 10)

A = np.eye(3)
B = np.eye(3)
A
B
AB = np.vstack((A, B))
AB