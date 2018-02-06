from time import sleep
import datetime
import pygmo as pg
from pygmo import *


# https://esa.github.io/pagmo2/quickstart.html
def quick_start_demo():
    # 1 - Instantiate a pygmo problem constructing it from a UDP
    # (user defined problem).
    prob = pg.problem(pg.schwefel(30))

    # 2 - Instantiate a pagmo algorithm
    algo = pg.algorithm(pg.sade(gen=100))

    # 3 - Instantiate an archipelago with 16 islands having each 20 individuals
    archi = pg.archipelago(16, algo=algo, prob=prob, pop_size=20)

    # 4 - Run the evolution in parallel on the 16 separate islands 10 times.
    archi.evolve(10)

    # 5 - Wait for the evolutions to be finished
    archi.wait()

    # 6 - Print the fitness of the best solution in each island
    res = [isl.get_population().champion_f for isl in archi]
    print(res)


class sphere_function:
    def __init__(self, dim):
        self.dim = dim
    def get_name(self):
        return "Sphere Function"
    def get_extra_info(self):
        return "\tDimensions: " + str(self.dim)
    def fitness(self, x):
        print("[{0}, {1}] x: {2}".format(id(self), datetime.datetime.now(), x))
        sleep(0.01)
        return [sum(x*x)]
    def get_bounds(self):
        return ([-1] * self.dim, [1] * self.dim)


# https://esa.github.io/pagmo2/docs/python/tutorials/coding_udp_simple.html
def user_defined_problem():
    prob = pg.problem(sphere_function(3))
    print(prob)

    algo = pg.algorithm(pg.bee_colony(gen = 200, limit = 20))
    pop = pg.population(prob, 10)
    pop = algo.evolve(pop)
    print(pop.champion_f)
    print(pop)


# https://esa.github.io/pagmo2/docs/python/algorithms/py_algorithms.html?highlight=nlopt#pygmo.nlopt
def nlopt_sample():
    nl = nlopt('bobyqa')
    nl.xtol_rel = 1E-6 # Change the default value of the xtol_rel stopping criterion
    print("nl.xtol_rel: ", nl.xtol_rel )

    algo = algorithm(nl)
    algo.set_verbosity(1)
    prob = problem(sphere_function(3))
    # prob.c_tol = [1E-6] * 3 # Set constraints tolerance to 1E-6
    pop = population(prob, 20)
    pop = algo.evolve(pop)
    print("pop: ", pop)

    return


def nlopt_parallel_sample():
    print("nlopt_parallel_sample started")
    prob = problem(sphere_function(3))
    nl = nlopt('bobyqa')
    algo = algorithm(nl)

    archi = pg.archipelago(4, algo=algo, prob=prob, pop_size=20)
    print(archi)

    archi.evolve(10)
    archi.wait()

    res = [isl.get_population().champion_f for isl in archi]
    print(res)

    print("nlopt_parallel_sample completed")


def nlopt_topology_parallel_sample():
    print("nlopt_parallel_sample started")
    prob = problem(sphere_function(3))
    nl = nlopt('bobyqa')
    algo = algorithm(nl)

    archi = archipelago(topology.rim())
    archi.push_back(island(prob, algo, 10))
    archi.push_back(island(prob, algo, 10))

    archi.evolve(20)
    archi.join

    # archi = pg.archipelago(4, algo=algo, prob=prob, pop_size=20)
    # print(archi)

    # archi.evolve(10)
    # archi.wait()

    # res = [isl.get_population().champion_f for isl in archi]
    # print(res)

    print("nlopt_parallel_sample completed")

if __name__ == "__main__":
    # quick_start_demo()
    # user_defined_problem()
    # nlopt_sample()
    # nlopt_parallel_sample()
    nlopt_topology_parallel_sample()
