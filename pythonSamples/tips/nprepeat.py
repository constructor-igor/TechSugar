import numpy as np
from sklearn.preprocessing import normalize

def get_weigths_list(num_bins, num_wl_per_comp):
    num_wl_per_bin = []
    for num_wl in num_wl_per_comp:
        wl_per_bin = num_wl // num_bins
        last_bin = num_wl - wl_per_bin * (num_bins - 1)
        assert wl_per_bin > 0
        num_wl_per_bin.extend([wl_per_bin] * (num_bins - 1))
        num_wl_per_bin.append(last_bin)
    return num_wl_per_bin


print("start")

num_bins = 10
num_wl_per_comp = [250, 250]
reflectivity_shape_0 = 4
num_wl_per_bin = [25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25]
num_wl_per_bin = [25, 25, 25, 25, 25, 25, 25, 25, 25, 25]
num_wl_per_bin = get_weigths_list(num_bins, num_wl_per_comp)
num_comp = 2
weights = [1, 1, 1, 1, 1, 1, 1, 1, 1, 1]

weights = np.array(weights).reshape((1, -1))
weights = normalize(weights) * np.sqrt(num_comp)
weights = np.tile(weights, [reflectivity_shape_0, 1])

W_per_wl = np.repeat(weights, num_wl_per_bin, axis=1)   # ERROR: ValueError : operands could not be broadcast together with shape (10,) (20,)

print("finish")