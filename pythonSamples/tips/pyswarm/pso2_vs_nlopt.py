import numpy as np
from scipy.linalg import eig
import pso2
import nlopt


def rosen(x, *args):
    """The Rosenbrock function"""
    return sum(100.0*(x[1:]-x[:-1]**2.0)**2.0 + (1-x[:-1])**2.0)

def udf_single(x, *args):
    x1 = x[0]
    x2 = x[1]
    return x1**4 - 2*x2*x1**2 + x2**2 + x1**2 - 2*x1 + 5


def con_single(x, *args):
    x1 = x[0]
    x2 = x[1]
    return [-(x1 + 0.25)**2 + 0.75*x2]


def multi(udf, points):
    return [udf(x) for x in points]


def wrapped_multi(func):
    def multi_func(*args, **kwargs):
        return multi(func, *args, **kwargs)
    return multi_func

def test_udf_funcs():
    print("udf_single:", udf_single([0.0, 1.0]))
    print("multi(udf_single: ", multi(udf_single, [[0.0, 1.0], [1.0, 2.0]]))
    print("con_single: ", con_single([0.0, 1.0]))
    print("multi(con_single: ", multi(con_single, [[0.0, 1.0], [1.0, 2.0]]))


def test_wrapped():
    f = wrapped_multi(udf_single)
    print("wrapped_multi", f([[0.0, 1.0]]))

def pso2_sample():
    print("[pso2_sample] started")
    lb = [-3, -1]
    ub = [2, 6]
    xopt, fopt = pso2.pso(wrapped_multi(udf_single), lb, ub, debug=True, processes=0)
    # xopt, fopt = pso2.pso(wrapped_multi(udf_single), lb, ub, f_ieqcons=wrapped_multi(con_single), debug=True, processes=0)
    print("xopt: ", xopt)
    print("fopt: ", fopt)
    print("[pso2_sample] finished")


def pso2_rosen_sample():
    print("[pso2_sample] started")
    lb = [-10] * 5
    ub = [+10] * 5
    xopt, fopt = pso2.pso(rosen, lb, ub, debug=True, processes=1, maxiter=1000)
    # xopt, fopt = pso2.pso(wrapped_multi(udf_single), lb, ub, f_ieqcons=wrapped_multi(con_single), debug=True, processes=0)
    print("xopt: ", xopt)
    print("fopt: ", fopt)
    print("[pso2_sample] finished")

def nlopt_rosen_sample():
    print("[nlopt_sample] started")
    lb = [-10] * 5
    ub = [+10] * 5
    opt = nlopt.opt(nlopt.LN_BOBYQA, 5)  # LN_BOBYQA
    opt.set_lower_bounds(lb)
    opt.set_upper_bounds(ub)
    opt.set_min_objective(rosen)
    opt.set_xtol_rel(1e-8)
    opt.set_maxeval(int(1000))  # Maximum number of function evaluations
    x = opt.optimize([2]*5)
    minf = opt.last_optimum_value()
    print("optimum at ", x)
    print("minimum value = ", minf)
    print("result code = ", opt.last_optimize_result())
    print("udf_single(best) = ", rosen(x))
    print("[nlopt_sample] finished")


def nlopt_sample():
    print("[nlopt_sample] started")
    lb = [-3, -1]
    ub = [2, 6]
    opt = nlopt.opt(nlopt.LN_BOBYQA, 2)  # LN_BOBYQA
    opt.set_lower_bounds(lb)
    opt.set_upper_bounds(ub)
    opt.set_min_objective(udf_single)
    opt.set_xtol_rel(1e-8)
    opt.set_maxeval(int(100))  # Maximum number of function evaluations
    x = opt.optimize([0, 1])
    minf = opt.last_optimum_value()
    print("optimum at ", x[0], x[1])
    print("minimum value = ", minf)
    print("result code = ", opt.last_optimize_result())
    print("udf_single(best) = ", udf_single(x))
    print("[nlopt_sample] finished")
    
# https://stackoverflow.com/questions/45630286/nlopt-minimize-eigenvalue-python
def so_sample():
    def my_func(x, grad):
        arr = np.array([[x[0]+x[1],-2],[-2,x[1]-2*(x[1]+x[0])]])
        ev, ew=eig(arr)
        return ev[0].real
    opt = nlopt.opt(nlopt.LN_BOBYQA, 2)
    opt.set_lower_bounds([1.0,1.0])
    opt.set_min_objective(my_func)
    opt.set_xtol_rel(1e-7)
    x = opt.optimize([10.0, 3.5])
    minf = opt.last_optimum_value()
    print("optimum at ", x)
    print("minimum value = ", minf)
    print("result code = ", opt.last_optimize_result())


if __name__ == "__main__":
    print("[Main] started")
    test_udf_funcs()
    test_wrapped()
    # pso2_sample()
    # nlopt_sample()
    # so_sample()
    pso2_rosen_sample()
    # nlopt_rosen_sample()
    print("[Main] finished")