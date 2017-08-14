# 
# https://docs.scipy.org/doc/numpy-dev/reference/generated/numpy.matlib.repmat.html
# 
import numpy as np
import numpy.matlib

a0 = np.array(1)
a0_repmat = np.matlib.repmat(a0, 2, 3)
print("==== a0 ====")
print(a0)
print(a0_repmat)

a1 = np.arange(4)
a1_repmat = np.matlib.repmat(a1, 2, 2)
print("==== a1 ====")
print(a1)
print(a1_repmat)

a2 = np.asmatrix(np.arange(6).reshape(2, 3))
a2_repmat = np.matlib.repmat(a2, 2, 3)
print("==== a2 ====")
print(a2)
print(a2_repmat)
