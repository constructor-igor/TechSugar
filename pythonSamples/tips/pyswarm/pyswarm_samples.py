from time import sleep
import os
import datetime
import logging
import pso2


def create_logger(level=logging.INFO):
    # https://docs.python.org/2/library/logging.html#logrecord-attributes
    logging.basicConfig(level=level, format='%(asctime)s %(name)s %(levelname)-5s [%(module)s.%(funcName)s()] %(message)s')
    logger = logging.getLogger()
    logger.setLevel(level)
    return logger


def banana(points, *args):
    result = [x[0]**4 - 2*x[1]*x[0]**2 + x[1]**2 + x[0]**2 - 2*x[0] + 5 for x in points]
    return result
    # result = []
    # for x in points:
    #     x1 = x[0]
    #     x2 = x[1]
    #     mf = x1**4 - 2*x2*x1**2 + x2**2 + x1**2 - 2*x1 + 5
    #     result.append(mf)
    # return result


def con(points, *args):
    result = [[-(x[0] + 0.25)**2 + 0.75*x[1]] for x in points]
    return result
    # x1 = x[0]
    # x2 = x[1]
    # return [-(x1 + 0.25)**2 + 0.75*x2]


# https://pythonhosted.org/pyswarm/
def intro_sample(processes, logger):
    lb = [-3, -1]
    ub = [2, 6]

    xopt, fopt = pso2.pso(banana, lb, ub, f_ieqcons=con, debug=False, processes=processes)
    print("xopt: ", xopt)
    print("fopt: ", fopt)
    # Optimum should be around x=[0.5, 0.76] with banana(x)=4.5 and con(x)=0

if __name__ == "__main__":
    print("[Main] started")
    logger = create_logger()
    for processes in [0]:
        intro_sample(processes, logger)
    print("[Main] finished")