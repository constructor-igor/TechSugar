import numpy as np
from sklearn.metrics import r2_score
# 
# https://www.investopedia.com/terms/r/r-squared.asp
# 

def calc_rsquared(x, y):
    xmean = x-x.mean()
    ymean = y-y.mean()
    top = np.sum(xmean * ymean)
    bottom_x = np.sum(np.power(xmean, 2))
    bottom_y = np.sum(np.power(ymean, 2))
    bottom = np.sqrt(bottom_x*bottom_y)
    return np.power(top/bottom, 2)

def compute_r2(y_true, y_predicted):
    sse = sum((y_true - y_predicted)**2)
    tse = (len(y_true) - 1) * np.var(y_true, ddof=1)
    r2_score = 1 - (sse / tse)
    return r2_score, sse, tse

def get_r2_numpy_manual(x, y):
    zx = (x-np.mean(x))/np.std(x, ddof=1)
    zy = (y-np.mean(y))/np.std(y, ddof=1)
    r = np.sum(zx*zy)/(len(x)-1)
    return r**2


if __name__ == "__main__":
    x = np.array([3, 10, 11, 15, 22, 22, 23, 28, 28, 6])
    y = np.array([40.88, 34.3, 33.36, 29.6, 23.02, 23.02, 22.08, 17.38, 17.38, 10.8])
    print("x: ", x)
    print("y: ", y)
    r2 = calc_rsquared(x, y)
    print("my r2: ", r2)
    print("r2", np.power(r2_score(x, y), 2))
    r, sse, tst = compute_r2(x, y)
    print("r2: ", np.power(r, 2))
    print("r2: ", get_r2_numpy_manual(x, y))