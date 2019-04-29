import math
import functools

def debug(func):
    """Print the function signature and return value"""
    @functools.wraps(func)
    def wrapper_debug(*args, **kwargs):
        args_repr = [repr(a) for a in args]                      # 1
        kwargs_repr = [f"{k}={v!r}" for k, v in kwargs.items()]  # 2
        signature = ", ".join(args_repr + kwargs_repr)           # 3
        print(f"Calling {func.__name__}({signature})")
        value = func(*args, **kwargs)
        print(f"{func.__name__!r} returned {value!r}")           # 4
        return value
    return wrapper_debug

def approximate_e(terms=18):
    return sum(1 / math.factorial(n) for n in range(terms))

if __name__ == "__main__":
    print("[main] started")
    print("[approximate_e()] starting")
    approximate_e()
    print("[approximate_e()] completed")

    print("[math.factorial = debug(math.factorial)")
    # Apply a decorator to a standard library function
    math.factorial = debug(math.factorial)

    print("[approximate_e()] starting")
    approximate_e()
    print("[approximate_e()] completed")

    print("[main] finished")