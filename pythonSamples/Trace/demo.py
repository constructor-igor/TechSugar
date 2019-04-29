import time

def method2():
    return 10

def method1():
    return method2()

if __name__ == "__main__":
    print("[main] started")
    result = method1()
    time.sleep(3)
    result = method1()
    print("[main] finished")