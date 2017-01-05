''' joblib sample '''
#
#   https://blog.dominodatalab.com/simple-parallelization/
#
from joblib import Parallel, delayed
import multiprocessing

# what are your inputs, and what operation do you want to
# perform on each input. For example...
inputs = range(10)

def workingFunction(taskIndex):
    print("taskIndex: {0}".format(taskIndex))
    return taskIndex*taskIndex

def parallelSample():
    num_cores = multiprocessing.cpu_count()
    print("num_cores:" + str(num_cores))
    print("inputs:" + ", ".join(str(x) for x in inputs))
    results = Parallel(n_jobs=num_cores)(delayed(workingFunction)(i) for i in inputs)
    print("results:" + ", ".join(str(x) for x in results))
    return

if __name__ == '__main__':
    parallelSample()
    