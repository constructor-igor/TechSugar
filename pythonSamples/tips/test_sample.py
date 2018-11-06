import os
import sys
import unittest
from unittest import mock
from unittest.mock import MagicMock
from mymodule import rm


def fact(n):
    """
    Factorial function

    :arg n: Number
    :returns: factorial of n

    """
    if n == 0:
        return 1
    return n * fact(n -1)

class AlgorithmMath:
    def calculate_fact(self, n):
        return fact(n)

class Algorithm:
    def __init__(self, algorithm_math):
        self.algorithm_math =  algorithm_math

    def calculate(self, n):
        return self.algorithm_math.calculate_fact(n) + 0.1

# 
# References:
# https://pymbook.readthedocs.io/en/latest/testing.html
# https://stackoverflow.com/questions/2066508/disable-individual-python-unit-tests-temporarily
# https://docs.python.org/3/library/unittest.mock.html
# https://www.toptal.com/python/an-introduction-to-mocking-in-python
# 

class TestFactorial(unittest.TestCase):
    def test_fact(self):
        res = fact(5)
        self.assertEqual(res, 120)

    @unittest.skip("sample of failed test")
    def test_failed(self):
        res = fact(1)
        self.assertEqual(res, 0)

    def test_algorithm(self):
        algorithm_math = AlgorithmMath()
        algorithm = Algorithm(algorithm_math)
        res = algorithm.calculate(5)
        self.assertEqual(res, 120+0.1)

    def test_algorithm_magicmock(self):
        algorithm_math_mock = AlgorithmMath()
        algorithm_math_mock.calculate_fact = MagicMock(return_value=11)
        algorithm = Algorithm(algorithm_math_mock)
        res = algorithm.calculate(5)
        self.assertEqual(res, 11+0.1)
        algorithm_math_mock.calculate_fact.assert_called_with(5)

    @mock.patch('mymodule.os')
    def test_rm(self, mock_os):
        rm("any path")
        # test that rm called os.remove with the right parameters
        mock_os.remove.assert_called_with("any path")

if __name__ == "__main__":
    print("test_sample: main()")
    unittest.main()
